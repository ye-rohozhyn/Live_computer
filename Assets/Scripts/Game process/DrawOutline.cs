using UnityEngine;

[RequireComponent(typeof(Outline))]

public class DrawOutline : MonoBehaviour
{
    [SerializeField] private int outlineWidth = 6;

    private Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
    }

    public void OnHoverEnter()
    {
        outline.OutlineWidth = outlineWidth;
    }

    public void OnHoverExit()
    {
        outline.OutlineWidth = 0;
    }
}
