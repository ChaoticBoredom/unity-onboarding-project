using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Startup : MonoBehaviour
{
    public GameObject matchmaker;
    public GameObject creepPath;
    void Start()
    {
#if UNITY_SERVER
        NetworkManager.Singleton.StartServer();
        Debug.Log("Server Starting");
#else
        Instantiate(matchmaker);
#endif
    }
}
