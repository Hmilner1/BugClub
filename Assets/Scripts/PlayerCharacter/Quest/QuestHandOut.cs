using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestHandOut : MonoBehaviour
{
    public List<Quest> possibleQuests;
    private Quest questToGive;

    [SerializeField]
    private QuestHolder holder;
    [SerializeField]
    private Canvas questCanvas;

    [SerializeField]
    private TMP_Text questName;
    [SerializeField]
    private TMP_Text questDesc;
    [SerializeField]
    private TMP_Text questReward;
    [SerializeField]
    private Button acceptButton;

    private void OnEnable()
    {
        questToGive = null;

        OpenQuests();

        if (holder.BothFull())
        {
            acceptButton.interactable = false;
        }
        else 
        {
            acceptButton.interactable = true;
        }
    }

    public void OpenQuests()
    {
        int randomNumber = Random.Range(0, possibleQuests.Count);
        questToGive = possibleQuests[randomNumber];
        questName.text = questToGive.QuestName;
        questDesc.text = questToGive.QuestDescription;
        questReward.text = "Bug Jelly: X" + questToGive.JellyReward.ToString();
    }

    public void OnClickAccept()
    {
        holder.UpdateQuests(questToGive);
        questCanvas.gameObject.SetActive(false);
    }
}
