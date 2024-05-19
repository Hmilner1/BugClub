using UnityEngine;

public class PartyManager : MonoBehaviour
{

    public static PartyManager instance;

    [SerializeField]
    private Bug[] PlayerBugTeam = new Bug[3];

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

    private void GetTeamFromSave()
    {
        playerBugTeam = new Bug[3];
        BugBox.instance.LoadParty(playerBugTeam);
    }


}
