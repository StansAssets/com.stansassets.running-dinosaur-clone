using SA.Foundation.Templates;

namespace SA.CrossPlatform.UI
{
    /// <summary>
    /// Object that contains a result of picked value from wheel picker
    /// using the <see cref="UM_WheelPickerDialog"/>
    /// </summary>
    public class UM_WheelPickerResult : SA_Result
    {
        
        /// <summary>
        /// Picker state, see <see cref="UM_WheelPickerState"/> for more info.
        /// </summary>
        public UM_WheelPickerState State { get; }
        
        /// <summary>
        /// User picker value.
        /// </summary>
        public string Value { get; }

        internal UM_WheelPickerResult(string value, UM_WheelPickerState state)
        {
            Value = value;
            State = state;
        }

        internal UM_WheelPickerResult(SA_Error error)
            : base(error) { }

      
    }
}
