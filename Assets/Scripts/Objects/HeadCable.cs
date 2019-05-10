using System;
using UnityEngine;

public class HeadCable : MonoBehaviour
{
    private bool _isPlug;
    
    private void OnTriggerEnter(Collider other)
    {
        var plug = other.GetComponent<Plug>();
        if (plug != null)
        {
            _isPlug = true;
        }
    }
}