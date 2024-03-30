using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class LimbTrack2 : MonoBehaviour
{
    private Quaternion offset = Quaternion.Euler(-90, 0, 0);

    public Transform orignal;
    public Transform destination;
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the cylinder to match the assigned start and end opint 
        var p1 = orignal.position;
        var p2 = destination.position;
        var delta = p2 - p1;
        transform.SetPositionAndRotation((p1+p2)/2f, Quaternion.LookRotation(delta));
        transform.localRotation *= offset;
        var scale = transform.localScale;
        scale.y = delta.magnitude/2f;
        transform.localScale  = scale;

    }
}
