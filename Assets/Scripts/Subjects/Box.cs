using UnityEngine;
using TMPro;

public class Box : MonoBehaviour
{
    [SerializeField] private Transform[] positions;
    [SerializeField] private Transform playerHand;
    [SerializeField] private MessageBox messageBox;

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

        if (obj.tag != "Fuse")
        {
            messageBox.ShowErrorMessage("it's not from here");
            return;
        }

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
            messageBox.ShowErrorMessage("Full box");
        }
    }

    private void GiveFuse()
    {
        bool found = false;

        for(int i = positions.Length - 1; i > -1; i--)
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
            messageBox.ShowErrorMessage("Empty box");
        }
    }
}
