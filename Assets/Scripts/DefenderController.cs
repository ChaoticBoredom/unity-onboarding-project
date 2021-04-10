using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class DefenderController : MonoBehaviour
{
    public GameObject towerPrefab;

    public void BuildTower()
    {
        var tower = Instantiate(towerPrefab);
    }
}
