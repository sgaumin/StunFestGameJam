using UnityEngine;

public class Screen : MonoBehaviour
{
#pragma warning disable 0649 

    [SerializeField] private Plug plugIn;
    [SerializeField] private Message symbolMessagePrefab;
    
    [HideInInspector] public CableController cableController;

    private Message _message;

    public void GenerateMessage()
    {
        _message = Instantiate(symbolMessagePrefab, Vector3.zero, Quaternion.identity);
    }

    public void SpawnMessage()
    {
        var position = plugIn.transform.position;
        position.z = -8f;

        _message.cableController = cableController;
        _message.FollowCable();
    }
}