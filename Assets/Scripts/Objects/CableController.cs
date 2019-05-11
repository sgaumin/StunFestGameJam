using UnityEngine;
using System.Collections;

public class CableController : MonoBehaviour
{
    public HeadCable firstHead;
    public HeadCable secondHead;

    public Screen screen;
    public bool isConnected;

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
            screen.SpawnSymbol();
        }
    }
}