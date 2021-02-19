using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerController : MonoBehaviour
{
    public int spawnCount;

    private GameObject m_SpawnPoint;
    private Spawner m_Spawner;

    void Start()
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
        m_Spawner.SpawnCreeps(spawnCount);
        
    }
}
