using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestHolder : MonoBehaviour
{
    private Quest quest1;
    private Quest quest2;

    [SerializeField]
    private TMP_Text quest1Name;
    [SerializeField]
    private TMP_Text quest1Desc;
    [SerializeField]
    private TMP_Text quest1Reward;
    [SerializeField]
    private Button handInButton1;


    [SerializeField]
    private TMP_Text quest2Name;
    [SerializeField]
    private TMP_Text quest2Desc;
    [SerializeField]
    private TMP_Text quest2Reward;
    [SerializeField]
    private Button handInButton2;

    private void OnEnable()
    {
        EventManager.instance.OnBeatBug.AddListener(Battle);
        EventManager.instance.OnCatchBug.AddListener(Capture);
        EventManager.instance.OnEnterCity.AddListener(GotoCity);
        EventManager.instance.OnEnterDungeon.AddListener(GotoDungeon);



        UpdateQuestUI();
    }

    private void OnDisable()
    {
        EventManager.instance.OnBeatBug.RemoveListener(Battle);
        EventManager.instance.OnCatchBug.RemoveListener(Capture);
        EventManager.instance.OnEnterCity.RemoveListener(GotoCity);
        EventManager.instance.OnEnterDungeon.RemoveListener(GotoDungeon);


    }

    private void Battle()
    {
        if (quest1 != null)
        {
            if (quest1.QuestType.questType == Type.Kill)
            {
                quest1.QuestType.Killed();
            }
        }

        if (quest2 != null)
        {
            if (quest2.QuestType.questType == Type.Kill)
            {
                quest2.QuestType.Killed();
            }
        }
    }

    private void Capture()
    {
        if (quest1 != null)
        {
            if (quest1.QuestType.questType == Type.Capture)
            {
                quest1.QuestType.Captured();
            }
        }

        if (quest2 != null)
        {
            if (quest2.QuestType.questType == Type.Capture)
            {
                quest2.QuestType.Captured();
            }
        }
    }

    private void GotoCity()
    {
        if (quest1 != null)
        {
            if (quest1.QuestType.questType == Type.GoTo)
            {
                quest1.QuestType.GoToCity();
            }
        }

        if (quest2 != null)
        {
            if (quest2.QuestType.questType == Type.GoTo)
            {
                quest2.QuestType.GoToCity();
            }
        }
    }

    private void GotoDungeon()
    {
        if (quest1 != null)
        {
            if (quest1.QuestType.questType == Type.GoTo)
            {
                quest1.QuestType.GoToDungeon();
            }
        }

        if (quest2 != null)
        {
            if (quest2.QuestType.questType == Type.Capture)
            {
                quest2.QuestType.GoToDungeon();
            }
        }
    }

    public void UpdateQuests(Quest questToGive)
    {
        if (quest1 == null)
        {
            quest1 = questToGive;
        }
        else if (quest2 == null)
        { 
            quest2 = questToGive;
        }
    }

    public void UpdateQuestUI()
    {
        if (quest1 != null)
        {
            quest1Name.text = quest1.QuestName;
            quest1Desc.text = quest1.QuestDescription;
            quest1Reward.text = quest1.JellyReward.ToString();
            if (quest1.QuestType.reached())
            {
                handInButton1.interactable = true;
            }
        }
        if (quest2 != null)
        {
            quest2Name.text = quest2.QuestName;
            quest2Desc.text = quest2.QuestDescription;
            quest2Reward.text = quest2.JellyReward.ToString();
            if (quest2.QuestType.reached())
            { 
                handInButton2.interactable = true;
            }
        }
    }

    public bool BothFull()
    {
        if (quest1 != null)
        {
            if (quest2 != null)
            {
                return true;
            }
        }
        return false;
    }

    public void OnClickComplete(int button)
    {
        if (button == 1)
        {
            if (QuestTracker.instance == null)
            {
                int itemAmount = PlayerInfo.instance.ItemAmount();
                PlayerInfo.instance.Player.itemAmount = itemAmount + quest1.JellyReward;
            }
            quest1.QuestType.currentAmount = 0;
            quest1 = null;
            quest1Name.text = "";
            quest1Desc.text = "";
            quest1Reward.text = "";
            handInButton1.interactable = false;

            if (QuestTracker.instance == null) { return; }

            QuestTracker.instance.UpdateQuest();
        }
        else if (button == 2)
        {
            if (QuestTracker.instance == null)
            {
                int itemAmount = PlayerInfo.instance.ItemAmount();
                PlayerInfo.instance.Player.itemAmount = itemAmount + quest2.JellyReward;
            }
            quest2.QuestType.currentAmount = 0;
            quest2 = null;
            quest2Name.text = "";
            quest2Desc.text = "";
            quest2Reward.text = "";
            handInButton2.interactable = false;

            if (QuestTracker.instance == null) { return; }

            QuestTracker.instance.UpdateQuest();
        }
    }
}
