using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BugBoxUIManager : MonoBehaviour
{
    public static BugBoxUIManager instance;
    [SerializeField]
    private GameObject BugPanel;
    [SerializeField] 
    private GameObject boxSlot;
    [SerializeField]
    private List<GameObject> BugList;
    [SerializeField]
    private List<Button> PartyList;
    public int bugswapindex1;
    public int bugswapindex2;

    private void OnEnable()
    {
        RefreshBugBox();
        bugswapindex1 = -1;
        bugswapindex2 = -1;
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void RefreshBugBox()
    {
        ReadParty();
        ReadBugBox();
    }

    private void ReadParty()
    {
        PartyManager.instance.GetTeamFromSave();
        for (int i = 0; i < PartyList.Count; i++)
        {
            BoxSlotContoller controller = PartyList[i].GetComponent<BoxSlotContoller>();
            controller.SetIndex(PartyManager.instance.playerBugTeam[i].baseBugIndex);
            controller.SetListIndex(i);
            controller.UpdateImage();
        }
    }

    private void ReadBugBox()
    {
        foreach (Transform childTransform in BugPanel.transform)
        {
            GameObject bugBox = childTransform.gameObject;
            if (bugBox == BugPanel) { return; }
            Destroy(bugBox);
        }

        for (int i = 4; i < BugBox.instance.allownedBugs.Count; i++)
        {
            var currentBoxSlot  = Instantiate(boxSlot, BugPanel.transform);
            BoxSlotContoller controller = currentBoxSlot.GetComponent<BoxSlotContoller>();
            controller.SetIndex(BugBox.instance.allownedBugs[i].baseBugIndex);
            controller.SetListIndex(i);
            controller.UpdateImage();
        }
    }

    public void BugSelected(int index)
    {
        //-1 is not possible to reach
        if (bugswapindex1 == -1)
        {
            bugswapindex1 = index;
        }
        else if (bugswapindex2 == -1)
        {
            bugswapindex2 = index;
            SwapBugs();
        }
    }

    private void SwapBugs()
    {
        BugBox.instance.BugSwap(bugswapindex1, bugswapindex2);
        bugswapindex1 = -1;
        bugswapindex2 = -1;
        RefreshBugBox();
        BugBox.instance.CloudSaveBugs();
    }
}
