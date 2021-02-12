using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public float range;
    public float fireRate;
    public float damage;
    public GameObject projectilePrefab;
    
    private Collider[] m_Targets;
    private Collider m_Collider;
    private LayerMask m_Mask;
    private bool m_Attacking;
    private GameObject m_Target;

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

    void ChooseTarget()
    {
        if (m_Target != null)
            return;
        
        m_Target = m_Targets[Random.Range(0, m_Targets.Length)].gameObject;
    }

    IEnumerator Attack()
    {
        if (m_Targets.Length == 0)
        {
            m_Attacking = false;
            yield break;
        }

        m_Attacking = true;
        ChooseTarget();
        var projectile = Instantiate(projectilePrefab);
        var projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.target = m_Target;
        projectileScript.damage = damage;
        
        
        yield return new WaitForSeconds(fireRate);

        StartCoroutine(Attack());
    }
}
