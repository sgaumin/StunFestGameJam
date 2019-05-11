using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Screen : MonoBehaviour
{
#pragma warning disable 0649 

    [SerializeField] private Plug plugIn;
    [SerializeField] private Message messagePrefab;
    [SerializeField] private Transform messageTransform;

    [SerializeField] private Image messageDemandImage;

    [HideInInspector] public CableController cableController;

    private Message _messageDemand;
    private Message _message;

    private void Start()
    {
        GenerateMessage();
        GenerateDemand();
    }

    private void GenerateMessage()
    {
        _message = Instantiate(messagePrefab, messageTransform.position + Vector3.back / 10, Quaternion.identity);
        _message.transform.SetParent(messageTransform);

        _message.messageColor = (MessageColors) Random.Range(0, 3);
        _message.messageShape = (MessageShapes) Random.Range(0, 3);

        _message.InitMessage();
    }

    private void GenerateDemand()
    {
        _messageDemand = Instantiate(messagePrefab, messageTransform.position + Vector3.back / 10, Quaternion.identity);
        _messageDemand.gameObject.SetActive(false);
        _messageDemand.transform.SetParent(messageTransform);

        _messageDemand.messageColor = (MessageColors) Random.Range(0, 3);
        _messageDemand.messageShape = (MessageShapes) Random.Range(0, 3);

        _messageDemand.InitMessage();

        messageDemandImage.sprite = _messageDemand.GetComponent<SpriteRenderer>().sprite;
        messageDemandImage.color = _messageDemand.GetComponent<SpriteRenderer>().color;
    }

    public void SpawnMessage()
    {
        var position = plugIn.transform.position;
        position.z = -8f;

        var messageTemp = Instantiate(messagePrefab, position, Quaternion.identity);
        messageTemp.messageColor = _message.messageColor;
        messageTemp.messageShape = _message.messageShape;
        messageTemp.InitMessage();

        messageTemp.cableController = cableController;
        messageTemp.FollowCable();
    }

    public void CompareMessage(Message receivedMessage)
    {
        if ((receivedMessage.messageColor == _messageDemand.messageColor) &&
            (receivedMessage.messageShape == _messageDemand.messageShape))
        {
            // WIN
            Debug.Log("Receive good message");
        }
        else
        {
            Debug.Log("Display Mire");
        }
    }
}