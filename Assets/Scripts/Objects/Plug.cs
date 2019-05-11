using UnityEngine;

public class Plug : MonoBehaviour
{
    public PlugType plugType;
    public PlugRole plugRole;

    [HideInInspector] public bool isUsed;
    [HideInInspector] public CableController cableController;
}