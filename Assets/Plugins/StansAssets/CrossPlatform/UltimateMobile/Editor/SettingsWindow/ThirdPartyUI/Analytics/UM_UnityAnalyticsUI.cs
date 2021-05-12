using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;

namespace SA.CrossPlatform.Editor
{
    static class UM_UnityAnalyticsUI
    {
        static readonly GUIContent s_LimitUserTrackingLabel = new GUIContent("Limit User Tracking");
        static readonly GUIContent s_DeviceStatsEnabledLabel = new GUIContent("Device Stats Enabled");

        public static void OnGUI()
        {
            var unityClient = UM_Settings.Instance.Analytics.UnityClient;

            EditorGUILayout.HelpBox("Controls whether to limit user tracking at runtime.", MessageType.Info);
            unityClient.LimitUserTracking = IMGUILayout.ToggleFiled(s_LimitUserTrackingLabel, unityClient.LimitUserTracking, IMGUIToggleStyle.ToggleType.YesNo);
            EditorGUILayout.Space();

            EditorGUILayout.HelpBox("Controls whether the sending of device stats at runtime is enabled.", MessageType.Info);
            unityClient.DeviceStatsEnabled = IMGUILayout.ToggleFiled(s_DeviceStatsEnabledLabel, unityClient.DeviceStatsEnabled, IMGUIToggleStyle.ToggleType.YesNo);
        }
    }
}
