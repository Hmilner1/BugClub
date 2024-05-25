using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectorController : MonoBehaviour
{
    [SerializeField]
    private GameObject MoveButton;
    [SerializeField]
    private GameObject buttonHolder;
    private Bug currentBug;

    public int buttonIndex { private get; set; }
    public int bugIndex { private get; set; }

    private void OnEnable()
    {
        EventManager.instance.OnCloseItemSelection.AddListener(ClosePanel);
    }
    private void OnDisable()
    {
        EventManager.instance.OnCloseItemSelection.RemoveListener(ClosePanel);
    }

    private void Start()
    {
        currentBug = PartyManager.instance.playerBugTeam[bugIndex];
        PopulateItems();
    }

    private void PopulateItems()
    {
        List<BattleItem> items = new List<BattleItem>();
        items = ItemManager.instance.GetItemsFromClass(currentBug.bugClass);
        foreach (BattleItem item in items) 
        {
            GameObject newButton = Instantiate(MoveButton, buttonHolder.transform);
            TMP_Text itemText = newButton.GetComponentInChildren<TMP_Text>();
            ItemButton itemButton = newButton.GetComponent<ItemButton>();
            itemButton.item = item;
            itemButton.bug = currentBug;
            itemButton.ItemIndex = buttonIndex;
            itemText.text = item.Name;
        }
    }

    public void ClosePanel()
    { 
        Destroy(gameObject);
    }
}
