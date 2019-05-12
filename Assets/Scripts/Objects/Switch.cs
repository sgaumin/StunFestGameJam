using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public MessageChangerType messageChangerType;

    public Sprite clockwiseSprite;
    public Sprite anticlockwiseSprite;
    public SpriteRenderer arrowsSprite;


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
                if(MessageChanger.MessageChangerColorClockwiseRotation)
                {
                    arrowsSprite.sprite = clockwiseSprite;
                } else
                {
                    arrowsSprite.sprite = anticlockwiseSprite;
                }
                break;
            case MessageChangerType.Shape:
                MessageChanger.MessageChangerShapeClockwiseRotation = !MessageChanger.MessageChangerShapeClockwiseRotation;
                if (MessageChanger.MessageChangerShapeClockwiseRotation)
                {
                    arrowsSprite.sprite = clockwiseSprite;
                }
                else
                {
                    arrowsSprite.sprite = anticlockwiseSprite;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
