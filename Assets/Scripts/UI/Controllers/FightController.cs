using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightController : MonoBehaviour
{
    [SerializeField]
    private List<Button> fightButtons = new List<Button>();

    private void OnEnable()
    {
        EventManager.instance.OnOverWorld.AddListener(CloseList);
        EventManager.instance.OnRefreshUI.AddListener(PopulateButtons);
    }

    private void OnDisable()
    {
        EventManager.instance.OnOverWorld.RemoveListener(CloseList);
        EventManager.instance.OnRefreshUI.RemoveListener(PopulateButtons);
    }

    private void CloseList()
    { 
        gameObject.SetActive(false);
    }

    private void PopulateButtons()
    {
        for (int i = 0; i < fightButtons.Count; i++)
        {
            TMP_Text MoveText = fightButtons[i].GetComponentInChildren<TMP_Text>();
            MoveText.text = PartyManager.instance.playerBugTeam[0].equippedItems[i].Name;
        }
    }

    public void CalcDamage(int ButtonIndex)
    {
        int Damage = PartyManager.instance.playerBugTeam[0].equippedItems[ButtonIndex].Damage;
        PreformAttack(Damage);
    }

    private void PreformAttack(int Value)
    {
        EventManager.instance.Attack(Value);
    }
}
