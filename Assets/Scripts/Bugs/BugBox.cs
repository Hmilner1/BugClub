using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BugBox : MonoBehaviour
{
    public static BugBox instance;

    private BugBoxData bugData;
    public List<Bug> allownedBugs;

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

    public void AddNewBug(Bug newBug)
    {
        allownedBugs.Add(newBug);
        SaveBugData();
        EventManager.instance.OverWorld();
    }

    public void LoadParty(Bug[] party)
    {
        if (allownedBugs == null) { return; }
        for (int i = 0; i < 4; i++)
        {
            if (allownedBugs[i] == null) { return; }
            Bug newBug = new Bug(allownedBugs[i].baseBugIndex, allownedBugs[i].lvl, allownedBugs[i].bugClass);
            party[i] = newBug;
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
        if (currentWildBug == null) 
        {
            Bug errorBug = new Bug(0, 1, BugClass.Tank);
            return errorBug;
        }
        
        return currentWildBug;}

    public void ChangeCurrentWildBug(Bug bug)
    {
        currentWildBug = bug;
    }

    public void LoadBugSaveData()
    { 
        bugData = LocalSaveManager.LoadBugs();
        foreach (Bug bugs in bugData.PlaysOwnedBugs)
        {
            Bug newBug = new Bug(bugs.baseBugIndex,bugs.lvl,bugs.bugClass);
            allownedBugs.Add(newBug);
        }
    }

    public void SaveBugData()
    {
        bugData.PlaysOwnedBugs = allownedBugs;
        LocalSaveManager.SaveBugData(bugData);
    }
}
