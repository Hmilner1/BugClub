using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PartyMemberTab : MonoBehaviour
{
    [SerializeField]
    private Image bugSprite;
    [SerializeField]
    private List<Button> moves;
    [SerializeField]
    private GameObject ItemSelectionPanel;
    [SerializeField]
    private Canvas PartyCanvas;
   
    
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
        PopulateMoves();
        PartyCanvas = GameObject.FindGameObjectWithTag("PartyUI").GetComponent<Canvas>();
    }

    private void RefreshPartyUI()
    {
        bugSprite.sprite = BugBox.instance.getBugModel(PartyManager.instance.playerBugTeam[partyIndex].baseBugIndex,true);
        PopulateMoves();
    }

    private void PopulateMoves()
    {
        for(int i =0; i < moves.Count; i++)
        { 
            TMP_Text buttonText = moves[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = PartyManager.instance.playerBugTeam[partyIndex].equippedItems[i].Name;
        }
    }

    public void OnMoveClicked(int ItemIndex)
    {
        GameObject panel = Instantiate(ItemSelectionPanel, PartyCanvas.transform);
        ItemSelectorController controller = panel.GetComponent<ItemSelectorController>();
        controller.buttonIndex = ItemIndex;
        controller.bugIndex = partyIndex;

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
