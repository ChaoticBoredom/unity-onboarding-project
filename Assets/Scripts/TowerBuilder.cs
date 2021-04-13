using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class TowerBuilder : MonoBehaviour
{
    private Tower m_TowerScript;
    private Rigidbody m_Rigidbody;
    private Camera m_MainCamera;
    private GameManager m_GameManager;
    private bool m_Dropping;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_MainCamera = Camera.main;
        m_GameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        var pos = Input.mousePosition;
        var location = m_MainCamera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 10));
        m_Rigidbody.MovePosition(location);
        
        if (Input.GetMouseButton(0))
        {
            m_GameManager.BuildTowerServerRpc(transform.position);
            gameObject.SetActive(false);
        }
    }
}
