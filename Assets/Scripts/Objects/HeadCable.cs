using System.Collections;
using UnityEngine;

public class HeadCable : MonoBehaviour
{
#pragma warning disable 0649 
    
    public HeadType headType;

    [HideInInspector] public bool isPlug;
    [HideInInspector] public Plug plug;

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