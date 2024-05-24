using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PartyMemberTab : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image bugSprite;

    [SerializeField]
    private List<Button> Moves;
   
    
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
        foreach (var button in Moves)
        {
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            buttonText.text = PartyManager.instance.playerBugTeam[partyIndex].equippedItems[0].Name;
        }
    }

    private void SetMoveValue()
    {
        
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
        else if (partyIndex == PartyManager.instance.playerBugTeam.Count-1)
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
