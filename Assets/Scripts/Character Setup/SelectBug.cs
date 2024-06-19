using UnityEngine;
using UnityEngine.UI;

public class SelectBug : MonoBehaviour
{
    [SerializeField]
    private int bugIndex;
    [SerializeField]
    private int bugLevel;
    [SerializeField]
    private BattleItem[] bugItems;
    [SerializeField]
    private BugClass bugClass;
    [SerializeField]
    private Button CompleteButton;

    public void OnclickBugButton()
    {
        if (BugBox.instance.allownedBugs.Count > 0)
        {
            BugBox.instance.allownedBugs.Clear();
        }

        bugItems = new BattleItem[4];

        for (int i = 0; i < 4; i++)
        {
            bugItems[i] = new BattleItem(0);
        }
        Bug newBug = new Bug(bugIndex, bugLevel, bugClass,bugItems);
        BugBox.instance.allownedBugs.Add(newBug);
        BugBox.instance.CloudSaveBugs();

        CompleteButton.interactable = true;
    }
}
