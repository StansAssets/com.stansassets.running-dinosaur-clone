using SA.iOS.Foundation;
using UnityEngine;
using UnityEngine.UI;
using SA.iOS.MediaPlayer;
using SA.iOS.UIKit;

public class UM_MusicPlayerExample : MonoBehaviour
{
    [SerializeField]
    Text m_StatusLabel = default;

    [Header("Media Player")]
    [SerializeField]
    Button m_Play = default;
    [SerializeField]
    Button m_Stop = default;
    [SerializeField]
    Button m_Pause = default;
    [SerializeField]
    Button m_Next = default;
    [SerializeField]
    Button m_Previous = default;

    [Header("Media Picker")]
    [SerializeField]
    Button m_OpenMediaPicker = default;

    // Start is called before the first frame update
    ISN_MPMusicPlayerController m_Player;

    void Start()
    {
        m_Player = ISN_MPMusicPlayerController.SystemMusicPlayer;
        UpdatePlayerStateUI();

        m_Play.onClick.AddListener(() => { m_Player.Play(); });
        m_Stop.onClick.AddListener(() => { m_Player.Stop(); });
        m_Pause.onClick.AddListener(() => { m_Player.Pause(); });
        m_Next.onClick.AddListener(() => { m_Player.SkipToNextItem(); });
        m_Previous.onClick.AddListener(() => { m_Player.SkipToPreviousItem(); });

        //Subscribing ot the events
        m_Player.BeginGeneratingPlaybackNotifications();

        var center = ISN_NSNotificationCenter.DefaultCenter;
        center.AddObserverForName(ISN_MPMusicPlayerController.NowPlayingItemDidChange,
            notification =>
            {
                UpdatePlayerStateUI();
                Debug.Log("MusicPlayer Now Playing Item Did Change");
            });

        center.AddObserverForName(ISN_MPMusicPlayerController.PlaybackStateDidChange,
            notification =>
            {
                UpdatePlayerStateUI();
                Debug.Log("MusicPlayer Playback State Did Change");
            });

        m_OpenMediaPicker.onClick.AddListener(() =>
        {
            var pickerController = new ISN_MPMediaPickerController();
            pickerController.AllowsPickingMultipleItems = true;

            if (ISN_UIDevice.CurrentDevice.UserInterfaceIdiom == ISN_UIUserInterfaceIdiom.IPad)
                pickerController.ModalPresentationStyle = ISN_UIModalPresentationStyle.Popover;

            pickerController.SetDelegate(new MyMediaPickerDelegate());
            pickerController.PresentViewController(true, () => { });
        });
    }

    void UpdatePlayerStateUI()
    {
        var item = m_Player.NowPlayingItem;
        m_StatusLabel.text = $"{m_Player.PlaybackState} = {item.Title}/{item.Artist}";
    }

    public class MyMediaPickerDelegate : ISN_IMPMediaPickerControllerDelegate
    {
        public void DidPickMediaItems(ISN_MPMediaPickerController mediaPicker,
            ISN_MPMediaItemCollection mediaItemCollection)
        {
            Debug.Log("MyMediaPickerDelegate::DidPickMediaItems");
            var musicPlayer = ISN_MPMusicPlayerController.SystemMusicPlayer;
            musicPlayer.SetQueueWithItemCollection(mediaItemCollection);
            musicPlayer.Play();

            mediaPicker.Dismiss(true, () => { });
        }

        public void MediaPickerDidCancel(ISN_MPMediaPickerController mediaPicker)
        {
            Debug.Log("MyMediaPickerDelegate::MediaPickerDidCancel");
            mediaPicker.Dismiss(true, () => { });
        }
    }
}
