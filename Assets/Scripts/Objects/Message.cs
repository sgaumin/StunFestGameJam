using System;
using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour
{
#pragma warning disable 0649 

    public MessageColors messageColor;
    public MessageShapes messageShape;

    [HideInInspector] public CableController cableController;

    [Space] [SerializeField] private Sprite triangle;
    [SerializeField] private Sprite cube;
    [SerializeField] private Sprite circle;
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

        switch (messageColor)
        {
            case MessageColors.Red:
                _sprite.color = Color.red;
                break;
            case MessageColors.Green:
                _sprite.color = Color.green;
                break;
            case MessageColors.Blue:
                _sprite.color = Color.blue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        switch (messageShape)
        {
            case MessageShapes.Cube:
                _sprite.sprite = cube;
                break;
            case MessageShapes.Circle:
                _sprite.sprite = circle;
                break;
            case MessageShapes.Triangle:
                _sprite.sprite = triangle;
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
                float elapsedTime = 0;
                Vector3 startingPos = transform.position;
                while (elapsedTime < time)
                {
                    if (_cable != null)
                    {
                        var tempPos = _cable.GetPosition(i);
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

        if (cableController.secondHead.plug.plugRole == PlugRole.Changer)
        {
            // Change to new Message
            var changer = cableController.secondHead.plug.GetComponentInParent<MessageChanger>();
            changer.ChangeMessage(this);
        }
        else if (cableController.secondHead.plug.plugRole == PlugRole.Screen)
        {
            // Compare Color + Shape with Screen requirement
            var screen = cableController.secondHead.plug.GetComponentInParent<Screen>();
            screen.CompareMessage(this);

            // Destroy object
            Destroy(gameObject);
        }
    }
}