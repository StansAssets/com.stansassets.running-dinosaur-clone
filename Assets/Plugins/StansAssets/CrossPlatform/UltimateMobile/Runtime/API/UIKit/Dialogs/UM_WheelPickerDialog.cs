using UnityEngine;
using System;
using System.Collections.Generic;
using SA.Android.App;
using SA.Foundation.Templates;
using SA.iOS.UIKit;

namespace SA.CrossPlatform.UI
{
    /// <summary>
    /// A simple dialog containing an Wheel picker
    /// </summary>
    public class UM_WheelPickerDialog
    {
        /// <summary>
        /// Picker values.
        /// </summary>
        public List<string> Values { get; }

        /// <summary>
        /// Default value index. `0` by default.
        /// </summary>
        public int DefaultValueIndex { get; }

        /// <summary>
        /// Create a new wheel picker dialog for the specified values.
        /// </summary>
        /// <param name="values">list of the elements to choose from.</param>
        /// <param name="defaultValueIndex">Default value index.</param>
        public UM_WheelPickerDialog(List<string> values, int defaultValueIndex = 0)
        {
            Values = values;
            DefaultValueIndex = defaultValueIndex;
        }

        /// <summary>
        /// Start of the dialog display in on screen.
        /// </summary>
        public void Show(Action<UM_WheelPickerResult> callback)
        {
            if (Application.isEditor)
            {
#if UNITY_EDITOR
                UnityEditor.EditorUtility.DisplayDialog(
                    "Not supported",
                    "The wheel picker view emulation is not supported inside the Editor.\n" +
                    "First value of the list will be returned as dialog result.",
                    "Okay");
                UM_WheelPickerResult result;
                if (Values != null && Values.Count > 0)
                    result = new UM_WheelPickerResult(Values[0], UM_WheelPickerState.Done);
                else
                    result = new UM_WheelPickerResult(new SA_Error(1, "No values provided"));
                callback.Invoke(result);
#endif
            }
            else
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        var picker = new AN_WheelPickerDialog(Values, DefaultValueIndex);
                        picker.Show(pickerResult =>
                        {
                            UM_WheelPickerResult result;
                            if (pickerResult.HasError)
                            {
                                result = new UM_WheelPickerResult(pickerResult.Error);
                            }
                            else
                            {
                                var value = pickerResult.Value;
                                var state = (UM_WheelPickerState)pickerResult.State;
                                result = new UM_WheelPickerResult(value, state);
                            }

                            callback.Invoke(result);
                        });
                        break;
                    case RuntimePlatform.IPhonePlayer:
                        var pickerController = new ISN_UIWheelPickerController(Values, DefaultValueIndex);
                        pickerController.Show(pickerResult =>
                        {
                            UM_WheelPickerResult result;
                            if (pickerResult.HasError)
                            {
                                result = new UM_WheelPickerResult(pickerResult.Error);
                            }
                            else
                            {
                                var value = pickerResult.Value;
                                var state = (UM_WheelPickerState)pickerResult.State;
                                result = new UM_WheelPickerResult(value, state);
                            }

                            callback.Invoke(result);
                        });
                        break;
                }
            }
        }
    }
}
