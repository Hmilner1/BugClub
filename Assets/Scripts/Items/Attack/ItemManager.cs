using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    [SerializeField]
    private BattleItemDataBase itemDatabase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private List<BattleItem> TankItems = new List<BattleItem>();
    [SerializeField]
    private List<BattleItem> DpsItems = new List<BattleItem>();
    [SerializeField]
    private List<BattleItem> SupportItems = new List<BattleItem>();

    public List<BattleItem> tankItems { get { return TankItems; } protected set { TankItems = value; } }
    public List<BattleItem> dpsItems { get { return DpsItems; } protected set { DpsItems = value; } }
    public List<BattleItem> supportItems { get { return SupportItems; } protected set { SupportItems = value; } }

    private void Start()
    {
        TankItems = ClassItems(BugClass.Tank);
        DpsItems = ClassItems(BugClass.DPS);
        SupportItems = ClassItems(BugClass.Support);
    }

    private List<BattleItem> ClassItems(BugClass itemClass)
    {
        List<BattleItem> tempItems = new List<BattleItem>();
        for(int i =0; i < itemDatabase.battleItemDataBase.Count; i++)
        {
            if (itemDatabase.battleItemDataBase[i].itemClass == itemClass)
            {
                BattleItem newItem = new BattleItem(i);
                tempItems.Add(newItem);
            }
        }
        return tempItems;
    }

    public List<BattleItem> GetItemsFromClass(BugClass bugClass)
    { 
        switch(bugClass)
        {
            case BugClass.Tank:
                return TankItems;
            case BugClass.DPS:
                return DpsItems;
            case BugClass.Support:
                return SupportItems;
        }
        return null;
    }

    public BattleItem[] GetRandomItems(BugClass bugClass)
    {
        BattleItem[] tempItems = new BattleItem[4];
     

        GetItemsFromClass(bugClass);

        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, GetItemsFromClass(bugClass).Count);
            BattleItem newItem = new BattleItem(GetItemsFromClass(bugClass)[randomIndex].itemIndex);
            tempItems[i] = newItem;
        }
        return tempItems;

    }


}
