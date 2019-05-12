using System;
using UnityEngine;

public class MessageChanger : MonoBehaviour
{
#pragma warning disable 0649 

    public MessageChangerType messageChangerType;

    public static bool MessageChangerColorClockwiseRotation = true;
    public static bool MessageChangerShapeClockwiseRotation = true;

    [SerializeField] private Plug plugIn;

    public void ChangeMessage(Message message)
    {
        switch (messageChangerType)
        {
            case MessageChangerType.Shape:
                if(MessageChangerShapeClockwiseRotation)
                {
                    switch (message.messageShape)
                    {
                        case MessageShapes.Cube:
                            message.messageShape = MessageShapes.Circle;
                            break;
                        case MessageShapes.Circle:
                            message.messageShape = MessageShapes.Triangle;
                            break;
                        case MessageShapes.Triangle:
                            message.messageShape = MessageShapes.Cube;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    switch (message.messageShape)
                    {
                        case MessageShapes.Cube:
                            message.messageShape = MessageShapes.Triangle;
                            break;
                        case MessageShapes.Circle:
                            message.messageShape = MessageShapes.Cube;
                            break;
                        case MessageShapes.Triangle:
                            message.messageShape = MessageShapes.Circle;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                break;
            case MessageChangerType.Color:
                if (MessageChangerColorClockwiseRotation)
                {
                    switch (message.messageColor)
                    {
                        case MessageColors.Red:
                            message.messageColor = MessageColors.Green;
                            break;
                        case MessageColors.Green:
                            message.messageColor = MessageColors.Blue;
                            break;
                        case MessageColors.Blue:
                            message.messageColor = MessageColors.Red;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                }
                else
                {
                    switch (message.messageColor)
                    {
                        case MessageColors.Red:
                            message.messageColor = MessageColors.Blue;
                            break;
                        case MessageColors.Green:
                            message.messageColor = MessageColors.Red;
                            break;
                        case MessageColors.Blue:
                            message.messageColor = MessageColors.Green;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        // if cable connected to Out Plug, transform to plug
        if (plugIn.isUsed)
        {
            var position = plugIn.transform.position;
            position.z = -8f;
            message.transform.position = position;


            // Follow cable
            if (plugIn.cableController.isConnected)
            {
                // Update Message
                message.InitMessage();

                message.cableController = plugIn.cableController;
                message.FollowCable();
            }
        }
    }
}