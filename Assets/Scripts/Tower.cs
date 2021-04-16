using System.Collections;
using System.Collections.Generic;
using MLAPI;
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
    private Vector3 m_TowerTop;
    private Vector3 m_TowerBottom;

    void Start()
    {
        m_Collider = GetComponent<Collider>();
        m_Mask = LayerMask.GetMask("Ground Creeps");
        var bounds = m_Collider.bounds;
        m_TowerTop = new Vector3(transform.position.x, bounds.max.y, transform.position.z);
        m_TowerBottom = new Vector3(transform.position.x, bounds.min.y, transform.position.z);
    }

    void FixedUpdate()
    {
        if (NetworkManager.Singleton.IsClient) return;

        m_Targets = Physics.OverlapCapsule(m_TowerTop, m_TowerBottom, range, m_Mask);

        if (m_Target)
        {
            if (Vector3.Distance(transform.position, m_Target.transform.position) > range)
                m_Target = null;
        }

        if (!m_Attacking && m_Targets.Length > 0)
            StartCoroutine(Attack());
    }

    void ChooseTarget()
    {
        if (m_Target != null)
            return;
        
        m_Targets = Physics.OverlapCapsule(m_TowerTop, m_TowerBottom, range, m_Mask);
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
        var projectile = Instantiate(projectilePrefab, m_TowerTop, Quaternion.identity);
        var projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.target = m_Target;
        projectileScript.damage = damage;
        projectile.GetComponent<NetworkObject>().Spawn();

        yield return new WaitForSeconds(fireRate);

        StartCoroutine(Attack());
    }
}
