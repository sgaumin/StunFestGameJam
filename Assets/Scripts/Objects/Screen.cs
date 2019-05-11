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

    private ScreenController _screen;    
    private Message _messageDemand;
    private Message _message;
    public bool _demandGenerated;
    
    public GameObject mire;

    private void Start()
    {
        _screen = GetComponent<ScreenController>();
        
        GenerateMessage();
        
        mire.SetActive(false);
    }

    private void GenerateMessage()
    {
        _message = Instantiate(messagePrefab, messageTransform.position + Vector3.back / 10, Quaternion.identity);
        _message.transform.SetParent(messageTransform);

        _message.messageColor = (MessageColors) Random.Range(0, 3);
        _message.messageShape = (MessageShapes) Random.Range(0, 3);

        _message.InitMessage();
    }

    public void GenerateDemand()
    {
        _demandGenerated = true;

        _messageDemand = Instantiate(messagePrefab, messageTransform.position + Vector3.back / 10, Quaternion.identity);
        _messageDemand.gameObject.SetActive(false);
        _messageDemand.transform.SetParent(messageTransform);

        _messageDemand.messageColor = (MessageColors) Random.Range(0, 3);
        _messageDemand.messageShape = (MessageShapes) Random.Range(0, 3);

        _messageDemand.InitMessage();

        _screen.ShowBubble();

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
        _screen.HideBubble();
        if ((receivedMessage.messageColor == _messageDemand.messageColor) &&
            (receivedMessage.messageShape == _messageDemand.messageShape))
        {
            // Win
            _screen.ResetTimer();
            GenerateDemand();
        }
        else
        {
            // Loose
            ScreenOver();
        }
    }

    public void ScreenOver()
    {
        // Show Mire
        mire.SetActive(true);
        
        // Hide Messages
        _message.gameObject.SetActive(false);
        
        // Stop Timer
        
    }
}