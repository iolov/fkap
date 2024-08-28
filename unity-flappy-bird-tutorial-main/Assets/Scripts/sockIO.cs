using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIOClient;

public class sockIO : MonoBehaviour
{
    private SocketIOUnity socket;

    private void Start()
    {
        socket = new SocketIOUnity("http://localhost:3243");
        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("conected");
        };
        socket.Connect();
    }

}
