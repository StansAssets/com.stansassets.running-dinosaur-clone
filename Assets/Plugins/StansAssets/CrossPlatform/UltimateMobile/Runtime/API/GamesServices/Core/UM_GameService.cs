using System;
using System.Collections.Generic;
using UnityEngine;

namespace SA.CrossPlatform.GameServices
{

   
    
    
    
    
    /// <summary>
    /// Main entry point for the Game Services APIs.
    /// This class provides APIs and interfaces to access the game services functionality.
    /// </summary>
    public static class UM_GameService
    {
        static UM_iSignInClient s_SignInClient = null;
        static UM_iAchievementsClient s_Achievements = null;
        static UM_iLeaderboardsClient s_Leaderboards = null;
        static UM_iSavedGamesClient s_SavedGames = null;

        /// <summary>
        /// say hello
        /// </summary>
        public static void HelloWorld()
        {
            Debug.Log("HelloWorld");
        }

        /// <summary>
        /// Returns a new instance of <see cref="UM_iSignInClient"/>
        /// </summary>
        public static UM_iSignInClient SignInClient
        {
            get
            {
                if (s_SignInClient == null)
                    switch (Application.platform)
                    {
                        case RuntimePlatform.Android:
                            s_SignInClient = new UM_AndroidSignInClient();
                            break;
                        case RuntimePlatform.tvOS:
                        case RuntimePlatform.OSXPlayer:
                        case RuntimePlatform.IPhonePlayer:
                            s_SignInClient = new UM_IOSSignInClient();
                            break;
                        default:
                            s_SignInClient = new UM_EditorSignInClient();
                            break;
                    }

                return s_SignInClient;
            }
        }

        /// <summary>
        /// Returns a new instance of <see cref="UM_iSignInClient"/>
        /// </summary>
        public static UM_iAchievementsClient AchievementsClient
        {
            get
            {
                if (s_Achievements == null)
                    switch (Application.platform)
                    {
                        case RuntimePlatform.Android:
                            s_Achievements = new UM_AndroidAchievementsClient();
                            break;
                        case RuntimePlatform.tvOS:
                        case RuntimePlatform.OSXPlayer:
                        case RuntimePlatform.IPhonePlayer:
                            s_Achievements = new UM_IOSAchievementsClient();
                            break;
                        default:
                            s_Achievements = new UM_EditorAchievementsClient();
                            break;
                    }

                return s_Achievements;
            }
        }

        /// <summary>
        /// Returns a new instance of <see cref="UM_iSignInClient"/>
        /// </summary>
        public static UM_iLeaderboardsClient LeaderboardsClient
        {
            get
            {
                if (s_Leaderboards == null)
                    switch (Application.platform)
                    {
                        case RuntimePlatform.Android:
                            s_Leaderboards = new UM_AndroidLeaderboardsClient();
                            break;
                        case RuntimePlatform.tvOS:
                        case RuntimePlatform.OSXPlayer:
                        case RuntimePlatform.IPhonePlayer:
                            s_Leaderboards = new UM_IOSLeaderboardsClient();
                            break;
                        default:
                            s_Leaderboards = new UM_EditorLeaderboardsClient();
                            break;
                    }

                return s_Leaderboards;
            }
        }

        /// <summary>
        /// Returns a new instance of <see cref="UM_iSavedGamesClient"/>
        /// </summary>
        public static UM_iSavedGamesClient SavedGamesClient
        {
            get
            {
                if (s_SavedGames == null)
                    switch (Application.platform)
                    {
                        case RuntimePlatform.Android:
                            s_SavedGames = new UM_AndroidSavedGamesClient();
                            break;

                        //not supported by tv OS
                        case RuntimePlatform.OSXPlayer:
                        case RuntimePlatform.IPhonePlayer:
                            s_SavedGames = new UM_IOSSavedGamesClient();
                            break;
                        default:
                            s_SavedGames = new UM_EditorSavedGamesClient();
                            break;
                    }

                return s_SavedGames;
            }
        }
    }
}
