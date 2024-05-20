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
    public int partyIndex;

    private void OnEnable()
    {
        EventManager.instance.OnRefreshParty.AddListener(RefreshPartyUI);
    }

    private void OnDisable()
    {
        EventManager.instance.OnRefreshParty.RemoveListener(RefreshPartyUI);
    }

    private void Start()
    {
        ManageButtons();
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
            Bug currentBug = PartyManager.instance.playerBugTeam[partyIndex];
            items = ItemManager.instance.GetItemsFromClass(currentBug.bugClass);
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

    public void OnDropValueChanged()
    { 
        
    }

    private void ManageButtons()
    {
        if (partyIndex == 0)
        {
            foreach (Transform button in GetComponentsInChildren<Transform>())
            {
                if (button.gameObject.tag == "Up")
                {
                    Destroy(button.gameObject);
                }
            }
        }
        else if (partyIndex == 3)
        {
            foreach (Transform button in GetComponentsInChildren<Transform>())
            {
                if (button.gameObject.tag == "Down")
                {
                    Destroy(button.gameObject);
                }
            }
        }
    }

    public void MoveBug(bool isUp)
    {
        if (isUp)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }

    }

    private void MoveUp()
    {
        PartyManager.instance.PartySwap(partyIndex, partyIndex - 1);
        EventManager.instance.RefreshParty();
    }

    private void MoveDown()
    {
        PartyManager.instance.PartySwap(partyIndex, partyIndex + 1);
        EventManager.instance.RefreshParty();
    }
}
