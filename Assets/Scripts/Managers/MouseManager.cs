using UnityEngine;

public class MouseManager : MonoBehaviour
{
#pragma warning disable 0649 

    public static MouseManager Instance;

    [SerializeField] private CableController cablePrefab;

    private GameObject _currentObjectToDrag;
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
        if (_currentObjectToDrag != null)
        {
            Vector3 dragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragPosition.z = -2f;

            _currentObjectToDrag.transform.position = dragPosition;
        }
    }

    public void StartDraggingFirstHead()
    {
        if (_currentObjectToDrag == null)
        {
            _cable = Instantiate(cablePrefab, transform.position, Quaternion.identity);
            _currentObjectToDrag = _cable.firstHead.gameObject;
        }
    }

    public void StartDraggingSecondHead()
    {
        if (_currentObjectToDrag == null)
        {
            _currentObjectToDrag = _cable.secondHead.gameObject;
        }
    }

    public void EndDragging()
    {
        _currentObjectToDrag = null;
    }
}