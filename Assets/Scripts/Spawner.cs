using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    public GameObject creep;
    public bool continuousSpawn;
    public float spawnInterval;

    private Waypoint _waypoint;
    private GameManager m_GameManager;
    
    public override void NetworkStart()
    {
        m_GameManager = FindObjectOfType<GameManager>();
        _waypoint = GetComponent<Waypoint>();
        if (continuousSpawn)
            StartCoroutine(ContinuousSpawn());
    }

    [ClientRpc]
    public void SpawnCreepsClientRpc(int count = 1)
    {
        Debug.Log("Spawn Creeps!");
        StartCoroutine(SpawnOnDemand(count));
    }

    IEnumerator SpawnOnDemand(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnCreep();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void SpawnCreep()
    {
        int creepCost = creep.GetComponent<Creep>().creepCost;
        if (m_GameManager.attackerGold.Value < creepCost) return;
        m_GameManager.attackerGold.Value -= creepCost;

        var position = transform.position;
        position.x = Random.Range(position.x - 0.5f, position.x + 0.5f);
        position.z = Random.Range(position.z - 0.5f, position.z + 0.5f);
        var newCreep = Instantiate(creep, position, Quaternion.identity);
        var creepScript = newCreep.GetComponent<Creep>();
        creepScript.goal = _waypoint.nextPoint;
        creepScript.SetGameManager(m_GameManager);
        newCreep.GetComponent<NetworkObject>().Spawn();
    }
    
    IEnumerator ContinuousSpawn()
    {
        SpawnCreep();

        yield return new WaitForSeconds(spawnInterval);

        StartCoroutine(ContinuousSpawn());
    }
}
