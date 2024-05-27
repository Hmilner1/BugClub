using Mono.Cecil.Cil;
using UnityEngine;

public class PartyCanvasController : MonoBehaviour
{
    [SerializeField]
    GameObject partyTabPrefab;
    [SerializeField]
    GameObject battlePartyTabPrefab;
    [SerializeField]
    GameObject partyPanel;


    public void UpdateBattleParty()
    {
        if (PartyManager.instance.playerBugTeam.Count == 0) { return; }

        for (int i = 0; i < PartyManager.instance.playerBugTeam.Count; i++)
        {
            var newTab = Instantiate(battlePartyTabPrefab, partyPanel.transform);
            PartyMemberTab tab = newTab.GetComponent<PartyMemberTab>();
            tab.partyIndex = i;
            EventManager.instance.RefreshParty();
        }
    }

    public void RemoveBattleParty()
    {
        foreach (Transform tab in partyPanel.transform)
        {
            if (tab.gameObject.tag == "PartyTab")
            {
                Destroy(tab.gameObject);
            }
        }
    }

    public void UpdateParty()
    {
        if (PartyManager.instance.playerBugTeam.Count == 0) { return; }

        for (int i = 0; i < PartyManager.instance.playerBugTeam.Count; i++)
        { 
            var newTab = Instantiate(partyTabPrefab,partyPanel.transform);
            PartyMemberTab tab = newTab.GetComponent<PartyMemberTab>();
            tab.partyIndex = i;
            EventManager.instance.RefreshParty();
        }
    }

    public void RemoveParty()
    {
        foreach (Transform tab in partyPanel.transform)
        {
            if (tab.gameObject.tag == "PartyTab")
            {
                Destroy(tab.gameObject);
            }
        }
    }
}
