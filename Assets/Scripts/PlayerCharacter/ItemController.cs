using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public static ItemController instance;

    private int Amount;
    [SerializeField]
    private GameObject ItemButton;
    [SerializeField]
    private GameObject ItemHolder;
    private bool ItemClicked;
    [SerializeField]
    private List<GameObject> partyList;

    private void OnEnable()
    {
        Amount = PlayerInfo.instance.ItemAmount();
        ItemClicked = false;
        UpdateUI();
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        ItemClicked = false;
    }

    private void UpdateUI()
    {
        foreach (Transform childTransform in ItemHolder.transform)
        {
            GameObject item = childTransform.gameObject;
            if (item == ItemHolder) { return; }
            Destroy(item);
        }

        for (int i = 0; i < Amount; i++)
        {
            Instantiate(ItemButton, ItemHolder.transform);
        }

        foreach (GameObject item in partyList) 
        {
            ItemBagPartyController controller = item.GetComponent<ItemBagPartyController>();
            controller.UpdateImage();
        }
    }

    public void OnItemClick()
    {
        ItemClicked = true;
    }

    public void LvlupBug(int BugIndex)
    {
        if (!ItemClicked) { return; }
        if (ItemClicked)
        {
            ItemClicked = false;
            PartyManager.instance.playerBugTeam[BugIndex].lvl += 1;
            BugBox.instance.CloudSaveBugs();
            Amount = Amount - 1;
            PlayerInfo.instance.Player.itemAmount = Amount;
            PlayerInfo.instance.SaveItemAmount(Amount);
            UpdateUI();
        }
    }
}
