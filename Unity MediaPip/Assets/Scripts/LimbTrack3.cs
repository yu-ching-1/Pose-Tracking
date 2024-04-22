using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.Profiling.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UIElements;

public class LimbTrack3 : MonoBehaviour
{
    private Quaternion offset = Quaternion.Euler(-90, 0,3);
    private Quaternion wrist_offset = Quaternion.Euler(83.267f, -77.028f,-76.707f);
    private Quaternion spine1_offset = Quaternion.Euler(23,0,0);
    private Quaternion head_offset = Quaternion.Euler(-15.711f,0.077f,-0.045f);
    private Quaternion ankle_offset = Quaternion.Euler(-1.162f,0.031f,61.038f);
    private Quaternion femur_offset = Quaternion.Euler(0.225f,5.0f,0);


    



    public LandmarkTracking LandmarkTracking;
    public Transform[] Avatar;

    // Start is called before the first frame update
    Transform zeros;
    
    void Start()
    {
       zeros = new GameObject ().transform;
       zeros.rotation  =  Quaternion.Euler(0,0,0);
    }

    // Update is called once per frame
    Quaternion forward;
    Quaternion up;
    Quaternion right;
   


    Vector3 origin;
    Vector3 destination;
    
 private float timeCount = 0.0f;
    void Update()
    {

        GameObject[] lms = LandmarkTracking.Landmarks;


        //HIP - CENTER
        //Avatar[0].position = (lms[23].transform.position + lms[24].transform.position) / 2f;
        origin = lms[24].transform.position;
        destination = lms[23].transform.position;
        right = Quaternion.FromToRotation(zeros.right,  destination - origin);
        Avatar[0].rotation = right*offset;

        
        //SPINE 1
        origin = (lms[23].transform.position + lms[24].transform.position) / 2f;
        destination = (lms[11].transform.position + lms[12].transform.position) / 2f;
        forward = Quaternion.FromToRotation(zeros.forward,  destination - origin);
        Avatar[1].rotation = forward*spine1_offset;
        up = Quaternion.LookRotation(Avatar[1].forward,Vector3.Cross(lms[12].transform.position-Avatar[1].position,lms[11].transform.position-Avatar[1].position));
        Avatar[1].rotation = up;

        //HEAD
        // origin = (lms[9].transform.position + lms[10].transform.position) / 2f;
        // destination = lms[0].transform.position;
        // Avatar[2].rotation = Quaternion.Euler(0, 0, 0);
        // forward = Quaternion.FromToRotation(Avatar[2].forward,  destination - origin);
        // Avatar[2].rotation = forward*head_offset;

        //Left Shoulder
        origin = lms[14].transform.position;
        destination = lms[12].transform.position;
        right = Quaternion.FromToRotation(zeros.right,  destination - origin);
        Avatar[3].rotation = right;

        //Left Elbow
        origin = lms[16].transform.position;
        destination = lms[14].transform.position;
        right = Quaternion.FromToRotation(zeros.right,  destination - origin);
        Avatar[4].rotation = right;
      
        //Left wrist
        origin = lms[16].transform.position;
        destination = (lms[20].transform.position+ lms[18].transform.position)/2f;
        var xDir = origin - destination;
        origin = lms[18].transform.position -  lms[16].transform.position;
        destination = lms[20].transform.position -  lms[16].transform.position;        
        var yDir = Vector3.Cross(origin,destination);
        var zDir = Vector3.Cross(xDir,yDir);
        up = Quaternion.LookRotation( zDir,yDir);
        Avatar[5].rotation = up*wrist_offset;
 
        //Right Shoulder
        origin = lms[11].transform.position;
        destination = lms[13].transform.position;
        right = Quaternion.FromToRotation(zeros.right,  destination - origin);
        Avatar[6].rotation = right;

        //Right Elbow
        origin = lms[13].transform.position;
        destination = lms[15].transform.position;
        right = Quaternion.FromToRotation(zeros.right,  destination - origin);
        Avatar[7].rotation = right;

        //Right wrist
        origin = lms[15].transform.position;
        destination = (lms[19].transform.position+ lms[17].transform.position)/2f;
        xDir = destination - origin;
        origin = lms[17].transform.position -  lms[15].transform.position;
        destination = lms[19].transform.position -  lms[15].transform.position;        
        yDir = Vector3.Cross(destination,origin);
        zDir = Vector3.Cross(xDir,yDir);
        up = Quaternion.LookRotation( zDir,yDir);
        Avatar[8].rotation = up;

        //Left Femur
        xDir = lms[24].transform.position - lms[26].transform.position;
        zDir =Vector3.Cross(xDir,Vector3.down) ;

        yDir = Vector3.Cross(zDir,xDir) ;
        up = Quaternion.LookRotation( zDir,yDir);
     
        // zDir = lms[24].transform.position - lms[23].transform.position;
        // yDir = Vector3.Cross(zDir,xDir) ;
        // up = Quaternion.LookRotation( zDir,yDir);
        Avatar[9].rotation = up;

        //Left Knee
        origin = lms[26].transform.position;
        destination = lms[28].transform.position;
        xDir = origin - destination;
        yDir = Vector3.Cross(xDir,Vector3.right);
        // yDir = lms[29].transform.position -lms[31].transform.position;
        zDir = Vector3.Cross(xDir,yDir) ;
        up = Quaternion.LookRotation( zDir,yDir);
        Avatar[10].rotation = up;

        //Left Ankle
        origin = lms[28].transform.position;
        destination = lms[32].transform.position;
        xDir = origin - destination;
        origin = lms[28].transform.position;
        destination = lms[32].transform.position;
        zDir = Vector3.Cross(lms[30].transform.position -lms[28].transform.position ,lms[32].transform.position -lms[28].transform.position);
        yDir = Vector3.Cross(zDir,xDir) ;
        
        up = Quaternion.LookRotation( zDir,yDir);
        right = Quaternion.FromToRotation(zeros.right, lms[28].transform.position -  lms[32].transform.position);

        Avatar[11].rotation = up;

        //Right Femur
        xDir = lms[23].transform.position - lms[25].transform.position;    
        right = Quaternion.FromToRotation(zeros.right, xDir);  
        // zDir = lms[24].transform.position -lms[23].transform.position;
        zDir =Vector3.Cross(xDir,Vector3.down) ;

        yDir = Vector3.Cross(zDir,xDir) ;
        up = Quaternion.LookRotation( zDir,yDir);
     
        Avatar[12].rotation = up;
       
      
        //Right Knee
        origin = lms[25].transform.position;
        destination = lms[27].transform.position;

        xDir = origin - destination;
        yDir = Vector3.Cross(xDir,Vector3.right);
        // yDir = lms[30].transform.position -lms[32].transform.position;
        zDir = Vector3.Cross(xDir,yDir) ;
        up = Quaternion.LookRotation( zDir,yDir);
        right = Quaternion.FromToRotation(zeros.right, xDir);  
        
        Avatar[13].rotation =up;

        //Right Ankle
        origin = lms[27].transform.position;
        destination = lms[31].transform.position;
        xDir = origin - destination;
        origin = lms[27].transform.position;
        destination = lms[31].transform.position;
        zDir = Vector3.Cross(lms[29].transform.position -lms[27].transform.position ,lms[31].transform.position -lms[27].transform.position);
        yDir = Vector3.Cross(zDir,xDir) ;
        
        up = Quaternion.LookRotation( zDir,yDir);

        Avatar[14].rotation = up;



        // Avatar[0].position = (lms[23].transform.position + lms[24].transform.position) / 2;
        // origin = lms[24].transform.position;
        // destination = lms[23].transform.position;
        // Avatar[0].rotation = Quaternion.FromToRotation(zeros.forward,  origin - destination)*offset;
        // // Avatar[0].rotation = Quaternion.FromToRotation(Avatar[0].up,  destination - origin);
        // //Avatar[0].rotation = Quaternion.Euler(0, 0, 0);
        // //Avatar[0].rotation = Quaternion.FromToRotation(Avatar[0].up, Avatar[1].position - (lms[23].transform.position + lms[24].transform.position) / 2);
        // //Avatar[0].rotation = Quaternion.FromToRotation(Avatar[0].forward, Vector3.Cross(Avatar[1].position - lms[23].transform.position, Avatar[1].position - lms[24].transform.position));
        // /*float p0f = Quaternion.FromToRotation(Avatar[0].right, lms[23].transform.position - lms[24].transform.position).eulerAngles.x;
        // Avatar[0].Rotate(p0f, 0, 0);*/
        // //Avatar[0].rotation = Quaternion.LookRotation(Vector3.Cross(Avatar[1].position - lms[23].transform.position, Avatar[1].position - lms[24].transform.position), Avatar[0].up);
        // /*Vector3 p0right = lms[23].transform.position - lms[24].transform.position;
        // Vector3 p0forward = Vector3.Cross(Avatar[1].position - lms[23].transform.position, Avatar[1].position - lms[24].transform.position);
        // Avatar[0].rotation = Quaternion.FromToRotation(Avatar[0].up, Vector3.Cross(p0forward, p0right));
        // Avatar[0].Rotate(180, 0, 0);*/
        //   //�y
        // Avatar[1].rotation = Quaternion.Euler(0, 0, 0);
        // Avatar[1].rotation = Quaternion.FromToRotation(Avatar[1].up, (lms[11].transform.position + lms[12].transform.position) / 2 - Avatar[1].position);
        // /*float p1u = Quaternion.FromToRotation(Avatar[1].forward, Vector3.Cross(lms[12].transform.position - Avatar[1].position, lms[11].transform.position - Avatar[1].position)).eulerAngles.y;
        // Avatar[1].Rotate(0, p1u, 0);*/
        // Avatar[1].rotation = Quaternion.LookRotation(Vector3.Cross(lms[12].transform.position - Avatar[1].position, lms[11].transform.position - Avatar[1].position), Avatar[1].up);
        // /*Vector3 p1up = (lms[11].transform.position + lms[12].transform.position) / 2 - Avatar[1].position;
        // Vector3 p1forward = Vector3.Cross(lms[12].transform.position - Avatar[1].position, lms[11].transform.position - Avatar[1].position);
        // Avatar[1].rotation = Quaternion.FromToRotation(Avatar[1].right, Vector3.Cross(p1forward, p1up));
        // Avatar[1].Rotate(0, 180, 0);*/
        //   //�Y
        // Avatar[2].rotation = Quaternion.Euler(0, 0, 0);
        // /*Avatar[5].rotation = Quaternion.FromToRotation(Avatar[5].up, (lms[2].transform.position + lms[5].transform.position) / 2 - (lms[9].transform.position + lms[10].transform.position) / 2);
        // float p5u = Quaternion.FromToRotation(Avatar[5].forward, Vector3.Cross(lms[2].transform.position - lms[9].transform.position, lms[5].transform.position - lms[10].transform.position)).eulerAngles.y;
        // Avatar[5].Rotate(0, p5u, 0);*/
        // //Avatar[5].rotation = Quaternion.LookRotation(Vector3.Cross(lms[5].transform.position - lms[10].transform.position, lms[2].transform.position - lms[9].transform.position), Avatar[5].up);
        // Vector3 p5up = (lms[2].transform.position + lms[5].transform.position) / 2 - (lms[9].transform.position + lms[10].transform.position) / 2;
        // Vector3 p5forward = Vector3.Cross(lms[0].transform.position - lms[9].transform.position, lms[0].transform.position - lms[10].transform.position);
        // Avatar[2].rotation = Quaternion.FromToRotation(Avatar[2].right, Vector3.Cross(p5forward, p5up));
        // Avatar[2].Rotate(0, 180, 0);

        
        // //���}:7~10
        //   //�L��
        // //Avatar[7].position = lms[23].transform.position;
        // Avatar[9].rotation = Quaternion.Euler(0, 0, 0);
        // Avatar[9].rotation = Quaternion.FromToRotation(Avatar[7].up, lms[25].transform.position - lms[23].transform.position);
        // /*float p7u = Quaternion.FromToRotation(Avatar[7].forward, Vector3.Cross(lms[25].transform.position - lms[23].transform.position, lms[23].transform.position - lms[24].transform.position)).eulerAngles.y;
        // Avatar[7].Rotate(0, p7u, 0);*/
        //   //���\
        // Avatar[10].rotation = Quaternion.Euler(0, 0, 0);
        // Avatar[10].rotation = Quaternion.FromToRotation(Avatar[10].up, lms[27].transform.position - lms[25].transform.position);
        // /*float p8u = Quaternion.FromToRotation(Avatar[8].forward, Vector3.Cross(lms[27].transform.position - lms[25].transform.position, lms[23].transform.position - lms[24].transform.position)).eulerAngles.y;
        // Avatar[8].Rotate(0, p8u, 0);*/
        //   //�}��
        // Avatar[11].rotation = Quaternion.Euler(0, 0, 0);
        // Avatar[11].rotation = Quaternion.FromToRotation(Avatar[11].up, lms[31].transform.position - lms[27].transform.position);
        // /*float p9u = Quaternion.FromToRotation(Avatar[9].forward, Vector3.Cross(lms[31].transform.position - lms[27].transform.position, lms[23].transform.position - lms[24].transform.position)).eulerAngles.y;
        // Avatar[9].Rotate(0, p9u, 0);*/

        // //�k�}:11~14
        //   //�L��
        // Avatar[12].rotation = Quaternion.Euler(0, 0, 0);
        // Avatar[12].rotation = Quaternion.FromToRotation(Avatar[12].up, lms[26].transform.position - lms[24].transform.position);
        // float p11u = Quaternion.FromToRotation(Avatar[12].forward, Vector3.Cross(lms[24].transform.position - lms[23].transform.position, lms[26].transform.position - lms[24].transform.position)).eulerAngles.y;
        // Avatar[12].Rotate(0, p11u, 0);
        //   //���\
        // Avatar[13].rotation = Quaternion.Euler(0, 0, 0);
        // Avatar[13].rotation = Quaternion.FromToRotation(Avatar[13].up, lms[28].transform.position - lms[26].transform.position);
        //   //�}��
        // Avatar[14].rotation = Quaternion.Euler(0, 0, 0);
        // Avatar[14].rotation = Quaternion.FromToRotation(Avatar[14].up, lms[32].transform.position - lms[28].transform.position);

        // //����:15~17
        //   //�ӻH
        // Avatar[3].rotation = Quaternion.Euler(0, 0, 0);
        // Avatar[3].rotation = Quaternion.FromToRotation(Avatar[3].up, lms[13].transform.position - lms[11].transform.position);
        //   //��y
        // Avatar[4].rotation = Quaternion.Euler(0, 0, 0);
        // Avatar[4].rotation = Quaternion.FromToRotation(Avatar[4].up, lms[15].transform.position - lms[13].transform.position);

        // //�k��:18~20
        //   //�ӻH
        // //Avatar[18].position = lms[12].transform.position;
        // Avatar[6].rotation = Quaternion.Euler(0, 0, 0);
        // Avatar[6].rotation = Quaternion.FromToRotation(Avatar[6].up, lms[14].transform.position - lms[12].transform.position);
        //   //��y
        // Avatar[7].rotation = Quaternion.Euler(0, 0, 0);
        // Avatar[7].rotation = Quaternion.FromToRotation(Avatar[7].up, lms[16].transform.position - lms[14].transform.position);

      
    }



}
