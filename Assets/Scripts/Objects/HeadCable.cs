using System.Collections;
using UnityEngine;

public class HeadCable : MonoBehaviour
{
#pragma warning disable 0649 
    
    public HeadType headType;

    [HideInInspector] public bool isPlug;
    [HideInInspector] public Plug plug;

    public Sprite cableEnd;

    [SerializeField] private AudioClip[] plugSounds;
    [SerializeField] private AudioClip unplugSound;

    private AudioSource _audioSource;
    private CableController _cableController;

    private void Awake()
    {
        _cableController = GetComponentInParent<CableController>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void ConnectHead(Vector3 plugPos)
    {
        PlayPlugSound();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cableEnd;
        isPlug = true;
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.offset = new Vector2(0, 0);
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
                    if (!plug.isUsed)
                    {
                        if((plug.plugType == PlugType.In) && !_cableController.firstHeadPlugged)
                        {
                            ConnectHead(plug.transform.position);
                            plug.isUsed = true;
                            plug.cableController = _cableController;
                            _cableController.firstHeadPlugged = true;
                            headType = HeadType.FirstHead;
                            this.plug = plug;
                            _cableController.screen = this.plug.GetComponentInParent<Screen>();

                            if (! (_cableController.firstHeadPlugged && _cableController.secondHeadPlugged))
                            {
                                _cableController.ActiveSecondHead();
                            }
                            else
                            {
                                _cableController.firstPluggedHead = HeadType.SecondHead;
                                _cableController.ActiveConnection();
                            }

                        }
                        else if(plug.plugType == PlugType.Out && !_cableController.secondHeadPlugged)
                        {
                            ConnectHead(plug.transform.position);
                            plug.isUsed = true;
                            plug.cableController = _cableController;
                            _cableController.secondHeadPlugged = true;
                            headType = HeadType.SecondHead;

                            this.plug = plug;
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
        StartCoroutine(Deconect());
    }

    private IEnumerator Deconect()
    {
        if (_cableController.isConnected)
        {
            _cableController.isConnected = false;
            
            _audioSource.clip = unplugSound;
            _audioSource.Play();
            
            yield return new WaitForSeconds(unplugSound.length);
            
            Destroy(_cableController.gameObject);
            if (_cableController != null)
                if (_cableController.screen != null)
                    _cableController.screen.StopAllCoroutines();
        }
    }

    private void PlayPlugSound()
    {
        _audioSource.clip = plugSounds[Random.Range(0, plugSounds.Length)];
        _audioSource.Play();
    }

    private void OnDestroy()
    {
        if (plug != null)
            plug.isUsed = false;
    }
}