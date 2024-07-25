using TMPro;
using Unity.Netcode;
using UnityEngine;
using static Unity.Networking.Transport.NetworkPipelineStage;

public class QuestTracker : NetworkBehaviour
{
    public static QuestTracker instance;

    [SerializeField]
    int amountNeeded;
    [SerializeField]
    GameObject objectToRemove;
    [SerializeField]
    GameObject Light;
    [SerializeField]
    TMP_Text amountText;

    int currentAmount;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
      
        currentAmount = 0;
        amountText.text = "Quests Complete: " + currentAmount.ToString() + "/" + amountNeeded.ToString();

    }

    public void UpdateQuest()
    {
        FireServerRpc();
    }

    public void increaseQuestAmount()
    {
        currentAmount++;
        amountText.text = "Quests Complete: " + currentAmount.ToString() + "/" + amountNeeded.ToString();
        CheckAmount();
    }

    private void CheckAmount()
    { 
        if (currentAmount >= amountNeeded) 
        {
            amountText.text = "COMPLETE";
            Destroy(objectToRemove);
            Light.SetActive(true);
        }
    }


    [ServerRpc(RequireOwnership = false)]
    public void FireServerRpc()
    {
        increaseQuestAmount();
        FireClientRpc();
    }

    [ClientRpc]
    private void FireClientRpc()
    {
        if (IsServer) { return; }
        increaseQuestAmount();
    }

}
