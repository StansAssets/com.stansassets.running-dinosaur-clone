using SA.CrossPlatform.UI;
using SA.iOS.Foundation;
using UnityEngine;
using UnityEngine.UI;

public class UM_CloudExampleController : MonoBehaviour
{
    [SerializeField]
    Button m_ButtonSet = null;
    [SerializeField]
    Button m_ButtonGet = null;

    [SerializeField]
    InputField m_DataInputField = null;

    [SerializeField]
    Button m_ButtonSynchronize = null;

    const string k_DataKey = "Test_Data_Key";

    void Start()
    {
        m_ButtonSynchronize.onClick.AddListener(() =>
        {
            var result = ISN_NSUbiquitousKeyValueStore.Synchronize();
            UM_DialogsUtility.ShowMessage("Synchronize", $"Result: {result}");
        });

        m_ButtonGet.onClick.AddListener(() =>
        {
            var val = ISN_NSUbiquitousKeyValueStore.KeyValueStoreObjectForKey(k_DataKey);
            UM_DialogsUtility.ShowNotification($"Value from cloud: {val.StringValue}");
        });

        m_ButtonSet.onClick.AddListener(() =>
        {
            ISN_NSUbiquitousKeyValueStore.SetString(k_DataKey, m_DataInputField.text);
            UM_DialogsUtility.ShowNotification($"Set to Cloud: {m_DataInputField.text}");
        });

        // Let's assume we store player current level in the cloud and what to apply to higher-level wins strategy.
        ISN_NSUbiquitousKeyValueStore.StoreDidChangeExternallyNotification.AddListener((e) =>
        {
            // You may defined different strategies for change different reasons
            Debug.Log($"Reason: {e.Reason}");

            // Let's check which data is changed
            foreach (var val in e.UpdatedData)
            {
                // For example, let' say we store player level using "level" key
                if (val.Key.Equals("level"))
                {
                    // our level is represented as an int value:
                    var level = val.IntValue;

                    // Now I want to make sure I will take higher level value
                    var currentLevel = GteCurrentPlayerLevel();

                    // Nothing to update
                    if(level == currentLevel)
                        continue;

                    if (level > currentLevel)
                    {
                        // cloud level is higher. Let's update current player level
                        SetPlayerLevel(level);
                    }
                    else
                    {
                        // Current player has higher level, let's updated cloud value to match.
                        ISN_NSUbiquitousKeyValueStore.SetInt("level", currentLevel);
                    }
                }
            }
        });
    }

    int GteCurrentPlayerLevel() => 1;
    void SetPlayerLevel(int level) { }
}
