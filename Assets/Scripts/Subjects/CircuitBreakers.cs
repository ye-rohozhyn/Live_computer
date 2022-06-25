using UnityEngine;

public class CircuitBreakers : MonoBehaviour
{
    [SerializeField] private SuperPC superPC;
    [SerializeField] private float damage;
    [SerializeField] private Transform[] fuseHolders;

    private void Update()
    {
        foreach (Transform fuseHolder in fuseHolders)
        {
            if (fuseHolder.childCount == 0)
            {
                superPC.DealingDamage(damage);
            }
        }
    }
}
