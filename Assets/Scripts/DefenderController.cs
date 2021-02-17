using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderController : MonoBehaviour
{
    public GameObject towerPrefab;

    public void BuildTower()
    {
        Instantiate(towerPrefab);
    }
}
