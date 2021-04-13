using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class DefenderController : MonoBehaviour
{
    public GameObject towerTemplate;

    private GameManager m_gameManager;

    void Start()
    {
        m_gameManager = FindObjectOfType<GameManager>();
    }

    public void BuildTower()
    {
        towerTemplate.SetActive(true);
    }
}
