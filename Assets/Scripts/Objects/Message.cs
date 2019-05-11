using System;
using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour
{
    public MessageColors messageColor;
    public MessageShapes messageShape;

    public LineRenderer cable;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void FollowCable()
    {
        StartCoroutine(GoToNextPosition(2f));
    }

    private IEnumerator GoToNextPosition(float time)
    {
        for (int i = 0; i < cable.positionCount; i++)
        {
            float elapsedTime = 0;
            Vector3 startingPos = transform.position;
            while (elapsedTime < time)
            {
                if (cable != null)
                {
                    transform.position = Vector3.Lerp(startingPos, cable.GetPosition(i), (elapsedTime / time));
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
        sprite.enabled = false;

        // Compare Color + Shape with Screen requirement
        
        // Destroy object
        Destroy(gameObject);
    }
}