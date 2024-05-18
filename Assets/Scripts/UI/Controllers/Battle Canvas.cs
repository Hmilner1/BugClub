using TMPro;
using UnityEngine;

public class BattleCanvas : MonoBehaviour
{
    [SerializeField]
    private Canvas battleCanvas;

    [SerializeField]
    private TMP_Text playerName;
    [SerializeField]
    private TMP_Text playerLvl;

    private void OnEnable()
    {
        EventManager.instance.OnOpenBattleCanvas.AddListener(OpenCanvas);
        EventManager.instance.OnCloseBattleCanvas.AddListener(CloseCanvas);

    }

    private void OnDisable()
    {
        EventManager.instance.OnOpenBattleCanvas.RemoveListener(OpenCanvas);
        EventManager.instance.OnCloseBattleCanvas.RemoveListener(CloseCanvas);
    }

    private void OpenCanvas()
    {
        UpdatePlayerInfo();
        battleCanvas.enabled = true;
    }

    private void CloseCanvas()
    {
        battleCanvas.enabled = false;
    }

    private void UpdatePlayerInfo()
    {
        playerName.text = BugBox.instance.GetBugName(BugBox.instance.playerBugTeam[0].baseBugIndex);
        playerLvl.text = "Lvl " + BugBox.instance.playerBugTeam[0].lvl.ToString();
    }
}
