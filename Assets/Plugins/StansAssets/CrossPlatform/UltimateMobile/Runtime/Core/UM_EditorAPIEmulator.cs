using System;
using StansAssets.Foundation.Async;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace SA.CrossPlatform
{
    static class UM_EditorAPIEmulator
    {
        public static void SetString(string key, string data)
        {
#if UNITY_EDITOR
            key = WrapKey(key);
            EditorPrefs.SetString(key, data);
#endif
        }

        public static string GetString(string key)
        {
#if UNITY_EDITOR
            key = WrapKey(key);
            return EditorPrefs.GetString(key);

#else
            return string.Empty;
#endif
        }

        public static void SetFloat(string key, float data)
        {
#if UNITY_EDITOR
            key = WrapKey(key);
            EditorPrefs.SetFloat(key, data);
#endif
        }

        public static float GetFloat(string key)
        {
#if UNITY_EDITOR
            key = WrapKey(key);
            return EditorPrefs.GetFloat(key);
#else
            return 0f;
#endif
        }

        public static bool HasKey(string key)
        {
#if UNITY_EDITOR
            key = WrapKey(key);
            return EditorPrefs.HasKey(key);
#else
            return false;
#endif
        }

        public static void WaitForNetwork(Action action)
        {
            var seconds = UnityEngine.Random.Range(0.5f, 3f);
            CoroutineUtility.WaitForSeconds(seconds, action);
        }

        static string WrapKey(string key)
        {
            return "um_api_editor_emulato_" + key;
            ;
        }
    }
}
