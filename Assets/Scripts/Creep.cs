using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : MonoBehaviour
{
    public GameObject goal;
    public float speed;
    public float maxHitPoints;
    
    private float m_CurrentHitPoints;
    private Rigidbody m_Rigidbody;

    void Start()
    {
        m_CurrentHitPoints = maxHitPoints;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 goalLocation = goal.transform.position;
        float distance = Vector3.Distance(transform.position, goalLocation);
        if (distance > 0.5f)
        {
            var stepPosition = Vector3.MoveTowards(transform.position, goalLocation, speed * Time.fixedDeltaTime);
            m_Rigidbody.MovePosition(stepPosition);
        }
        else
        {
            Destroy(gameObject);
        } 
    }
}
