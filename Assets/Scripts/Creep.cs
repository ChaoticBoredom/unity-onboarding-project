using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Creep : MonoBehaviour
{
    public GameObject goal;
    public float speed;
    public float maxHitPoints;
    public int damage;
    public int gold;
    public int creepCost;
    
    private float m_CurrentHitPoints;
    private Rigidbody m_Rigidbody;
    private GameManager m_GameManager;

    void Start()
    {
        m_CurrentHitPoints = maxHitPoints;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_GameManager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        if (NetworkManager.Singleton.IsClient) return;

        Vector3 goalLocation = goal.transform.position;
        float distance = Vector3.Distance(transform.position, goalLocation);
        if (distance > 0.2f)
        {
            var stepPosition = Vector3.MoveTowards(transform.position, goalLocation, speed * Time.fixedDeltaTime);
            m_Rigidbody.MovePosition(stepPosition);
        }
        else
        {
            goal = GetNextPoint();
            if (!goal)
            {
                m_GameManager.defenderHP.Value -= damage;
                GetComponent<NetworkObject>().Despawn();
                Destroy(gameObject);
            }
        } 
    }

    public void SetGameManager(GameManager manager)
    {
        m_GameManager = manager;
    }

    public void Hit(float incomingDamage)
    {
        if (NetworkManager.Singleton.IsClient) return;

        m_CurrentHitPoints -= incomingDamage;

        if (m_CurrentHitPoints <= 0)
        {
            m_GameManager.defenderGold.Value += gold;
            GetComponent<NetworkObject>().Despawn();
            Destroy(gameObject);
        }
    }

    GameObject GetNextPoint()
    {
        Waypoint script = goal.GetComponent<Waypoint>();
        if (script == null)
            return null;
        return script.nextPoint;
    }
}
