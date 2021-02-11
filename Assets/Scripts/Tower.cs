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
        var top = new Vector3(transform.position.x, bounds.max.y, transform.position.z);
        var bottom = new Vector3(transform.position.x, bounds.min.y, transform.position.z);
        m_Targets = Physics.OverlapCapsule(top, bottom, range, m_Mask);
    }
}
