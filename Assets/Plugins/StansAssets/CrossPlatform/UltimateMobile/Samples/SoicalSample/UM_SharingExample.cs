using SA.CrossPlatform.Social;
using SA.CrossPlatform.UI;
using SA.Foundation.Templates;
using SA.iOS.Foundation;
using SA.iOS.Photos;
using SA.iOS.UIKit;
using StansAssets.Foundation;
using UnityEngine;
using UnityEngine.UI;

public class UM_SharingExample : MonoBehaviour
{
    [Header("Native Sharing")]
    [SerializeField]
    Button m_SystemSharingDialog = null;
    [SerializeField]
    Button m_SendMailDialog = null;

    [Header("Social Media")]
    [SerializeField]
    Button m_Facebook = null;
    [SerializeField]
    Button m_Instagram = null;
    [SerializeField]
    Button m_Twitter = null;
    [SerializeField]
    Button m_Whatsapp = null;

    void Start()
    {
        //Native Sharing
        m_SystemSharingDialog.onClick.AddListener(() =>
        {
            var client = UM_SocialService.SharingClient;
            client.SystemSharingDialog(MakeSharingBuilder(), PrintSharingResult);
        });

        m_SendMailDialog.onClick.AddListener(() =>
        {
            var client = UM_SocialService.SharingClient;
            var dialog = new UM_EmailDialogBuilder();

            dialog.SetSubject("Subject");
            dialog.SetText("Hello World!");
            dialog.SetUrl("https://stansassets.com/");

            //Juts generating simple red texture with 32x32 resolution
            var sampleRedTexture = Texture2DUtility.MakePlainColorImage(Color.red, 32, 32);
            dialog.AddImage(sampleRedTexture);
            dialog.AddRecipient("support@stansassets.com");

            client.ShowSendMailDialog(dialog, PrintSharingResult);
        });

        //Sharing to Social Media
        m_Facebook.onClick.AddListener(() =>
        {
            var client = UM_SocialService.SharingClient;
            client.ShareToFacebook(MakeSharingBuilder(), PrintSharingResult);
        });

        m_Instagram.onClick.AddListener(() =>
        {
            //var client = UM_SocialService.SharingClient;

            // client.ShareToInstagram(MakeSharingBuilder(), PrintSharingResult);
            var sampleRedTexture = Texture2DUtility.MakePlainColorImage(Color.red, 32, 32);
            ISN_PhotoAlbum.UIImageWriteToSavedPhotosAlbum(sampleRedTexture, (result) =>
            {
                if (result.HasError)
                {
                    Debug.LogError(result.Error.FullMessage);
                    return;
                }

                var fetchOptions = new ISN_PHFetchOptions();
                fetchOptions.SortDescriptor = new ISN_NSSortDescriptor("creationDate", false);
                fetchOptions.FetchLimit = 1;

                var fetchResult = ISN_PHAsset.FetchAssetsWithOptions(fetchOptions);

                var lastAsset = fetchResult.FirstObject;
                var url = "instagram://library?LocalIdentifier=" + lastAsset.LocalIdentifier;

                if (ISN_UIApplication.CanOpenURL(url))
                    ISN_UIApplication.OpenURL(url);
                else
                    Debug.LogError("Instagram application is not installed!");
            });
        });

        m_Twitter.onClick.AddListener(() =>
        {
            var client = UM_SocialService.SharingClient;
            client.ShareToTwitter(MakeSharingBuilder(), PrintSharingResult);
        });

        m_Whatsapp.onClick.AddListener(() =>
        {
            var client = UM_SocialService.SharingClient;
            client.ShareToWhatsapp(MakeSharingBuilder(), PrintSharingResult);
        });
    }

    UM_ShareDialogBuilder MakeSharingBuilder()
    {
        var builder = new UM_ShareDialogBuilder();
        builder.SetText("Hello world!");
        builder.SetUrl("https://stansassets.com/");

        //Juts generating simple red texture with 32x32 resolution
        var sampleRedTexture = Texture2DUtility.MakePlainColorImage(Color.red, 32, 32);
        builder.AddImage(sampleRedTexture);

        return builder;
    }

    public static void PrintSharingResult(SA_Result result)
    {
        if (result.IsSucceeded)
        {
            UM_DialogsUtility.ShowMessage("Result", "Sharing Completed.");
            Debug.Log("Sharing Completed.");
        }
        else
        {
            UM_DialogsUtility.ShowMessage("Result", "Failed to share: " + result.Error.FullMessage);
            Debug.Log("Failed to share: " + result.Error.FullMessage);
        }
    }
}
