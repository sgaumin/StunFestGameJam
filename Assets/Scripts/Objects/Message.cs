using System;
using UnityEngine;
using System.Collections;
using System.IO;

public class Message : MonoBehaviour
{
#pragma warning disable 0649 

    public MessageColors messageColor;
    public MessageShapes messageShape;

    [HideInInspector] public CableController cableController;

    [Space] [SerializeField] private Sprite blueTriangle;
    [SerializeField] private Sprite blueCube;
    [SerializeField] private Sprite blueCircle;
    [SerializeField] private Sprite greenTriangle;
    [SerializeField] private Sprite greenCube;
    [SerializeField] private Sprite greenCircle;
    [SerializeField] private Sprite redTriangle;
    [SerializeField] private Sprite redCube;
    [SerializeField] private Sprite redCircle;

    [SerializeField] private float timePerNods = 0.1f;

    private SpriteRenderer _sprite;
    private LineRenderer _cable;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InitMessage();
    }

    public void InitMessage()
    {
        _sprite.enabled = true;
        string fileName = "";

        switch (messageShape)
        {
            case MessageShapes.Cube:
                fileName +="losange ";
                break;
            case MessageShapes.Circle:
                fileName += "lune ";
                break;
            case MessageShapes.Triangle:
                fileName += "triangle ";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        switch (messageColor)
        {
            case MessageColors.Red:
                fileName += "rouge";
                break;
            case MessageColors.Green:
                fileName += "vert";
                break;
            case MessageColors.Blue:
                fileName += "bleu";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        switch(fileName)
        {
            case "losange bleu":
                _sprite.sprite = blueCube;
                break;
            case "lune bleu":
                _sprite.sprite = blueCircle;
                break;
            case "triangle bleu":
                _sprite.sprite = blueTriangle;
                break;
            case "losange vert":
                _sprite.sprite = greenCube;
                break;
            case "lune vert":
                _sprite.sprite = greenCircle;
                break;
            case "triangle vert":
                _sprite.sprite = greenTriangle;
                break;
            case "losange rouge":
                _sprite.sprite = redCube;
                break;
            case "lune rouge":
                _sprite.sprite = redCircle;
                break;
            case "triangle rouge":
                _sprite.sprite = redTriangle;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }

    public void FollowCable()
    {
        StartCoroutine(GoToNextPosition(timePerNods));
    }

    private IEnumerator GoToNextPosition(float time)
    {
        _cable = cableController.cable.GetComponent<LineRenderer>();
        if (_cable != null)
        {
            int lenght = _cable.positionCount;
            
            for (int i = 1; i < lenght; i++)
            {
                int indice;

                if (cableController.firstPluggedHead == HeadType.FirstHead)
                {
                    indice = i;
                } else
                {
                    indice = lenght - i;
                }
                float elapsedTime = 0;
                Vector3 startingPos = transform.position;
                while (elapsedTime < time)
                {
                    if (_cable != null)
                    {
                        var tempPos = _cable.GetPosition(indice);
                        tempPos.z = startingPos.z;

                        transform.position = Vector3.Lerp(startingPos, tempPos, (elapsedTime / time));
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    else
                    {
                        Destroy(gameObject);
                        yield break;
                    }
                }
            }
        }
        else
        {
            Destroy(gameObject);
            yield break;
        }

        yield return new WaitForSeconds(0.2f);

        CheckMessageWhenArrived();
    }

    void CheckMessageWhenArrived()
    {
        // Hide message
        _sprite.enabled = false;

        if (cableController.GetOutHead().plug.plugRole == PlugRole.Changer)
        {
            // Change to new Message
            var changer = cableController.GetOutHead().plug.GetComponentInParent<MessageChanger>();
            changer.ChangeMessage(this);
        }
        else if (cableController.GetOutHead().plug.plugRole == PlugRole.Screen)
        {
            // Compare Color + Shape with Screen requirement
            var screen = cableController.GetOutHead().plug.GetComponentInParent<Screen>();
            screen.CompareMessage(this);

            // Destroy object
            Destroy(gameObject);
        }
    }
}