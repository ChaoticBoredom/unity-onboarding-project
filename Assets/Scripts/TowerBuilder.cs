using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    private Tower m_TowerScript;
    private Rigidbody m_Rigidbody;
    private Camera m_MainCamera;
    private bool m_Dropping;
    
    void Start()
    {
        m_TowerScript = GetComponent<Tower>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_MainCamera = Camera.main;
    }

    void Update()
    {
        if (m_Dropping)
            return;
        
        var pos = Input.mousePosition;
        var location = m_MainCamera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 10));
        m_Rigidbody.MovePosition(location);
        
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(DropTower());
        }
    }

    IEnumerator DropTower()
    {
        m_Dropping = true;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        m_Rigidbody.MovePosition(new Vector3(transform.position.x, 1, transform.position.z));
        yield return new WaitForSeconds(0.1f);
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        m_TowerScript.enabled = true;
        enabled = false;
    }
}
