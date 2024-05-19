using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;


public class PartyMemberTab : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image bugSprite;
    [SerializeField]
    private TMP_Dropdown[] moves = new TMP_Dropdown[4];
   
    
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
        SetMoveValue();
    }

    private void PopulateMoves()
    {
        foreach (var dropBox in moves)
        {
            List<BattleItem> items = new List<BattleItem>();
            items = PartyManager.instance.playerBugTeam[partyIndex].equipableItems;
            for (int i = 0; i < items.Count; i++)
            {
                TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
                dropBox.options.Add(data);
                dropBox.options[i].text = items[i].Name;
            }
        }
    }

    private void SetMoveValue()
    {
        for (int i = 0; i < moves.Length; i++)
        {
            moves[i].value = 0;
            moves[i].RefreshShownValue();
        }
    }
}
