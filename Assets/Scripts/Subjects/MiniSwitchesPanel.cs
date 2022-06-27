using System.Collections.Generic;
using UnityEngine;

public class MiniSwitchesPanel : MonoBehaviour
{
    [SerializeField] private float leftTime = 15;
    [SerializeField] private float range = 5;
    [SerializeField] private List<Switch> waitingList;

    private float startTime = 15;

    private void Start()
    {
        startTime = leftTime;
    }

    private void Update()
    {
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime;
        }
        else if (waitingList.Count > 0)
        {
            int index = Random.Range(0, waitingList.Count);
            waitingList[index].OffSwitch();
            leftTime = startTime;
        }
    }

    public void SetWaitingList(List<Switch> switches)
    {
        waitingList = switches;
    }

    public List<Switch> GetWaitingList()
    {
        return waitingList;
    }
}
