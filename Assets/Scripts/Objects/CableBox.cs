using UnityEngine;

public class CableBox : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (MouseManager.Instance.currentHeadCableToDrag == null)
        {
            MouseManager.Instance.StartDraggingFirstHead();
        }
        else
        {
            Destroy(MouseManager.Instance.currentHeadCableToDrag.transform.parent.gameObject);
        }
    }
}