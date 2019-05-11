using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public MessageChangerType messageChangerType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        SwitchChangerOrder();
    }

    void SwitchChangerOrder()
    {
        switch (messageChangerType)
        {
            case MessageChangerType.Color:
                MessageChanger.MessageChangerColorClockwiseRotation = !MessageChanger.MessageChangerColorClockwiseRotation;
                break;
            case MessageChangerType.Shape:
                MessageChanger.MessageChangerShapeClockwiseRotation = !MessageChanger.MessageChangerShapeClockwiseRotation;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
