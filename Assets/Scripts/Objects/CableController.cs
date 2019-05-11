using System;
using UnityEngine;

public class CableController : MonoBehaviour
{
    public HeadCable firstHead;
    public HeadCable secondHead;

    [SerializeField] private GameObject cable;

    private void Awake()
    {
        secondHead.gameObject.SetActive(false);
        cable.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Left click
        {
            Destroy(gameObject);
        }
    }

    public void ActiveSecondHead()
    {
        secondHead.gameObject.SetActive(true);
        cable.gameObject.SetActive(true);
        MouseManager.Instance.StartDraggingSecondHead();
    }
}