using UnityEngine;

public class MouseManager : MonoBehaviour
{
#pragma warning disable 0649 

    public static MouseManager Instance;

    [SerializeField] private CableController cablePrefab;

    public HeadCable currentHeadCableToDrag;
    private CableController _cable;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (currentHeadCableToDrag != null)
        {
            Vector3 dragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragPosition.z = -2f;

            currentHeadCableToDrag.transform.position = dragPosition;
        }
    }

    public void StartDraggingFirstHead()
    {
        if (currentHeadCableToDrag == null)
        {
            _cable = Instantiate(cablePrefab, transform.position, Quaternion.identity);
            currentHeadCableToDrag = _cable.firstHead;
        }
    }

    public void StartDraggingSecondHead()
    {
        if (currentHeadCableToDrag == null)
        {
            currentHeadCableToDrag = _cable.secondHead;
        }
    }

    public void EndDragging()
    {
        currentHeadCableToDrag = null;
    }
}