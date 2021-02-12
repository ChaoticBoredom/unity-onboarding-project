using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject target;
    public float speed;
    public float damage;

    private Rigidbody m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetLocation = target.transform.position;
        float distance = Vector3.Distance(transform.position, targetLocation);
        var stepPosition = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.fixedDeltaTime);
        m_Rigidbody.MovePosition(stepPosition);
    }

    void OnCollisionEnter(Collision coll)
    {
        var creep = coll.gameObject.GetComponent<Creep>();
        creep.Hit(damage);
        Destroy(gameObject);
    }
}