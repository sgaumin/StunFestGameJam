using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Screen : MonoBehaviour
{
#pragma warning disable 0649 

    public ScreenStates screenState;

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
        screenState = ScreenStates.Display;
        
        _screen = GetComponent<ScreenController>();

        GenerateMessage();
        mire.SetActive(false);
    }

    private void GenerateMessage()
    {
        _message = Instantiate(messagePrefab, messageTransform.position + Vector3.back * 1f, Quaternion.identity);
        _message.transform.SetParent(messageTransform);

        MessageColors messageColor = (MessageColors) Random.Range(0, 3);
        MessageShapes messageShape = (MessageShapes) Random.Range(0, 3);

        _screen.SetBackground(messageColor);

        _message.messageColor = messageColor;
        _message.messageShape = messageShape;

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
        if (screenState == ScreenStates.Display)
            StartCoroutine(SpawnMessage());
    }

    private IEnumerator SpawnMessage()
    {
        while (true)
        {
            if (screenState == ScreenStates.Display)
            {
                //var position = plugIn.transform.position;
                var position = cableController.GetMessageStartingPosition();

                position.z = -8f;

                var messageTemp = Instantiate(messagePrefab, position, Quaternion.identity);
                messageTemp.messageColor = _message.messageColor;
                messageTemp.messageShape = _message.messageShape;
                messageTemp.InitMessage();

                messageTemp.cableController = cableController;
                messageTemp.FollowCable();

                yield return new WaitForSeconds(5f);
            }
            else
                break;
        }
    }

    public void CompareMessage(Message receivedMessage)
    {
        if (screenState == ScreenStates.Display)
        {
            if (_messageDemand != null)
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
            }
            else
            {
                ScreenOver();
            }
        }
    }

    public void ScreenOver()
    {
        // Update Status
        screenState = ScreenStates.Mire;
        GameSystem.Instance.UpdateLife();

        GameSystem.Instance.screensDisplay.Remove(this);
        
        // Show Mire
        mire.SetActive(true);

        // Hide Messages
        _message.gameObject.SetActive(false);

        // Stop Timer

        // if demand message was created
        if (demandGenerated)
            GameSystem.Instance.messageReceived++;
    }
}