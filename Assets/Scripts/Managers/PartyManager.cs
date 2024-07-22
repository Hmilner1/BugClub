using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{

    public static PartyManager instance;

    [SerializeField]
    private List<Bug> PlayerBugTeam = new List<Bug>();
    public List<Bug> playerBugTeam { get { return PlayerBugTeam; } protected set { PlayerBugTeam = value; }}

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

    private void Start()
    {
        GetTeamFromSave();
    }

    public Bug GetFirstAliveBug()
    { 
        foreach (Bug bug in PlayerBugTeam)
        {
            if (bug.currentHP > 0)
            { 
                return bug;
            }
        }
        return null;
    }

    public void GetTeamFromSave()
    {
        playerBugTeam = new List<Bug>();
        BugBox.instance.LoadParty(playerBugTeam);
        EventManager.instance.RefreshParty();
    }

    public void PartySwap(int index1, int index2)
    { 
        BugBox.instance.BugSwap(index1, index2);
        GetTeamFromSave();
        BugBox.instance.CloudSaveBugs();
    }

}
