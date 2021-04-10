using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Startup : MonoBehaviour
{
    public GameObject matchmaker;
    void Start()
    {
#if UNITY_SERVER
        NetworkManager.Singleton.StartServer();
        System.Console.WriteLine(string.Format("Server Started at {0}", NetworkManager.Singleton.ConnectedHostname));
#else
        Instantiate(matchmaker);
#endif
    }
}
