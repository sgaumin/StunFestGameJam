using UnityEngine;

public class CableBox : MonoBehaviour
{
    private void OnMouseDown()
    {
        MouseManager.instance.StartDragging();
    }
}