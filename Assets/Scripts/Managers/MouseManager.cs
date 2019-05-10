using UnityEngine;

public class MouseManager : MonoBehaviour
{
#pragma warning disable 0649 

    public static MouseManager instance;
    
    [SerializeField] private GameObject cablePrefab;
    private bool isDragging;
    private GameObject currentObjectToDrag;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void StartDragging()
    {
        isDragging = true;
        var cable = Instantiate(cablePrefab, transform.position, Quaternion.identity);
        currentObjectToDrag = cable;
    }

    public void EndDragging()
    {
        isDragging = false;
        currentObjectToDrag = null;
    }

    private void Update()
    {
        if (currentObjectToDrag != null)
        {
            Vector3 dragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragPosition.z = -0.1f;
            
            currentObjectToDrag.transform.position = dragPosition;
        }
    }
}