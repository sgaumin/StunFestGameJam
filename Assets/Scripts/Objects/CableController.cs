using System;
using UnityEngine;

public class CableController : MonoBehaviour
{
    [SerializeField] private HeadCable firstHead;
    [SerializeField] private HeadCable secondHead;

    private void Awake()
    {
        secondHead.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Cable detected");

        var plug = other.GetComponent<Plug>();
        if (plug != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!firstHead.isPlug)
                {
                    firstHead.ConnectHead(plug.transform.position);
                    MouseManager.instance.EndDragging();
                    return;
                }

                if (!secondHead.isPlug)
                {
                    firstHead.ConnectHead(plug.transform.position);
                }
            }
        }
    }
}