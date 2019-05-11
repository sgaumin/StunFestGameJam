using UnityEngine;

public class MouseManager : MonoBehaviour
{
#pragma warning disable 0649 

    public static MouseManager Instance;

    [SerializeField] private CableController cablePrefab;
    
    private GameObject _currentObjectToDrag;
    private CableController _cable;
    private bool _isDragging;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartDraggingFirstHead()
    {
        if (_currentObjectToDrag == null)
        {
            _isDragging = true;
            _cable = Instantiate(cablePrefab, transform.position, Quaternion.identity);
            _currentObjectToDrag = _cable.firstHead.gameObject;
        }
    }

    public void StartDraggingSecondHead()
    {
        if (_currentObjectToDrag == null)
        {
            _isDragging = true;
            _currentObjectToDrag = _cable.secondHead.gameObject;
        }
    }

    public void EndDragging()
    {
        _isDragging = false;
        _currentObjectToDrag = null;
    }

    private void Update()
    {
        if (_currentObjectToDrag != null)
        {
            Vector3 dragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragPosition.z = -0.1f;

            _currentObjectToDrag.transform.position = dragPosition;
        }
    }
}