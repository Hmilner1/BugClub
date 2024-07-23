using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBagPartyController : MonoBehaviour
{
    [SerializeField]
    private int listIndex;
    [SerializeField]
    private Image bugImage;
    [SerializeField]
    private TMP_Text lvlText;

    public void UpdateImage()
    {
        bugImage.sprite = BugBox.instance.getBugModel(PartyManager.instance.playerBugTeam[listIndex].baseBugIndex, true);
        lvlText.text = "LVL: " + PartyManager.instance.playerBugTeam[listIndex].lvl.ToString();
    }

    public void OnBugClicked()
    {
        ItemController.instance.LvlupBug(listIndex);
    }
}
