using SA.CrossPlatform.Media;
using SA.iOS.AVFoundation;
using SA.iOS.AVKit;
using SA.iOS.Foundation;
using UnityEngine;
using UnityEngine.UI;

public class UM_MedialPlayerExample : MonoBehaviour
{
    [Header("Unified")]
    [SerializeField]
    Button m_PlayButton = null;

    [Header("iOS Only")]
    [SerializeField]
    Button m_IOSPlayButton = null;
    [SerializeField]
    Toggle m_AllowsPictureInPicturePlayback = null;
    [SerializeField]
    Toggle m_ShouldCloseWhenFinished = null;
    [SerializeField]
    Toggle m_ShowsPlaybackControls = null;

    //"https://videocdn.bodybuilding.com/video/mp4/62000/62792m.mp4";
    //http://techslides.com/demos/sample-videos/small.mp4
    const string k_ExampleVideoUrl = "https://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4";

    void Awake()
    {
        m_PlayButton.onClick.AddListener(() =>
        {
            UM_MediaPlayer.ShowRemoteVideo(k_ExampleVideoUrl, () =>
            {
                Debug.Log("Video closed");
            });
        });

        IOSOnlySetup();
    }

    void IOSOnlySetup()
    {
        m_IOSPlayButton.onClick.AddListener(() =>
        {
            var url = ISN_NSUrl.UrlWithString(k_ExampleVideoUrl);
            var player = new ISN_AVPlayer(url);

            var viewController = new ISN_AVPlayerViewController();
            viewController.Player = player;

            //Optional setting that you can apply
            player.Volume = 0.8f;

            viewController.ShowsPlaybackControls = m_ShowsPlaybackControls.isOn;
            viewController.AllowsPictureInPicturePlayback = m_AllowsPictureInPicturePlayback.isOn;
            viewController.ShouldCloseWhenFinished = m_ShouldCloseWhenFinished.isOn;

            viewController.Show();
        });
    }
}
