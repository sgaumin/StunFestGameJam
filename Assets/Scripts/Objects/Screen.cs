using UnityEngine;

public class Screen : MonoBehaviour
{
    public GameObject symbolMessagePrefab;

    [SerializeField] private Plug plugIn;
    
    public void SpawnSymbol()
    {
        var position = plugIn.transform.position;
        position.z = -8f;
        
        var message = Instantiate(symbolMessagePrefab, position, Quaternion.identity);
    }
}