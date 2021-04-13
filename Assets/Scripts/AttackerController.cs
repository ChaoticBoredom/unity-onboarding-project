using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class AttackerController : MonoBehaviour
{
    public int spawnCount;

    private GameObject m_SpawnPoint;
    private Spawner m_Spawner;

    public void Start()
    {
        m_SpawnPoint = GameObject.FindWithTag("Respawn");
        m_Spawner = m_SpawnPoint.GetComponent<Spawner>();
    }

    public void UpdateSpawnCount(string count)
    {
        spawnCount = int.Parse(count);
    }

    public void TriggerSpawn()
    {
        m_Spawner.SpawnCreepsServerRpc(spawnCount);
    }
}
