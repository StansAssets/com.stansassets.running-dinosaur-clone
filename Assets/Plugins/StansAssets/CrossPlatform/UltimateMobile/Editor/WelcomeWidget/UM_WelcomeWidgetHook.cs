using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SA.CrossPlatform.Editor
{
    [InitializeOnLoad]
    static class UM_WelcomeWidgetHook
    {
        const string k_TriggerSceneName = "UM_Welcome";
        static readonly Dictionary<SceneView, UM_WelcomeWidget> s_Widgets = new Dictionary<SceneView, UM_WelcomeWidget>();

        static UM_WelcomeWidgetHook()
        {
#if UNITY_2019_1_OR_NEWER
            SceneView.duringSceneGui += OnSceneGUI;
#else
            SceneView.onSceneGUIDelegate += OnSceneGUI;
#endif
        }

        public static void Restart()
        {
            s_Widgets.Clear();
        }

        static void OnSceneGUI(SceneView sceneView)
        {
            if (Application.isPlaying || SceneManager.GetActiveScene().name != k_TriggerSceneName) return;

            UM_WelcomeWidget widget;
            if (s_Widgets.ContainsKey(sceneView))
            {
                widget = s_Widgets[sceneView];
            }
            else
            {
                widget = new UM_WelcomeWidget(sceneView);
                s_Widgets.Add(sceneView, widget);
            }

            widget.OnGUI();
        }
    }
}
