using System;
using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour
{
    public MessageColors messageColor;
    public MessageShapes messageShape;

    [HideInInspector] public CableController cableController;

    private SpriteRenderer _sprite;
    private LineRenderer _cable;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _cable = GetComponent<LineRenderer>();
    }

    public void FollowCable()
    {
        StartCoroutine(GoToNextPosition(2f));
    }

    private IEnumerator GoToNextPosition(float time)
    {
        for (int i = 0; i < _cable.positionCount; i++)
        {
            float elapsedTime = 0;
            Vector3 startingPos = transform.position;
            while (elapsedTime < time)
            {
                if (_cable != null)
                {
                    transform.position = Vector3.Lerp(startingPos, _cable.GetPosition(i), (elapsedTime / time));
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

        yield return new WaitForSeconds(1f);

        CheckMessageWhenArrived();
    }

    void CheckMessageWhenArrived()
    {
        // Hide message
        _sprite.enabled = false;

        if (cableController.secondHead.plug.plugRole == PlugRole.Changer)
        {
            // Change to new Message
        }
        else if (cableController.secondHead.plug.plugRole == PlugRole.Screen)
        {
            // Compare Color + Shape with Screen requirement
        }

        // Destroy object
        Destroy(gameObject);
    }
}