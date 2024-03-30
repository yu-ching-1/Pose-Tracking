using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmarkTracking : MonoBehaviour
{
    public UdpReceiver UdpReceiver;
    public GameObject[] Landmarks;
    public GameObject[] Head;
    public bool IsPrintToConsole = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string data = UdpReceiver.data;
        if (data.Length < 2) { return; }

        //remove NULL values and square bracket 
        int idx = data.IndexOf('\0');
        if(idx>0) data = data.Substring(0,idx);
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        
        string[] points = data.Split(',');
       
        int index = 0;
        float x = float.Parse(points[index*3]);
        float y = float.Parse(points[index*3+1]);
        float z = float.Parse(points[index*3+2]);
        

        // Head - set landmark #0 as center of head
        Head[0].transform.position = new Vector3(x,y,z);
        if (IsPrintToConsole) print("landmark #"+ index+" "+new Vector3(x,y,z));

        for (int i = 0;i<Landmarks.Length;i++)
        {
            index = i+11;
         
            x = float.Parse(points[index * 3]);
            y = float.Parse(points[index * 3 + 1]);
            z = float.Parse(points[index * 3 + 2]);
          
            Landmarks[i].transform.localPosition = new Vector3(x,y,z);
            if (IsPrintToConsole) print("landmark #"+ index+" "+new Vector3(x,y,z));
        }

    }

}
