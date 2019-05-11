using UnityEngine;

public class HeadCable : MonoBehaviour
{
    public HeadType headType;

    [HideInInspector] public bool isPlug;

    private CableController _cableController;
    private Plug _plug;

    private void Awake()
    {
        _cableController = GetComponentInParent<CableController>();
    }

    private void ConnectHead(Vector3 plugPos)
    {
        isPlug = true;
        transform.position = plugPos;
        MouseManager.Instance.EndDragging();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var plug = other.GetComponent<Plug>();
        if (plug != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isPlug)
                {
                    if (headType == HeadType.FirstHead)
                    {
                        if (!plug.isUsed && plug.plugType == PlugType.In)
                        {
                            ConnectHead(plug.transform.position);
                            plug.isUsed = true;

                            _plug = plug;
                            _cableController.screen = _plug.GetComponentInParent<Screen>();
                            _cableController.ActiveSecondHead();
                        }
                    }
                    else if (headType == HeadType.SecondHead)
                    {
                        if (!plug.isUsed && plug.plugType == PlugType.Out)
                        {
                            ConnectHead(plug.transform.position);
                            plug.isUsed = true;

                            _plug = plug;
                            _cableController.ActiveConnection();
                        }
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (_cableController.isConnected)
            Destroy(_cableController.gameObject);
    }

    private void OnDestroy()
    {
        if (_plug != null)
            _plug.isUsed = false;
    }
}