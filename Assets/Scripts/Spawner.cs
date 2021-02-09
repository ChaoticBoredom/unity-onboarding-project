using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject creep;
    public GameObject goal;
    public float spawnInterval;
    
    void Start()
    {
        StartCoroutine(SpawnCreep());
    }

    IEnumerator SpawnCreep()
    {
        var newCreep = Instantiate(creep, transform.position, Quaternion.identity);
        newCreep.GetComponent<Creep>().goal = goal;

        yield return new WaitForSeconds(spawnInterval);

        StartCoroutine(SpawnCreep());
    }
}
