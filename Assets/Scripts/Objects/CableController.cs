using UnityEngine;
using System.Collections;

public class CableController : MonoBehaviour
{
    public HeadCable firstHead;
    public HeadCable secondHead;

    public Screen screen;
    public bool isConnected;

    public bool firstHeadPlugged = false;
    public bool secondHeadPlugged = false;
    public HeadType firstPluggedHead;

    public GameObject cable;

    private void Awake()
    {
        secondHead.gameObject.SetActive(false);
        cable.gameObject.SetActive(false);
    }

    public void ActiveSecondHead()
    {
        secondHead.gameObject.SetActive(true);
        cable.gameObject.SetActive(true);
        MouseManager.Instance.StartDraggingSecondHead();
    }

    public void ActiveConnection()
    {
        StartCoroutine(ChangeStatus());
    }

    private IEnumerator ChangeStatus()
    {
        yield return new WaitForSeconds(0.2f);
        isConnected = true;

        // Launch Message from the screen to cable
        if (screen != null)
        {
            screen.cableController = this;
            screen.StartSendingMessage();
        }
    }


    public Vector3 GetMessageStartingPosition()
    {
        Vector3 position = new Vector3();

        if(firstPluggedHead == HeadType.FirstHead) //in branché ne premier
        {
            position = firstHead.plug.transform.position;
        } else // out branché en premier
        {
            position = secondHead.plug.transform.position;
        }

        return position;
    }


    public HeadCable GetInHead()
    {
        if(firstPluggedHead == HeadType.FirstHead)
        {
            return firstHead;
        } else
        {
            return secondHead;
        }
    }

    public HeadCable GetOutHead()
    {
        if (firstPluggedHead == HeadType.FirstHead)
        {
            return secondHead;
        }
        else
        {
            return firstHead;
        }
    }

}