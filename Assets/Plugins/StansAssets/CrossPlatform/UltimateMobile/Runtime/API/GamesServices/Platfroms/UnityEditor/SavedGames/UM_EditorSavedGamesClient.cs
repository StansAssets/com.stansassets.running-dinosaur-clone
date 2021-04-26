using System;
using System.Collections.Generic;
using System.Linq;
using SA.Foundation.Templates;
using UnityEngine;
using StansAssets.Foundation.Async;

namespace SA.CrossPlatform.GameServices
{
    /// <summary>
    /// A client to interact with saved games functionality.
    /// </summary>
    class UM_EditorSavedGamesClient : UM_iSavedGamesClient
    {
        const string k_EditorSavesKey = "UM_SAVED_GAMES_DATA";

        public void FetchSavedGames(Action<UM_SavedGamesMetadataResult> callback)
        {
            var loadResult = new UM_SavedGamesMetadataResult();
            var editorGamesList = LoadSavesList();
            foreach (var game in editorGamesList.Saves) loadResult.AddMetadata(game);

            CoroutineUtility.WaitForSeconds(1.5f, () =>
            {
                callback.Invoke(loadResult);
            });
        }

        public void SaveGame(string name, byte[] data, Action<SA_Result> callback)
        {
            var editorGamesList = LoadSavesList();
            var game = editorGamesList.GetByName(name);
            if (game == null)
            {
                game = new EditorSavedGame();
                game.Name = name;
                editorGamesList.Saves.Add(game);
            }

            game.GameData = System.Text.Encoding.UTF8.GetString(data);
            EditorSaveGames(editorGamesList);

            CoroutineUtility.WaitForSeconds(1.5f, () =>
            {
                callback.Invoke(new SA_Result());
            });
        }

        public void SaveGameWithMeta(string name, byte[] data, UM_SaveInfo meta, Action<SA_Result> callback)
        {
            var extendedData = Combine(BitConverter.GetBytes(meta.ProgressValue),
                BitConverter.GetBytes(meta.PlayedTime),
                data);

            SaveGame(name, extendedData, callback);
        }

        byte[] Combine(params byte[][] arrays)
        {
            var rv = new byte[arrays.Sum(a => a.Length)];
            var offset = 0;
            foreach (var array in arrays)
            {
                Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }

            return rv;
        }

        public void LoadGameData(UM_iSavedGameMetadata game, Action<UM_SavedGameDataLoadResult> callback)
        {
            LoadFromPlayerPrefs(game, false, callback);
        }

        public void LoadGameWithMeta(UM_iSavedGameMetadata game, Action<UM_SavedGameDataLoadResult> callback)
        {
            LoadFromPlayerPrefs(game, true, callback);
        }

        public void LoadFromPlayerPrefs(UM_iSavedGameMetadata game, bool parseMeta, Action<UM_SavedGameDataLoadResult> callback)
        {
            var editorGamesList = LoadSavesList();
            var editorGame = editorGamesList.GetByName(game.Name);

            UM_SavedGameDataLoadResult loadResult;
            if (editorGame != null)
            {
                var bytesArrayData = System.Text.Encoding.UTF8.GetBytes(editorGame.GameData);
                if (parseMeta)
                {
                    var meta = new UM_SaveInfo();
                    meta.SetProgressValue(BitConverter.ToInt64(bytesArrayData, 0));
                    meta.SetPlayedTimeMillis(BitConverter.ToInt64(bytesArrayData, 8));

                    var userData = new byte[bytesArrayData.Length - 16];
                    Array.Copy(bytesArrayData, 16, userData, 0, userData.Length);
                    loadResult = new UM_SavedGameDataLoadResult(userData, meta);
                }
                else
                {
                    loadResult = new UM_SavedGameDataLoadResult(bytesArrayData, new UM_SaveInfo());
                }
            }
            else
            {
                var error = new SA_Error(100, "Saved game with name: " + game.Name + " wasn't found");
                loadResult = new UM_SavedGameDataLoadResult(error);
            }

            EditorSaveGames(editorGamesList);

            CoroutineUtility.WaitForSeconds(1.5f, () =>
            {
                callback.Invoke(loadResult);
            });
        }

        public void Delete(UM_iSavedGameMetadata game, Action<SA_Result> callback)
        {
            var editorGamesList = LoadSavesList();
            var editorGame = editorGamesList.GetByName(game.Name);

            if (editorGame != null)
            {
                editorGamesList.Saves.Remove(editorGame);
                EditorSaveGames(editorGamesList);
            }

            CoroutineUtility.WaitForSeconds(1.5f, () =>
            {
                callback.Invoke(new SA_Result());
            });
        }

        EditorSavedGamesList LoadSavesList()
        {
            if (PlayerPrefs.HasKey(k_EditorSavesKey))
            {
                var json = PlayerPrefs.GetString(k_EditorSavesKey);
                return JsonUtility.FromJson<EditorSavedGamesList>(json);
            }

            return new EditorSavedGamesList();
        }

        void EditorSaveGames(EditorSavedGamesList list)
        {
            var json = JsonUtility.ToJson(list);
            PlayerPrefs.SetString(k_EditorSavesKey, json);
        }

        [Serializable]
        class EditorSavedGame : UM_iSavedGameMetadata
        {
            [SerializeField]
            string m_name = null;
            [SerializeField]
            public string GameData = null;

            public string DeviceName => "Editor";

            public string Name
            {
                get => m_name;
                set => m_name = value;
            }
        }

        [Serializable]
        class EditorSavedGamesList
        {
            [SerializeField]
            List<EditorSavedGame> m_saves = new List<EditorSavedGame>();
            public List<EditorSavedGame> Saves => m_saves;

            public EditorSavedGame GetByName(string name)
            {
                foreach (var game in m_saves)
                    if (game.Name.Equals(name))
                        return game;

                return null;
            }
        }
    }
}
