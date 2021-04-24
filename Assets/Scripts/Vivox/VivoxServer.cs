using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;

public class VivoxServer : MonoBehaviour
{
    private VivoxUnity.Client client;
    readonly Uri server = new Uri("");
    readonly string issuer = "";
    readonly string domain = "";
    readonly string key = "";
    readonly TimeSpan timespan = new TimeSpan(90);
    

    private ILoginSession loginSession;
    private IChannelSession channelSession;
    
    
}
