using StansAssets.Foundation.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SA.CrossPlatform.Samples
{
    [ExecuteInEditMode]
    public class UM_WelcomeController : MonoBehaviour
    {
        [SerializeField]
        GameObject m_ButtonsPanel = null;
        [SerializeField]
        GameObject m_FeatureViewport = null;

        Scene m_CurrentlyFeaturedScene;

        void Start()
        {
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
            var buttons = m_ButtonsPanel.GetComponentsInChildren<Button>();
            foreach (var button in buttons)
            {
                var currentButton = button;
                button.onClick.AddListener(() =>
                {
                    var sceneLink = currentButton.GetComponent<UM_SceneLink>();
                    if (sceneLink != null) LoadScene(sceneLink.SceneName);
                });
            }

            InitMainScreenServices();
        }

        void InitMainScreenServices()
        {
            UM_LocalNotificationsExample.SubscribeToTheNotificationEvents();
        }

        void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            m_CurrentlyFeaturedScene = scene;
            foreach (var rootGameObject in scene.GetRootGameObjects())
                if (rootGameObject.GetComponent<Canvas>() == null)
                {
                    Destroy(rootGameObject);
                }
                else
                {
                    var canvasRect = rootGameObject.GetComponent<RectTransform>();
                    rootGameObject.transform.SetParent(m_FeatureViewport.transform);
                    canvasRect.anchorMin = new Vector2(0, 0);
                    canvasRect.anchorMax = new Vector2(1, 1);

                    canvasRect.transform.localScale = Vector3.one;
                    canvasRect.anchoredPosition = Vector2.zero;

                    canvasRect.offsetMin = Vector2.zero;
                    canvasRect.offsetMax = Vector2.zero;
                }
        }

        void LoadScene(string sceneName)
        {
            if (sceneName.Equals(m_CurrentlyFeaturedScene.name)) return;

            m_FeatureViewport.transform.Clear();
            if (m_CurrentlyFeaturedScene.isLoaded) SceneManager.UnloadSceneAsync(m_CurrentlyFeaturedScene);

            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }
}
