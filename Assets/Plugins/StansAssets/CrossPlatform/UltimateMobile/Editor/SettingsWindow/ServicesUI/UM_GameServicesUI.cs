using UnityEngine;
using UnityEditor;
using SA.iOS;
using SA.Android;
using SA.Android.Editor;
using SA.CrossPlatform.GameServices;
using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_GameServicesUI : UM_ServiceSettingsUI
    {
        class GameKitSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<ISN_GameKitUI>();

            public override bool IsEnabled => ISN_Preprocessor.GetResolver<ISN_GameKitResolver>().IsSettingsEnabled;
        }

        class GooglePlaySettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<AN_GooglePlayFeaturesUI>();

            public override bool IsEnabled => AN_Preprocessor.GetResolver<AN_GooglePlayResolver>().IsSettingsEnabled;
        }

        public override void OnLayoutEnable()
        {
            base.OnLayoutEnable();
            AddPlatform(UM_UIPlatform.IOS, new GameKitSettings());
            AddPlatform(UM_UIPlatform.Android, new GooglePlaySettings());

            AddFeatureUrl("Getting Started", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Getting-Started-(Game-Services)");

            AddFeatureUrl("Sing In", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Authentication#sing-in");
            AddFeatureUrl("Sing Out", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Authentication#sing-out");
            AddFeatureUrl("Player Avatar Image", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Authentication#player-avatar-image");
            AddFeatureUrl("Auth State", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Authentication#check-auth-state");

            AddFeatureUrl("Achievements", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Achievements");
            AddFeatureUrl("Leaderboards", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Leaderboards");
            AddFeatureUrl("Saved Games", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Saved-Games");

            AddFeatureUrl("Editor API Emulation", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Editor-API-Emulation");
        }

        public override string Title => "Game Services";

        protected override string Description =>
            "Enables your users to track their best scores on a leaderboard, " +
            "compare their achievements, save game progress and more.";

        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_game_icon.png");

        protected override void OnServiceUI()
        {
            Settings();
            EditorAPIEmulation();
        }

        static readonly GUIContent k_AndroidSavedGamesContent = new GUIContent("Saved Games API [?]:", "Without Games API enabled only basic sing in flow can be used");
        static readonly GUIContent k_AndroidRequestProfileContent = new GUIContent("Request Profile [?]:", "Request User Profile will be added to sing-in builder");
        static readonly GUIContent k_AndroidRequestEmailContent = new GUIContent("Request Email [?]:", "Request User Email will be added to sing-in builder");
        static readonly GUIContent k_AndroidRequestServerAuthCodeContent = new GUIContent("Request Server Auth Code[?]:", "Request  Server Auth Code will be added to sing-in builder");

        static readonly GUIContent s_IOSSavingAGameContent = new GUIContent("Saving A Game[?]:", "The saves API will allow you to provide your player an ability to save & load game progress at any time.");

        void Settings()
        {
            using (new SA_WindowBlockWithSpace(new GUIContent("Settings")))
            {
                using (new SA_H2WindowBlockWithSpace(new GUIContent("ANDROID"))) {
                    AN_Settings.Instance.GooglePlayGamesAPI = IMGUILayout.ToggleFiled(AN_GooglePlayFeaturesUI.GamesLabelContent, AN_Settings.Instance.GooglePlayGamesAPI, IMGUIToggleStyle.ToggleType.EnabledDisabled);

                    UM_Settings.Instance.AndroidRequestEmail = IMGUILayout.ToggleFiled(k_AndroidRequestEmailContent, UM_Settings.Instance.AndroidRequestEmail, IMGUIToggleStyle.ToggleType.EnabledDisabled);
                    UM_Settings.Instance.AndroidRequestProfile = IMGUILayout.ToggleFiled(k_AndroidRequestProfileContent, UM_Settings.Instance.AndroidRequestProfile, IMGUIToggleStyle.ToggleType.EnabledDisabled);

                    UM_Settings.Instance.AndroidSavedGamesEnabled = IMGUILayout.ToggleFiled(k_AndroidSavedGamesContent, UM_Settings.Instance.AndroidSavedGamesEnabled, IMGUIToggleStyle.ToggleType.EnabledDisabled);
                    UM_Settings.Instance.AndroidRequestServerAuthCode = IMGUILayout.ToggleFiled(k_AndroidRequestServerAuthCodeContent, UM_Settings.Instance.AndroidRequestServerAuthCode, IMGUIToggleStyle.ToggleType.EnabledDisabled);

                    if (UM_Settings.Instance.AndroidRequestServerAuthCode) UM_Settings.Instance.AndroidGMSServerId = IMGUILayout.TextField("Server Id", UM_Settings.Instance.AndroidGMSServerId);
                }

                using (new SA_H2WindowBlockWithSpace(new GUIContent("iOS")))
                {
                    ISN_Settings.Instance.SavingAGame = IMGUILayout.ToggleFiled(s_IOSSavingAGameContent, ISN_Settings.Instance.SavingAGame, IMGUIToggleStyle.ToggleType.EnabledDisabled);
                }

                using (new SA_H2WindowBlockWithSpace(new GUIContent("EDITOR")))
                {
                    EditorGUILayout.HelpBox("Platform does not require any additional settings.", MessageType.Info);
                }
            }
        }

        static readonly GUIContent s_PlayerId = new GUIContent("Id[?]:", "Player identifier.");
        static readonly GUIContent s_PlayerAlias = new GUIContent("Alias[?]:", "Player Alias. " +
            "Typically, you never display the alias string directly in your user interface. " +
            "Instead DisplayName property.");
        static readonly GUIContent s_PlayerDisplayName = new GUIContent("DisplayName[?]:", "Player display name.");
        static readonly GUIContent s_PlayerAvatar = new GUIContent("Avatar Image[?]:", "The image will be used as signed player " +
            "avatar while you testing in editor mode.");

        void EditorAPIEmulation()
        {
            using (new SA_WindowBlockWithSpace(new GUIContent("Editor API Emulation")))
            {
                using (new SA_H2WindowBlockWithSpace(new GUIContent("PLAYER")))
                {
                    var player = UM_Settings.Instance.GSEditorPlayer;
                    player.Id = IMGUILayout.TextField(s_PlayerId, player.Id);
                    player.Alias = IMGUILayout.TextField(s_PlayerAlias, player.Alias);
                    player.DisplayName = IMGUILayout.TextField(s_PlayerDisplayName, player.DisplayName);

                    using (new IMGUIBeginHorizontal())
                    {
                        EditorGUILayout.LabelField(s_PlayerAvatar);
                        player.Avatar = (Texture2D)EditorGUILayout.ObjectField(player.Avatar, typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                    }
                }

                using (new SA_H2WindowBlockWithSpace(new GUIContent("ACHIEVEMENTS")))
                {
                    EditorGUILayout.HelpBox("Achievements Editor list is bound with iOS Achivement's list.", MessageType.Info);
                    ISN_GameKitUI.DrawAchievementsList();
                }

                using (new SA_H2WindowBlockWithSpace(new GUIContent("LEADERBOARDS")))
                {
                    EditorGUILayout.HelpBox("This data will only be used for an editor API emulation.", MessageType.Info);
                    DrawLeaderboardsList();
                }
            }
        }

        static readonly GUIContent s_LeaderboardIdDLabel = new GUIContent("Leaderboard Id[?]:", "A unique identifier of this leaderboard.");
        static readonly GUIContent s_LeaderboardNameLabel = new GUIContent("Leaderboard Title[?]:", "The Title of the leaderboard.");

        void DrawLeaderboardsList()
        {
            SA_EditorGUILayout.ReorderablList(UM_Settings.Instance.GSLeaderboards, GetLeaderboardDisplayName, DrawLeaderboardContent, () =>
            {
                UM_Settings.Instance.GSLeaderboards.Add(new UM_LeaderboardMeta("my.new.leaderboard.id", "New Leaderboard"));
            });
        }

        string GetLeaderboardDisplayName(UM_LeaderboardMeta leaderboard)
        {
            return leaderboard.Title + "(" + leaderboard.Identifier + ")";
        }

        static void DrawLeaderboardContent(UM_LeaderboardMeta leaderboard)
        {
            leaderboard.Identifier = IMGUILayout.TextField(s_LeaderboardIdDLabel, leaderboard.Identifier);
            leaderboard.Title = IMGUILayout.TextField(s_LeaderboardNameLabel, leaderboard.Title);
        }
    }
}
