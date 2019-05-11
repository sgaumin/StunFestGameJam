using UnityEngine;

public class Screen : MonoBehaviour
{
    public Message symbolMessagePrefab;

    [SerializeField] private Plug plugIn;

    [HideInInspector] public LineRenderer cable;
    
    public void SpawnSymbol()
    {
        var position = plugIn.transform.position;
        position.z = -8f;

        var message = Instantiate(symbolMessagePrefab, position, Quaternion.identity);
        message.cable = cable;
        message.FollowCable();
    }
}