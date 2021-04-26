using System;
using SA.Foundation.Templates;
using SA.Foundation.Events;
using SA.CrossPlatform.Analytics;

namespace SA.CrossPlatform.GameServices
{
    abstract class UM_AbstractSignInClient
    {
        UM_PlayerInfo m_CurrentPlayerInfo = new UM_PlayerInfo(UM_PlayerState.SignedOut, null);
        readonly SA_Event m_OnPlayerChanged = new SA_Event();
        readonly SA_Event<SA_Result> m_SingInCallback = new SA_Event<SA_Result>();
        bool m_SignInFlowInProgress;

        //--------------------------------------
        // Abstract Methods
        //--------------------------------------

        protected abstract void StartSingInFlow(Action<SA_Result> callback);

        //--------------------------------------
        // Public Methods
        //--------------------------------------

        public void SingIn(Action<SA_Result> callback) => SingIn(callback);
        public void SignIn(Action<SA_Result> callback)
        {
            m_SingInCallback.AddListener(callback);

            //Preventing double sing in
            if (m_SignInFlowInProgress) return;
            m_SignInFlowInProgress = true;
            StartSingInFlow(result =>
            {
                m_SignInFlowInProgress = false;
                m_SingInCallback.Invoke(result);
                m_SingInCallback.RemoveAllListeners();
            });
        }

        //--------------------------------------
        // Get / Set
        //--------------------------------------

        public SA_iEvent OnPlayerUpdated => m_OnPlayerChanged;

        public UM_PlayerInfo PlayerInfo => m_CurrentPlayerInfo;

        public bool IsSignInFlowInProgress => m_SignInFlowInProgress;

        //--------------------------------------
        // Protected Methods
        //--------------------------------------

        protected void UpdateSignedPlayer(UM_PlayerInfo info)
        {
            m_CurrentPlayerInfo = info;
            m_OnPlayerChanged.Invoke();
            UM_AnalyticsInternal.OnPlayerUpdated(info);
        }
    }
}
