using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BugBox : MonoBehaviour
{
    public static BugBox instance;

    public BugBoxData bugData;
    public Bug[] playerBugTeam;

    [SerializeField]
    private BugDataBase bugDataBase;

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
    }

    private void Start()
    {
        LoadBugSaveData();
        LoadParty();
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

    public Sprite FindBugModel(int bugIndex, bool front)
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
        bugData.PlaysOwnedBugs.Add(newBug);
        SaveBugData();
    }

    public void LoadParty()
    {
        if (bugData == null) { return; }
        playerBugTeam = new Bug[3];
        for (int i = 0; i < 3; i++)
        {
            if (bugData.PlaysOwnedBugs[i] == null) { return; }
            playerBugTeam[i] = bugData.PlaysOwnedBugs[i];
        }
    }

    public void LoadBugSaveData()
    { 
        bugData = LocalSaveManager.LoadBugs();
    }

    public void SaveBugData()
    { 
        LocalSaveManager.SaveBugData(bugData);
    }

}
