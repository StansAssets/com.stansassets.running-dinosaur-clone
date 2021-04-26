using SA.iOS.Utilities;

namespace SA.iOS.AuthenticationServices
{
    /// <summary>
    /// A mechanism for generating requests to authenticate users based on their Apple ID.
    /// </summary>
    public class ISN_ASAuthorizationAppleIDProvider : ISN_NativeObject
    {
        public ISN_ASAuthorizationAppleIDProvider()
            : base(ISN_AuthenticationServicesLib._ISN_ASAuthorizationAppleIDProvider_init()) { }

        /// <summary>
        /// Creates a new Apple ID authorization request.
        /// </summary>
        /// <returns>An Apple ID authorization request that you can configure and execute.</returns>
        public ISN_IASAuthorizationAppleIDRequest CreateRequest()
        {
            var requestHash = ISN_AuthenticationServicesLib._ISN_ASAuthorizationAppleIDProvider_createRequest(NativeHashCode);
            return new ISN_ASAuthorizationSingleSignOnRequest(requestHash);
        }
    }
}
