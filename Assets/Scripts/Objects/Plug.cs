using UnityEngine;

public class Plug : MonoBehaviour
{
    public PlugType plugType;
    public PlugRole plugRole;

    [HideInInspector] public bool isUsed;
    [HideInInspector] public CableController cableController;

    private CircleCollider2D circleCollider;
    private float initialRadius;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        initialRadius = circleCollider.radius;
    }

    public void SetActive()
    {
        circleCollider.radius = initialRadius;
    }

    public void SetInactive()
    {
        circleCollider.radius = 0f;
    }
}