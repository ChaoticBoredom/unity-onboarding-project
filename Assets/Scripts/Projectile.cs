using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject target;
    public float speed;

    private Rigidbody m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target == null)
            Destroy(gameObject);
        
        Vector3 targetLocation = target.transform.position;
        float distance = Vector3.Distance(transform.position, targetLocation);
        if (distance > 0.5f)
        {
            var stepPosition = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.fixedDeltaTime);
            m_Rigidbody.MovePosition(stepPosition);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}