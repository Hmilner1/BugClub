using UnityEngine;

public class MoveParty : MonoBehaviour
{
    [SerializeField]
    private int partyIndex;


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
        PartyManager.instance.PartySwap(partyIndex,partyIndex-1);
        EventManager.instance.RefreshParty();
    }

    private void MoveDown()
    { 
        PartyManager.instance.PartySwap(partyIndex,partyIndex+1);
        EventManager.instance.RefreshParty();
    }
}
