using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Waypoint 
{
    public GameObject creep;
    public float spawnInterval;
    
    void Start()
    {
        StartCoroutine(ContinuousSpawn());
    }

    public void SpawnCreep(int count = 1)
    {
        var newCreep = Instantiate(creep, transform.position, Quaternion.identity);
        newCreep.GetComponent<Creep>().goal = nextPoint;
    }

    IEnumerator ContinuousSpawn()
    {
        SpawnCreep();

        yield return new WaitForSeconds(spawnInterval);

        StartCoroutine(ContinuousSpawn());
    }
}
