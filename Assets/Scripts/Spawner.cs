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
    
    public override void NetworkStart()
    {
        _waypoint = GetComponent<Waypoint>();
        if (continuousSpawn)
            StartCoroutine(ContinuousSpawn());
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnCreepsServerRpc(int count = 1)
    {
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
        var position = transform.position;
        position.x = Random.Range(position.x - 0.5f, position.x + 0.5f);
        position.z = Random.Range(position.z - 0.5f, position.z + 0.5f);
        var newCreep = Instantiate(creep, position, Quaternion.identity);
        newCreep.GetComponent<Creep>().goal = _waypoint.nextPoint;
        newCreep.GetComponent<NetworkObject>().Spawn();
    }
    
    IEnumerator ContinuousSpawn()
    {
        SpawnCreep();

        yield return new WaitForSeconds(spawnInterval);

        StartCoroutine(ContinuousSpawn());
    }
}
