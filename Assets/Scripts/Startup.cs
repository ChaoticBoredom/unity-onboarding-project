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
        var serverData = System.Environment.GetCommandLineArgs().Last().Split(':');
        NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = serverData.First();
        NetworkManager.Singleton.GetComponent<UNetTransport>().ServerListenPort = int.Parse(serverData.Last());
        NetworkManager.Singleton.StartServer();
        Debug.Log("Server Starting");
#else
        Instantiate(matchmaker);
#endif
    }
}
