using System;
using System.ComponentModel;
using Bhanu;
using UnityEngine;
using VivoxUnity;

namespace BhanuAssets.Scripts
{
    public class LoginCredentials : MonoBehaviour
    {
        private AsyncCallback _asyncLoginCallback;
        private ILoginSession _iLogInSession;
        private string _domain = "mtu1xp.vivox.com";
        private string _tokenIssuer = "57729-mymul-61567-test";
        private string _tokenKey = "opmzsgf5dFJ616dxMGA6i4fujeEnOhXD";
        private TimeSpan _timeSpan = new TimeSpan(90);
        private Uri _server = new Uri("https://unity.vivox.com/appconfig/57729-mymul-61567-test");
        private VivoxUnity.Client _vivoxClient;
        
        private void Awake()
        {
            _vivoxClient = new Client();
            _vivoxClient.Uninitialize();
            _vivoxClient.Initialize();
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            LoginPlayer();
        }

        private void OnApplicationQuit()
        {
            _vivoxClient.Uninitialize();
        }

        public void BindLoginCallbackListeners(bool bind , ILoginSession iLoginSession)
        {
            if(bind)
            {
                iLoginSession.PropertyChanged += LoginStatus;   
            }
            else
            {
                iLoginSession.PropertyChanged -= LoginStatus;
            }
        }

        public void Login(string username)
        {
            AccountId accountID = new AccountId(_tokenIssuer , username , _domain);
            
            _iLogInSession = _vivoxClient.GetLoginSession(accountID);

            BindLoginCallbackListeners(true , _iLogInSession);
            
            _iLogInSession.BeginLogin(_server , _iLogInSession.GetLoginToken(_tokenKey , _timeSpan) , ar =>
            {
                try
                {
                    _iLogInSession.EndLogin(ar);
                }
                catch(Exception e)
                {
                    BindLoginCallbackListeners(false , _iLogInSession);
                    LogMessages.ErrorMessage(e.Message);
                    throw;
                }
            });
        }

        public void LoginPlayer()
        {
            Login("Bhanu");
        }

        public void LoginStatus(object sender , PropertyChangedEventArgs loginStatusArgs)
        {
            ILoginSession loginSessionSource = (ILoginSession)sender;

            switch(loginSessionSource.State)
            {
                case LoginState.LoggedIn:
                    
                break;
                
                case LoginState.LoggingIn:
                   LogMessages.AllIsWellMessage($"Logged In {_iLogInSession.LoginSessionId.Name}"); 
                break;
            }
        }

        public void Logout()
        {
            _iLogInSession.Logout();
            BindLoginCallbackListeners(false , _iLogInSession);
            //This may not raise an event and if that is the case, use LoggedOut case above
        }
    }
}
