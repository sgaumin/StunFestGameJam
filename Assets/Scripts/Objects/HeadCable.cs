using System;
using UnityEngine;

public class HeadCable : MonoBehaviour
{
    [HideInInspector] public bool isPlug;

    public void ConnectHead(Vector3 plugPos)
    {
        isPlug = true;
        transform.position = plugPos;
    }
}