using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private Transform[] positions;
    [SerializeField] private Transform playerHand;

    public void Click_Action()
    {
        if (playerHand.childCount == 0)
        {
            GiveFuse();
        }
        else
        {
            TakeFuse();
        }
    }

    private void TakeFuse()
    {
        Transform obj = playerHand.GetChild(0);

        bool found = false;

        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i].childCount == 0)
            {
                obj.GetComponent<Fuse>().Give(positions[i].gameObject);

                found = true;
                break;
            }
        }

        if (!found)
        {
            Debug.Log("Full box");
        }
    }

    private void GiveFuse()
    {
        bool found = false;

        for(int i = positions.Length - 1; i > 0; i--)
        {
            if (positions[i].childCount > 0)
            {
                positions[i].GetChild(0).GetComponent<Fuse>().Take();

                found = true;
                break;
            }
        }

        if (!found)
        {
            Debug.Log("Empty box");
        }
    }
}
