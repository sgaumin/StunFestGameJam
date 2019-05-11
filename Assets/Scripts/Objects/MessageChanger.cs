using System;
using UnityEngine;

public class MessageChanger : MonoBehaviour
{
    public MessageChangerType messageChangerType;

    [SerializeField] private Plug plugIn;

    public void ChangeMessage(Message message)
    {
        switch (messageChangerType)
        {
            case MessageChangerType.Shape:
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

                break;
            case MessageChangerType.Color:
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

            // Update Message
            message.InitMessage();

            // Follow cable
            message.cableController = plugIn.cableController;
            message.FollowCable();
        }
    }
}