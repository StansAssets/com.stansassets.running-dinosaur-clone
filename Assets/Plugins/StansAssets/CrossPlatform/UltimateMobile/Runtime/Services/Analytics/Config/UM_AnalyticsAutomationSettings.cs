using System;

namespace SA.CrossPlatform.Analytics
{
    [Serializable]
    class UM_AnalyticsAutomationSettings
    {
        // General
        public bool Exceptions = true;

        // Games Services
        public bool Scores = true;
        public bool Achievements = true;
        public bool GameSaves = true;
        public bool PlayerIdTracking = true;

        // In-Apps
        public bool SuccessfulTransactions = true;
        public bool FailedTransactions = true;
        public bool RestoreRequests = true;
    }
}
