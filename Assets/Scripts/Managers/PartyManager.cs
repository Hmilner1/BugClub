using UnityEngine;

public class PartyManager : MonoBehaviour
{

    public static PartyManager instance;

    [SerializeField]
    private Bug[] PlayerBugTeam = new Bug[4];

    public Bug[] playerBugTeam { get { return PlayerBugTeam; } protected set { PlayerBugTeam = value; }}

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


    private void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            PartySwap(0, 1);
        }
    }
    private void GetTeamFromSave()
    {
        playerBugTeam = new Bug[4];
        BugBox.instance.LoadParty(playerBugTeam);
        EventManager.instance.RefreshParty();
    }

    public void PartySwap(int index1, int index2)
    { 
        Bug tempBug = playerBugTeam[index1];

        playerBugTeam[index1] = playerBugTeam[index2];
        playerBugTeam[index2] = tempBug;
        EventManager.instance.RefreshParty();
    }

}
