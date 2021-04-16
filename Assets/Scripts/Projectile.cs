using System.Collections;
using System.Collections.Generic;
using MLAPI;
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
        if (NetworkManager.Singleton.IsClient) return;

        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetLocation = target.transform.position;
        var stepPosition = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.fixedDeltaTime);
        m_Rigidbody.MovePosition(stepPosition);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (NetworkManager.Singleton.IsClient) return;

        var creep = coll.gameObject.GetComponent<Creep>();
        creep.Hit(damage);
        Destroy(gameObject);
    }
}
