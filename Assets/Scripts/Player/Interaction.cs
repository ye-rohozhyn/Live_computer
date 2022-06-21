using UnityEngine;


[RequireComponent(typeof(Outline))]

public class Interaction : MonoBehaviour
{
    private Outline outline;
 

    private void OnEnable()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
    }
    public void OnHoverEnter()
    {
        outline.OutlineWidth = 5;
    }
    public void OnHoverExit()
    {
        outline.OutlineWidth = 0;
    }
}
