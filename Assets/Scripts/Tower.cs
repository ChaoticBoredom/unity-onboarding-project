using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public float range;
    public float fireRate;
    public GameObject projectilePrefab;
    
    private Collider[] m_Targets;
    private Collider m_Collider;
    private LayerMask m_Mask;
    private bool m_Attacking;

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

        if (!m_Attacking && m_Targets.Length > 0)
            StartCoroutine(Attack());
    }

    GameObject ChooseTarget()
    {
        return m_Targets[Random.Range(0, m_Targets.Length)].gameObject;
    }

    IEnumerator Attack()
    {
        if (m_Targets.Length == 0)
        {
            m_Attacking = false;
            yield break;
        }

        m_Attacking = true;
        var target = ChooseTarget();
        var projectile = Instantiate(projectilePrefab);
        projectile.GetComponent<Projectile>().target = target;
        
        yield return new WaitForSeconds(fireRate);

        StartCoroutine(Attack());
    }
}
