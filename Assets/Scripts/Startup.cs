using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MLAPI;
using MLAPI.Transports.UNET;

public class Startup : MonoBehaviour
{
    public GameObject matchmaker;
    public GameObject creepPath;
    void Start()
    {
#if UNITY_SERVER
        var serverData = System.Environment.GetCommandLineArgs().Last();
        NetworkManager.Singleton.GetComponent<UNetTransport>().ServerListenPort = int.Parse(serverData);
        NetworkManager.Singleton.StartServer();
#else
        Instantiate(matchmaker);
#endif
    }
}
