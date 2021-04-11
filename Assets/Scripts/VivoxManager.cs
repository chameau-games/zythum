
using System;
using VivoxUnity;
using UnityEngine;
using System.ComponentModel;

public class VivoxManager : MonoBehaviour
{
    private VivoxUnity.Client client;
    private Uri server = new Uri("");
    private string issuer = "";
    private string domain = "";
    private string key = "";
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
    
    public void Login(string userName)
    {
        AccountId accountId = new AccountId(issuer,userName,domain);
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

        switch (source.State)
        {
            case LoginState.LoggedIn:
                Debug.Log("Logged in");
                break;
            
            case LoginState.LoggingIn:
                Debug.Log("Logging in");
                break;
        }
    }
    
    #endregion
    
    
    #region Join Channel methods

    public void JoinChannel(string channelName, bool isAudio, bool isText, bool switchTransmission,ChannelType channelType)
    {
        ChannelId channelId = new ChannelId(issuer,channelName,domain, channelType);
        channelSession = loginSession.GetChannelSession(channelId);
        Bind_Channel_Callback_Listeners(true, channelSession);
        channelSession.BeginConnect(isAudio, isText, switchTransmission, channelSession.GetConnectToken(key, timespan), ar =>
        {
            try
            {
                channelSession.EndConnect(ar);
            }
            catch (Exception e)
            {
                Bind_Channel_Callback_Listeners(false,channelSession);
                Debug.Log(e.Message);
            }
        });
    }

    public void LeaveChannel(IChannelSession channelToDisconnect, string channelName)
    {
        channelToDisconnect.Disconnect();
        loginSession.DeleteChannelSession(new ChannelId(issuer,channelName,domain));
    }

    public void On_Channel_Status_Changed(object sender, PropertyChangedEventArgs channelArgs)
    {
        IChannelSession source = (IChannelSession) sender;

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
    
    #endregion
}
