using UnityEngine;

public class HeadCable : MonoBehaviour
{
    public HeadType headType;

    [HideInInspector] public bool isPlug;
    [HideInInspector] public Plug plug;

    private CableController _cableController;

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
                            plug.cableController = _cableController;

                            this.plug = plug;
                            _cableController.screen = this.plug.GetComponentInParent<Screen>();
                            _cableController.ActiveSecondHead();
                        }
                    }
                    else if (headType == HeadType.SecondHead)
                    {
                        if (!plug.isUsed && plug.plugType == PlugType.Out)
                        {
                            ConnectHead(plug.transform.position);
                            plug.isUsed = true;
                            plug.cableController = _cableController;

                            this.plug = plug;
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
        {
            Destroy(_cableController.gameObject);
            if (_cableController != null)
                if (_cableController.screen != null)
                    _cableController.screen.StopAllCoroutines();
        }
    }

    private void OnDestroy()
    {
        if (plug != null)
            plug.isUsed = false;
    }
}