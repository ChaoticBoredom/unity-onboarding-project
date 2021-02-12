using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RangeFinder : MonoBehaviour
{
    private Tower m_Tower;
    
    void Start()
    {
        m_Tower = gameObject.GetComponentInParent<Tower>();
    }

    void Update()
    {
        var scale = m_Tower.range * 2;
        transform.localScale = new Vector3(scale, scale);
    }
}
