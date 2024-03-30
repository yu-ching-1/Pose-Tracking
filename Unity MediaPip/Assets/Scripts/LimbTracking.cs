using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LimbTracking : MonoBehaviour
{
    // Start is called before the first frame update
    LineRenderer lineRenderer;
    public Transform origin;
    public Transform destination;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;

    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0,origin.position);
        lineRenderer.SetPosition(1,destination.position);

    }
}
