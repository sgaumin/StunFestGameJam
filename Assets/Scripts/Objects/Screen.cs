using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Screen : MonoBehaviour
{
#pragma warning disable 0649 

    [SerializeField] private Plug plugIn;
    [SerializeField] private Message messagePrefab;
    [SerializeField] private Transform messageTransform;
    
    [HideInInspector] public CableController cableController;

    private Message _message;

    private void Start()
    {
        GenerateMessage();
    }

    private void GenerateMessage()
    {
        _message = Instantiate(messagePrefab, messageTransform.position + Vector3.back/10, Quaternion.identity);
        _message.transform.SetParent(messageTransform);
        
        _message.messageColor =  (MessageColors)Random.Range(0, 3);
        _message.messageShape =  (MessageShapes)Random.Range(0, 3);
        
        _message.InitMessage();
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
}