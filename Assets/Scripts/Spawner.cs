using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Waypoint 
{
    public GameObject creep;
    public bool continuousSpawn;
    public float spawnInterval;
    
    void Start()
    {
        if (continuousSpawn)
            StartCoroutine(ContinuousSpawn());
    }

    public void SpawnCreeps(int count = 1)
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
        newCreep.GetComponent<Creep>().goal = nextPoint;
    }
    
    IEnumerator ContinuousSpawn()
    {
        SpawnCreep();

        yield return new WaitForSeconds(spawnInterval);

        StartCoroutine(ContinuousSpawn());
    }
}
