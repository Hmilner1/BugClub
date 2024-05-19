using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;


public class PartyMemberTab : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image bugSprite;
    [SerializeField]
    private Dropdown[] moves = new Dropdown[4];
    
    [SerializeField]
    private int partyIndex;

    private void OnEnable()
    {
        EventManager.instance.OnRefreshParty.AddListener(RefreshPartyUI);
    }

    private void OnDisable()
    {
        EventManager.instance.OnRefreshParty.RemoveListener(RefreshPartyUI);

    }

    private void RefreshPartyUI()
    {
        bugSprite.sprite = BugBox.instance.getBugModel(PartyManager.instance.playerBugTeam[partyIndex].baseBugIndex,true);
        PopulateMoves();
    }

    private void PopulateMoves()
    {
        List<BattleItem> items = new List<BattleItem>();
        items = PartyManager.instance.playerBugTeam[partyIndex].equipableItems;
        foreach (var dropBox in moves)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Dropdown.OptionData data = new Dropdown.OptionData();
                dropBox.options.Add(data);
                dropBox.options[i].text = items[i].Name;
            }
        }
    }
}
