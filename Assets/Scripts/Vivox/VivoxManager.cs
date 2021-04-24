
using System;
using VivoxUnity;
using UnityEngine;
using System.ComponentModel;

public class VivoxManager : MonoBehaviour
{
    private VivoxUnity.Client client;
    private string _userName = SystemInfo.deviceUniqueIdentifier;
    private Uri server = new Uri("https://mt1s.www.vivox.com/api2");
    private string issuer = "chamea7761-zy80-dev";
    private string domain = "mt1s.vivox.com";
    private string key = "kelp356";
    private TimeSpan timespan = new TimeSpan(90);
    

    private ILoginSession loginSession;
    private IChannelSession channelSession;


    private void Awake()
    {
        client = new Client();
        client.Uninitialize();
        client.Initialize();
        DontDestroyOnLoad(this);
    }

    private void OnApplicationQuit()
    {
        client.Uninitialize();
    }
    

    public void Bind_Login_Callback_Listeners(bool bind, ILoginSession loginSesh)
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

    public void Bind_Channel_Callback_Listeners(bool bind, IChannelSession channelSesh)
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
    
    
    
    #region Loggin methods 
    
    public void Login()
    {
        AccountId accountId = new AccountId(issuer,_userName,domain);
        loginSession = client.GetLoginSession(accountId);
        Bind_Login_Callback_Listeners(true,loginSession);
        loginSession.BeginLogin(server, loginSession.GetLoginToken(key, timespan), ar =>
        {
            try
            {
                loginSession.EndLogin(ar);
            }
            catch (Exception e)
            {
                Bind_Login_Callback_Listeners(false,loginSession);
                Debug.Log(e.Message);
            }
        });
    }
    

    public void Logout()
    {
        loginSession.Logout();
        Bind_Login_Callback_Listeners(false,loginSession);
    }

    public void Login_status(object sender, PropertyChangedEventArgs loginArgs)
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

    public void JoinChannel(string channelName, bool switchTransmission)
    {
        ChannelId channelId = new ChannelId(issuer,channelName,domain);
        channelSession = loginSession.GetChannelSession(channelId);
        Bind_Channel_Callback_Listeners(true, channelSession);
        channelSession.PropertyChanged += On_Audio_State_Changed;
        channelSession.BeginConnect(true, false, switchTransmission, channelSession.GetConnectToken(key, timespan), ar =>
        {
            try
            {
                channelSession.EndConnect(ar);
            }
            catch (Exception e)
            {
                Bind_Channel_Callback_Listeners(false,channelSession);
                channelSession.PropertyChanged -= On_Audio_State_Changed;
                Debug.Log(e.Message);
            }
        });
    }

    public void LeaveChannel(IChannelSession channelToDisconnect)
    {
        ChannelId channelId = channelToDisconnect.Channel;
        channelToDisconnect.Disconnect();
        loginSession.DeleteChannelSession(channelId);
    }

    public void On_Channel_Status_Changed(object sender, PropertyChangedEventArgs channelArgs)
    {
        IChannelSession source = (IChannelSession) sender;
        if (channelArgs.PropertyName == "ChannelState")
        {
            switch (source.ChannelState)
            {
                case ConnectionState.Connected:
                    Debug.Log($"{source.Channel.Name} connected");
                    break;
                case ConnectionState.Connecting:
                    Debug.Log("Channel connecting");
                    break;
                case ConnectionState.Disconnected:
                    Debug.Log($"{source.Channel.Name} disconnected");
                    break;
                case ConnectionState.Disconnecting:
                    Debug.Log($"{source.Channel.Name} disconnecting");
                    break;
            }
        }
    }
    
    public void On_Audio_State_Changed(object sender, PropertyChangedEventArgs audioArgs)
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
                    break;
                case ConnectionState.Disconnecting:
                    Debug.Log("Audio channel disconnecting");
                    channelSession.PropertyChanged -= On_Audio_State_Changed;
                    break;
            }
        }
    }
    
    #endregion

    public void Mute()
    {
        client.AudioInputDevices.Muted = !client.AudioInputDevices.Muted;
    }
}
