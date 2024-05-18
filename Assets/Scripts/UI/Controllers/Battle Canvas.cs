using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleCanvas : MonoBehaviour
{
    [SerializeField]
    private Canvas battleCanvas;

    [SerializeField]
    private TMP_Text playerName;
    [SerializeField]
    private TMP_Text playerLvl;
    [SerializeField]
    private Slider playerHpSlider;
    [SerializeField]
    private SpriteRenderer playerBugSprite;

    [SerializeField]
    private TMP_Text enemyName;
    [SerializeField]
    private TMP_Text enemyLvl;
    [SerializeField]
    private Slider enemyHpSlider;
    [SerializeField]
    private SpriteRenderer enemyBugSprite;

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
        Bug activeBug = BugBox.instance.GetActiveBug();
        playerBugSprite.sprite = BugBox.instance.FindBugModel(BugBox.instance.playerBugTeam[0].baseBugIndex, false);
        playerName.text = BugBox.instance.GetBugName(BugBox.instance.playerBugTeam[0].baseBugIndex);
        playerLvl.text = "Lvl " + BugBox.instance.playerBugTeam[0].lvl.ToString();
        playerHpSlider.maxValue = activeBug.HP;
    }

    private void UpdateEnemyInfo(Bug EnemyBug)
    {
        enemyBugSprite.sprite = BugBox.instance.FindBugModel(EnemyBug.baseBugIndex, true);
        enemyName.text = BugBox.instance.GetBugName(EnemyBug.baseBugIndex);
        enemyLvl.text = "Lvl " + EnemyBug.lvl.ToString();
        enemyHpSlider.maxValue = EnemyBug.HP;
    }
}
