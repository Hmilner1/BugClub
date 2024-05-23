using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BugBox : MonoBehaviour
{
    public static BugBox instance;

    private BugBoxData bugData;
    public List<Bug> allownedBugs = new List<Bug>();

    [SerializeField]
    private BugDataBase bugDataBase;
    [SerializeField]
    private Sprite DPSImage;
    [SerializeField]
    private Sprite tankImage;
    [SerializeField]
    private Sprite supportImage;

    private Bug currentWildBug;

    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadBugSaveData();
    }


    private void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            SaveBugData();
        }

        if (Input.GetKeyDown("p"))
        {
            LoadBugSaveData();
        }
    }

    public Sprite getBugModel(int bugIndex, bool front)
    {
        if (front)
        { 
            return bugDataBase.bugDataBase[bugIndex].frontSprite;
        }
        else if(!front)
        {
            return bugDataBase.bugDataBase[bugIndex].backSprite;

        }
        return bugDataBase.bugDataBase[0].backSprite;
    }

    public string GetBugName(int bugIndex)
    {
        return bugDataBase.bugDataBase[bugIndex].bugName;
    }

    public string GetBugDesc(int bugIndex)
    {
        return bugDataBase.bugDataBase[bugIndex].description;
    }

    public void AddNewBug()
    {
        allownedBugs.Add(currentWildBug);
        SaveBugData();
        EventManager.instance.OverWorld();
    }

    public void GiveNewBug(Bug newBug)
    {
        allownedBugs.Add(newBug);
        SaveBugData();
    }

    public void LoadParty(List<Bug> party)
    {
        if (allownedBugs.Count == 0) { return; }
        if (allownedBugs.Count < 4)
        {
            for (int i = 0; i < allownedBugs.Count; i++)
            {
                party.Add(allownedBugs[i]);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                party.Add(allownedBugs[i]);
            }
        }
    }

    public Bug GetActiveBug()
    { 
        return allownedBugs[0];
    }

    public Sprite GetClassUI(BugClass bugClass)
    { 
        switch (bugClass)
        {
            case BugClass.DPS:
                return DPSImage;
              
            case BugClass.Support:
                return supportImage;
            case BugClass.Tank:
                return tankImage;
        }
        return DPSImage;
    }

    public Bug GetWildBug()
    { 
        return currentWildBug;
    }

    public void ChangeCurrentWildBug(Bug bug)
    {
        if (bug == null) { Debug.Log("could not generate"); }

        currentWildBug = bug;
    }

    public void LoadBugSaveData()
    {
        bugData = LocalSaveManager.LoadBugs();
        if (bugData == null)
        {
            bugData = new BugBoxData(allownedBugs);
            return;
        }
        foreach (Bug bugs in bugData.PlaysOwnedBugs)
        {
            Bug newBug = new Bug(bugs.baseBugIndex,bugs.lvl,bugs.bugClass,bugs.currentHP, bugs.equippedItems);

            for(int i = 0; i < newBug.equippedItems.Length; i++)
            {
                BattleItem newItem = new BattleItem(newBug.equippedItems[i].itemIndex);
                newBug.equippedItems[i] = newItem;
            }
            allownedBugs.Add(newBug);
        }
    }

    public void SaveBugData()
    {
        bugData.PlaysOwnedBugs = allownedBugs;
        LocalSaveManager.SaveBugData(bugData);
    }
}
