using UnityEngine;

public class CableBox : MonoBehaviour
{
    private void OnMouseDown()
    {
        MouseManager.Instance.StartDraggingFirstHead();
    }
}