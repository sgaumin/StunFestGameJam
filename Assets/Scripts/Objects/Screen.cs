﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Screen : MonoBehaviour
{
#pragma warning disable 0649 

    public ScreenStates screenState = ScreenStates.Display;
    
    [SerializeField] private Plug plugIn;
    [SerializeField] private Message messagePrefab;
    [SerializeField] private Transform messageTransform;

    [SerializeField] private Image messageDemandImage;

    [HideInInspector] public CableController cableController;
    [HideInInspector] public bool demandGenerated;

    private ScreenController _screen;
    private Message _messageDemand;
    private Message _message;

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
        demandGenerated = true;

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

    public void StartSendingMessage()
    {
        StartCoroutine(SpawnMessage());
    }

    private IEnumerator SpawnMessage()
    {
        while (true)
        {
            var position = plugIn.transform.position;
            position.z = -8f;

            var messageTemp = Instantiate(messagePrefab, position, Quaternion.identity);
            messageTemp.messageColor = _message.messageColor;
            messageTemp.messageShape = _message.messageShape;
            messageTemp.InitMessage();

            messageTemp.cableController = cableController;
            messageTemp.FollowCable();

            yield return new WaitForSeconds(5f);
        }
    }

    public void CompareMessage(Message receivedMessage)
    {
        if(_messageDemand != null)
        {
            _screen.HideBubble();
            if ((receivedMessage.messageColor == _messageDemand.messageColor) &&
                (receivedMessage.messageShape == _messageDemand.messageShape))
            {
                // Win
                _screen.ResetTimer();
                _messageDemand = null;
                demandGenerated = false;
                GameSystem.Instance.messageReceived++;
            }
            else
            {
                // Loose
                ScreenOver();
            }
        } else
        {
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
        
        // Update Status
        screenState = ScreenStates.Mire;
        
        // if demand message was created
        if (demandGenerated != null)
            GameSystem.Instance.messageReceived++;
    }
}