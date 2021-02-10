using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public float range;
    private Collider[] m_Targets;
    private Collider m_Collider;
    private LayerMask m_Mask;

    void Start()
    {
        m_Collider = GetComponent<Collider>();
        m_Mask = LayerMask.GetMask("Ground Creeps");
    }
    
    void FixedUpdate()
    {
        var bounds = m_Collider.bounds;
        m_Targets = Physics.OverlapCapsule(bounds.max, bounds.min, range, m_Mask);
        Debug.Log(m_Targets.Length);
    }
}
