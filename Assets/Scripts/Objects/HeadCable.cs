using System.Collections;
using UnityEngine;

public class HeadCable : MonoBehaviour
{
#pragma warning disable 0649 

    public HeadType headType;

    [HideInInspector] public bool isPlug;
    [HideInInspector] public Plug plug;

    public Sprite cableEnd;

    [SerializeField] private ParticleSystem sparkPrefab;

    private CableController _cableController;

    private void Awake()
    {
        _cableController = GetComponentInParent<CableController>();
    }

    private void ConnectHead(Vector3 plugPos)
    {
        PlayPlugSound();
        plug.SetInactive();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cableEnd;
        isPlug = true;
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.offset = new Vector2(0, 0);

        var newPos = plugPos;
        newPos.z = transform.position.z;
        transform.position = plugPos;

        // Spawn Particle
        var particulePos = plugPos + Vector3.back * 50f;
        SpawnParticule(particulePos);

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
                    if (!plug.isUsed)
                    {
                        if ((plug.plugType == PlugType.In) && !_cableController.firstHeadPlugged)
                        {
                            this.plug = plug;
                            ConnectHead(plug.transform.position);
                            plug.isUsed = true;
                            plug.cableController = _cableController;
                            _cableController.firstHeadPlugged = true;
                            headType = HeadType.FirstHead;
                            _cableController.screen = this.plug.GetComponentInParent<Screen>();

                            if (!(_cableController.firstHeadPlugged && _cableController.secondHeadPlugged))
                            {
                                _cableController.ActiveSecondHead();
                            }
                            else
                            {
                                _cableController.firstPluggedHead = HeadType.SecondHead;
                                _cableController.ActiveConnection();
                            }
                        }
                        else if (plug.plugType == PlugType.Out && !_cableController.secondHeadPlugged)
                        {
                            this.plug = plug;

                            ConnectHead(plug.transform.position);
                            plug.isUsed = true;
                            plug.cableController = _cableController;
                            _cableController.secondHeadPlugged = true;
                            headType = HeadType.SecondHead;

                            if (!(_cableController.firstHeadPlugged && _cableController.secondHeadPlugged))
                            {
                                _cableController.ActiveSecondHead();
                            }
                            else
                            {
                                _cableController.firstPluggedHead = HeadType.FirstHead;
                                _cableController.ActiveConnection();
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        Deconect();
    }

    private void Deconect()
    {
        if (_cableController.isConnected)
        {
            plug.SetActive();
            _cableController.isConnected = false;

            PlayUnplugSound();

            Destroy(_cableController.gameObject);
            if (_cableController != null)
                if (_cableController.screen != null)
                    _cableController.screen.StopAllCoroutines();
        }
    }

    private void PlayPlugSound()
    {
        AudioManager.Instance.PlayPlugSound();
    }

    private void PlayUnplugSound()
    {
        AudioManager.Instance.PlayUnplugSound();
    }

    private void OnDestroy()
    {
        if (plug != null)
            plug.isUsed = false;
    }

    private void SpawnParticule(Vector3 position)
    {
        var tempParticule = Instantiate(sparkPrefab, position, Quaternion.identity);
        Destroy(tempParticule, 5f);
    }
}