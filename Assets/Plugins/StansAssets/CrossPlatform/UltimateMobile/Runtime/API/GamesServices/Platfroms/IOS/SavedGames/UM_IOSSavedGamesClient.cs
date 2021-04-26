using System;
using System.Collections.Generic;
using System.Linq;
using SA.iOS.GameKit;
using SA.Foundation.Templates;
using StansAssets.Foundation;

namespace SA.CrossPlatform.GameServices
{
    /// <summary>
    /// A client to interact with saved games functionality.
    /// </summary>
    class UM_IOSSavedGamesClient : UM_AbstractSavedGamesClient, UM_iSavedGamesClient
    {
        //Automatic conflict resolution
        public UM_IOSSavedGamesClient()
        {
            ISN_GKLocalPlayerListener.HasConflictingSavedGames.AddListener(HasConflictingSavedGames);
        }

        void HasConflictingSavedGames(ISN_GKSavedGameFetchResult result)
        {
            ISN_GKSavedGame resultGame = null;
            var conflictedSavedGamesIds = new List<string>();
            foreach (var game in result.SavedGames)
            {
                conflictedSavedGamesIds.Add(game.Id);

                if (resultGame == null)
                {
                    resultGame = game;
                }
                else
                {
                    var gameUnixTime = TimeUtility.ToUnixTime(game.ModificationDate);
                    var currentResultTime = TimeUtility.ToUnixTime(resultGame.ModificationDate);
                    if (gameUnixTime > currentResultTime) resultGame = game;
                }
            }

            ISN_GKLocalPlayer.LoadGameData(resultGame, (dataLoadResult) =>
            {
                if (dataLoadResult.IsSucceeded)
                    ISN_GKLocalPlayer.ResolveConflictingSavedGames(conflictedSavedGamesIds, dataLoadResult.BytesArrayData, (resResult) => { });
            });
        }

        public void FetchSavedGames(Action<UM_SavedGamesMetadataResult> callback)
        {
            ISN_GKLocalPlayer.FetchSavedGames((result) =>
            {
                UM_SavedGamesMetadataResult loadResult;

                if (result.IsSucceeded)
                {
                    loadResult = new UM_SavedGamesMetadataResult();
                    foreach (var game in result.SavedGames)
                    {
                        var isnMeta = new UM_IOSSavedGameMetadata(game);
                        loadResult.AddMetadata(isnMeta);
                    }
                }
                else
                {
                    loadResult = new UM_SavedGamesMetadataResult(result.Error);
                }

                callback.Invoke(loadResult);
            });
        }

        public void SaveGame(string name, byte[] data, Action<SA_Result> callback)
        {
            ISN_GKLocalPlayer.SavedGame(name, data, (result) =>
            {
                ReportGameSave(name, result);
                callback.Invoke(result);
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
            LoadFromGameKitData(game, false, callback);
        }

        public void LoadGameWithMeta(UM_iSavedGameMetadata game, Action<UM_SavedGameDataLoadResult> callback)
        {
            LoadFromGameKitData(game, true, callback);
        }

        public void LoadFromGameKitData(UM_iSavedGameMetadata game, bool parseMeta, Action<UM_SavedGameDataLoadResult> callback)
        {
            var isnMeta = (UM_IOSSavedGameMetadata)game;
            ISN_GKLocalPlayer.LoadGameData(isnMeta.NativeMeta, (result) =>
            {
                UM_SavedGameDataLoadResult loadResult;
                if (result.IsSucceeded)
                {
                    if (parseMeta)
                    {
                        var meta = new UM_SaveInfo();
                        meta.SetProgressValue(BitConverter.ToInt64(result.BytesArrayData, 0));
                        meta.SetPlayedTimeMillis(BitConverter.ToInt64(result.BytesArrayData, 8));

                        var userData = new byte[result.BytesArrayData.Length - 16];
                        Array.Copy(result.BytesArrayData, 16, userData, 0, userData.Length);
                        loadResult = new UM_SavedGameDataLoadResult(userData, meta);
                    }
                    else
                    {
                        loadResult = new UM_SavedGameDataLoadResult(result.BytesArrayData, new UM_SaveInfo());
                    }
                }
                else
                {
                    loadResult = new UM_SavedGameDataLoadResult(result.Error);
                }

                callback.Invoke(loadResult);
            });
        }

        public void Delete(UM_iSavedGameMetadata game, Action<SA_Result> callback)
        {
            var isnMeta = (UM_IOSSavedGameMetadata)game;
            ISN_GKLocalPlayer.DeleteSavedGame(isnMeta.NativeMeta, callback);
        }
    }
}
