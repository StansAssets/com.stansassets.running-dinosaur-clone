using System;
using SA.Foundation.Templates;

namespace SA.CrossPlatform.UI
{
    /// <summary>
    /// Object that contains a picked dated result from <see cref="UM_DatePickerDialog"/>
    /// </summary>
    public class UN_DatePickerResult : SA_Result
    {
        internal UN_DatePickerResult(DateTime date)
        {
            Date = date;
        }

        internal UN_DatePickerResult(SA_Error error)
            : base(error) { }

        /// <summary>
        /// User picked date.
        /// </summary>
        public DateTime Date { get; }
    }
}
