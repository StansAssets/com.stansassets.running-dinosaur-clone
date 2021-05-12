using SA.CrossPlatform.Analytics;
using UnityEngine;

namespace StansAssets.ProjectSample.InApps
{
    public static class RewardManager
    {
        public static void AddConsumable()
        {
            int amount = Consumables + 1;
            PlayerPrefs.SetInt(InApps.ConsumableProductId, amount);
            UM_AnalyticsService.Client.Transaction(InApps.ConsumableProductId, amount, InApps.ConsumableProductId);
        }

        public static void ActivatePremium()
        {
            PlayerPrefs.SetInt(InApps.PremiumProductId, 1);
        }

        public static int Consumables => PlayerPrefs.HasKey(InApps.ConsumableProductId) 
                                             ? PlayerPrefs.GetInt(InApps.ConsumableProductId) : 
                                             0;

        public static bool HasPremium => PlayerPrefs.HasKey(InApps.PremiumProductId);

        public static void Reset()
        {
            PlayerPrefs.DeleteKey(InApps.ConsumableProductId);
            PlayerPrefs.DeleteKey(InApps.PremiumProductId);
        }
    }
}
