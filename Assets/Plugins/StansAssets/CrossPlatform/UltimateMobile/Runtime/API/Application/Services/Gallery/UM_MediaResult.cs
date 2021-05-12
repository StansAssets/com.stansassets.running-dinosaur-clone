using SA.Foundation.Templates;

namespace SA.CrossPlatform.App
{
    /// <inheritdoc />
    /// <summary>
    /// Image result object
    /// </summary>
    public class UM_MediaResult : SA_Result
    {
        public UM_MediaResult(UM_Media media)
        {
            Media = media;
            if (Media == null) m_error = new SA_Error(100, "No Media");
        }

        public UM_MediaResult(SA_Error error)
            : base(error) { }

        /// <summary>
        /// Contains <see cref="UM_Media"/> if result <see cref="IsSucceeded"/>,
        /// Otherwise the object is <c>null</c>.
        /// </summary>
        /// <value>The image.</value>
        public UM_Media Media { get; }
    }
}
