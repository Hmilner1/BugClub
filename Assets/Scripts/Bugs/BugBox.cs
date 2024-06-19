using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BugBox : MonoBehaviour
{
    public static BugBox instance;

    public BugBoxData bugData;
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
        allownedBugs = new List<Bug>();
        //LoadBugSaveData();
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

    public void BugSwap(int index1, int index2)
    {
        Bug tempBug = allownedBugs[index1];

        allownedBugs[index1] = allownedBugs[index2];
        allownedBugs[index2] = tempBug;
        SaveBugData();
    }

    public void HealAll()
    {
        foreach (Bug bugs in allownedBugs)
        {
            int newHealth = bugs.HP;
            bugs.currentHP = newHealth;
            PartyManager.instance.GetTeamFromSave();
        }
        SaveBugData();
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

    public void CloudSaveBugs()
    {
        bugData = new BugBoxData(allownedBugs);
        //bugData.PlaysOwnedBugs = allownedBugs;
        CloudSaveManager.instance.SaveBugData(bugData);
    }

    public async void CloudLoadBugs()
    {
        CloudSaveManager.instance.LoadBugSave();

        await Task.Delay(1000);

        if (bugData == null)
        {
            Debug.Log("Save Not Found");
            SceneController.Instance.LoadSceneAdditive("CharacterSetup");
            //bugData = new BugBoxData(allownedBugs);
            return;
        }
        else
        {

            foreach (Bug bugs in bugData.PlaysOwnedBugs)
            {
                Bug newBug = new Bug(bugs.baseBugIndex, bugs.lvl, bugs.bugClass, bugs.currentHP, bugs.equippedItems);

                for (int i = 0; i < newBug.equippedItems.Length; i++)
                {
                    BattleItem newItem = new BattleItem(newBug.equippedItems[i].itemIndex);
                    newBug.equippedItems[i] = newItem;
                }
                allownedBugs.Add(newBug);
            }
        }
        //SaveBugData();
    }
}
