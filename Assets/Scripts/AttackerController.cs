using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;

public class AttackerController : MonoBehaviour
{
    public int spawnCount;

    private GameManager m_GameManager;

    public void Start()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }

    public void UpdateSpawnCount(string count)
    {
        spawnCount = int.Parse(count);
    }

    public void TriggerSpawn()
    {
        m_GameManager.SpawnCreepsServerRpc(spawnCount);
    }
}
