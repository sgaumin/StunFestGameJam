using UnityEngine;

public class Screen : MonoBehaviour
{
    public GameObject symbolMessagePrefab;

    [SerializeField] private Plug plugIn;
    
    public void SpawnSymbol()
    {
        var message = Instantiate(symbolMessagePrefab, plugIn.transform.position, Quaternion.identity);
    }
}