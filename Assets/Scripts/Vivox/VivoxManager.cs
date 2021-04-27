using System;
using VivoxUnity;
using UnityEngine;
using System.ComponentModel;
using Photon.Pun;

namespace Vivox
{
    public class VivoxManager : MonoBehaviour
    {
        private Client _client;
        private string _userName;
        private Uri server = new Uri("https://mt1s.www.vivox.com/api2");
        private string issuer = "chamea7761-zy80-dev";
        private string domain = "mt1s.vivox.com";
        private string key = "kelp356";
        private TimeSpan timespan = TimeSpan.FromSeconds(90);

        private ILoginSession _loginSession;
        private IChannelSession _channelSession;

        private void Awake()
        {
            if (FindObjectsOfType(GetType()).Length > 1) //POUR EVITER LES DUPLICATE
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(this);
            _userName = SystemInfo.deviceUniqueIdentifier;
            _client = new Client();
            _client.Uninitialize();
            _client.Initialize();
        }

        private void OnApplicationQuit()
        {
            _client.Uninitialize();
        }


        private void Bind_Login_Callback_Listeners(bool bind, ILoginSession loginSesh)
        {
            if (bind)
            {
                loginSesh.PropertyChanged += Login_status;
            }
            else
            {
                loginSesh.PropertyChanged -= Login_status;
            }
        }

        private void Bind_Channel_Callback_Listeners(bool bind, IChannelSession channelSesh)
        {
            if (bind)
            {
                channelSesh.PropertyChanged += On_Channel_Status_Changed;
            }
            else
            {
                channelSesh.PropertyChanged -= On_Channel_Status_Changed;
            }
        }


        #region Login methods

        public void Login()
        {
            AccountId accountId = new AccountId(issuer, _userName, domain);
            _loginSession = _client.GetLoginSession(accountId);
            Bind_Login_Callback_Listeners(true, _loginSession);
            _loginSession.BeginLogin(server, _loginSession.GetLoginToken(key, timespan), ar =>
            {
                try
                {
                    _loginSession.EndLogin(ar);
                }
                catch (Exception e)
                {
                    Bind_Login_Callback_Listeners(false, _loginSession);
                    Debug.Log(e.Message);
                }

                JoinChannel(PhotonNetwork.CurrentRoom.Name);
            });
        }

        private void Logout()
        {
            _loginSession.Logout();
            Bind_Login_Callback_Listeners(false, _loginSession);
        }

        private void Login_status(object sender, PropertyChangedEventArgs loginArgs)
        {
            ILoginSession source = (ILoginSession) sender;

            if ("State" == loginArgs.PropertyName)
            {
                switch (source.State)
                {
                    case LoginState.LoggedIn:
                        Debug.Log("Logged in");
                        break;
                    case LoginState.LoggingIn:
                        Debug.Log("Logging in");
                        break;
                    case LoginState.LoggedOut:
                        Debug.Log("Logged out");
                        break;
                    case LoginState.LoggingOut:
                        Debug.Log("Logging out");
                        break;
                }
            }
        }

        #endregion


        #region Join Channel methods

        private void JoinChannel(string channelName)
        {
            ChannelId channelId = new ChannelId(issuer, channelName, domain);
            _channelSession = _loginSession.GetChannelSession(channelId);
            Bind_Channel_Callback_Listeners(true, _channelSession);
            _channelSession.PropertyChanged += On_Audio_State_Changed;
            _channelSession.BeginConnect(true, false, true, _channelSession.GetConnectToken(key, timespan), ar =>
            {
                try
                {
                    _channelSession.EndConnect(ar);
                }
                catch (Exception e)
                {
                    Bind_Channel_Callback_Listeners(false, _channelSession);
                    _channelSession.PropertyChanged -= On_Audio_State_Changed;
                    Debug.Log(e.Message);
                }
            });
        }

        public void LeaveChannel()
        {
            if (_channelSession != null)
            {
                ChannelId channelId = _channelSession.Channel;
                _channelSession.Disconnect(ar =>
                {
                    // _loginSession.DeleteChannelSession(channelId);
                    Logout();
                });
            }
        }

        private void On_Channel_Status_Changed(object sender, PropertyChangedEventArgs channelArgs)
        {
            IChannelSession source = (IChannelSession) sender;
            if (channelArgs.PropertyName == "ChannelState")
            {
                switch (source.ChannelState)
                {
                    case ConnectionState.Connected:
                        Debug.Log("Channel connected");
                        break;
                    case ConnectionState.Connecting:
                        Debug.Log("Channel connecting");
                        break;
                    case ConnectionState.Disconnected:
                        Debug.Log("Channel disconnected");
                        Bind_Channel_Callback_Listeners(false, _channelSession);
                        break;
                    case ConnectionState.Disconnecting:
                        Debug.Log("Channel disconnecting");
                        break;
                }
            }
        }

        private void On_Audio_State_Changed(object sender, PropertyChangedEventArgs audioArgs)
        {
            IChannelSession source = (IChannelSession) sender;

            if (audioArgs.PropertyName == "AudioState")
            {
                switch (source.AudioState)
                {
                    case ConnectionState.Connected:
                        Debug.Log("Audio channel connected");
                        break;
                    case ConnectionState.Connecting:
                        Debug.Log("Audio channel connecting");
                        break;
                    case ConnectionState.Disconnected:
                        Debug.Log("Audio channel disconnected");
                        _channelSession.PropertyChanged -= On_Audio_State_Changed;
                        break;
                    case ConnectionState.Disconnecting:
                        Debug.Log("Audio channel disconnecting");
                        break;
                }
            }
        }

        #endregion

        public void Mute()
        {
            _client.AudioInputDevices.Muted = !_client.AudioInputDevices.Muted;
        }
    }
}