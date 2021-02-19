using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject nextPoint;

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, nextPoint.transform.position);
    }
}
