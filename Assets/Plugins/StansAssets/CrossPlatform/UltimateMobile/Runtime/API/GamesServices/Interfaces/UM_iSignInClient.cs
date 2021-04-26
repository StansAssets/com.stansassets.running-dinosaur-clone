using System;
using SA.Foundation.Events;
using SA.Foundation.Templates;

namespace SA.CrossPlatform.GameServices
{
    /// <summary>
    /// A client to interact with sing-in flow.
    /// </summary>
    public interface UM_iSignInClient
    {
        /// <summary>
        /// Starts Sing In flow
        /// </summary>
        /// <param name="callback">Operation async callback</param>
        void SignIn(Action<SA_Result> callback);

        [Obsolete("SingIn is Deprecated, please use SignIn instead.")]
        void SingIn(Action<SA_Result> callback);

        /// <summary>
        /// Starts Sing Out flow
        /// </summary>
        /// <param name="callback">Operation async callback</param>
        void SignOut(Action<SA_Result> callback);

        /// <summary>
        /// Fired when player info is changed.
        /// Player Singed in / Signed out / Changed Account
        /// </summary>
        SA_iEvent OnPlayerUpdated { get; }

        /// <summary>
        /// Current Player info
        /// Use this property to find out current <see cref="UM_PlayerState"/>
        /// and get singed <see cref="UM_iPlayer"/> object
        /// </summary>
        UM_PlayerInfo PlayerInfo { get; }
    }
}
