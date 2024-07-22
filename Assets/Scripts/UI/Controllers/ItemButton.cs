using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public BattleItem item;
    public Bug bug;
    public int ItemIndex;

    public void OnItemClicked()
    {
        bug.equippedItems[ItemIndex] = item;
        EventManager.instance.RefreshParty();
        EventManager.instance.OpenItemSelectionMenu();
        BugBox.instance.CloudSaveBugs();
    }
}
