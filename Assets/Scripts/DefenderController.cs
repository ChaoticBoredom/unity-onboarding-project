using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class DefenderController : MonoBehaviour
{
    public GameObject towerTemplate;

    public void BuildTower()
    {
        towerTemplate.SetActive(true);
    }
}
