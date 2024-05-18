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
    private Image playerBugClass;

    [SerializeField]
    private TMP_Text enemyName;
    [SerializeField]
    private TMP_Text enemyLvl;
    [SerializeField]
    private Slider enemyHpSlider;
    [SerializeField]
    private SpriteRenderer enemyBugSprite;
    [SerializeField]
    private Image enemyBugClass;

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
        UpdateEnemyInfo();
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
        playerHpSlider.value = activeBug.HP;
        playerBugClass.sprite = BugBox.instance.GetClassUI(activeBug.bugClass);
    }

    private void UpdateEnemyInfo()
    {
        Bug enemyBug = BugBox.instance.GetWildBug();

        enemyBugSprite.sprite = BugBox.instance.FindBugModel(enemyBug.baseBugIndex, true);
        enemyName.text = BugBox.instance.GetBugName(enemyBug.baseBugIndex);
        enemyLvl.text = "Lvl " + enemyBug.lvl.ToString();
        enemyHpSlider.maxValue = enemyBug.HP;
        enemyHpSlider.value = enemyBug.HP;
        enemyBugClass.sprite = BugBox.instance.GetClassUI(enemyBug.bugClass);
    }

    public void CatchCurrentBug()
    {
        BugBox.instance.AddNewBug(BugBox.instance.GetWildBug());
    }
}
