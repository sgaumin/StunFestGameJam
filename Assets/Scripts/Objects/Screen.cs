using UnityEngine;

public class Screen : MonoBehaviour
{
    public Message symbolMessagePrefab;

    [SerializeField] private Plug plugIn;

    [HideInInspector] public CableController cableController;
    
    public void SpawnSymbol()
    {
        var position = plugIn.transform.position;
        position.z = -8f;

        var message = Instantiate(symbolMessagePrefab, position, Quaternion.identity);
        message.cableController = cableController;
        message.FollowCable();
    }
}