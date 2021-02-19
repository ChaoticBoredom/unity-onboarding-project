using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerController : MonoBehaviour
{
    public int spawnCount;

    private GameObject m_SpawnPoint;

    void Start()
    {
        m_SpawnPoint = GameObject.FindWithTag("Respawn");
    }

    public void UpdateSpawnCount(string count)
    {
        spawnCount = int.Parse(count);
    }

    public void TriggerSpawn()
    {
        m_SpawnPoint.GetComponent<Spawner>().SpawnCreeps(spawnCount);
        
    }
}
