using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data;
using Unity.VisualScripting;

public class UdpReceiver : MonoBehaviour
{
    // Start is called before the first frames update

    public int port = 5024;
    public string address = "127.0.0.1";
    public bool IsReceiveData = true;
    public bool IsPrintToConsole = true;
    public string data;
    private Socket server;
    private Thread receiveThread;
    private const int bufSize = 8*1024;
   
    
    public void Start()
    {
        print("start");
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }


    private void ReceiveData()
    {
        server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        EndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
        server.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
        server.Bind(new IPEndPoint(IPAddress.Parse(address), port));
     
        while (IsReceiveData)
        {
            try
            {               
                byte[] data_byte = new byte[bufSize];
                int recv = server.ReceiveFrom(data_byte,ref anyIP);
                data = Encoding.UTF8.GetString(data_byte);
                
                if(IsPrintToConsole){print(data);}
            }
            catch (Exception ex)
            {
                print(ex.ToString());
            }
        }
    }
    // Update is called once per frame
    
}
