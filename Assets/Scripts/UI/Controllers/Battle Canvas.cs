using System;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using static BattleManager;

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

    private bool battleStarted = false;

    private void OnEnable()
    {
        EventManager.instance.OnOpenBattleCanvas.AddListener(OpenCanvas);
        EventManager.instance.OnCloseBattleCanvas.AddListener(CloseCanvas);
        EventManager.instance.OnRefreshParty.AddListener(UpdatePlayerInfo);
        EventManager.instance.OnEnemyKnockedOut.AddListener(UpdateEnemyInfo);
    }

    private void OnDisable()
    {
        EventManager.instance.OnOpenBattleCanvas.RemoveListener(OpenCanvas);
        EventManager.instance.OnCloseBattleCanvas.RemoveListener(CloseCanvas);
        EventManager.instance.OnRefreshParty.RemoveListener(UpdatePlayerInfo);
        EventManager.instance.OnEnemyKnockedOut.AddListener(UpdateEnemyInfo);
    }
    private void Update()
    {
        if (!battleStarted) { return; }
        UpdateHealthBar(BattleManager.instance.enemyBug[0].currentHP, enemyHpSlider);
        UpdateHealthBar(PartyManager.instance.playerBugTeam[0].currentHP,playerHpSlider);
    }

    private void OpenCanvas()
    {
        UpdatePlayerInfo();
        UpdateEnemyInfo();
        battleStarted = true;
        battleCanvas.enabled = true;
    }

    private void CloseCanvas()
    {
        battleStarted = false;
        battleCanvas.enabled = false;
    }

    private void UpdatePlayerInfo()
    {
        Bug activeBug = PartyManager.instance.playerBugTeam[0];
        playerBugSprite.sprite = BugBox.instance.getBugModel(activeBug.baseBugIndex, false);
        playerName.text = BugBox.instance.GetBugName(activeBug.baseBugIndex);
        playerLvl.text = "Lvl " + activeBug.lvl.ToString();
        playerHpSlider.maxValue = activeBug.HP;
        playerHpSlider.value = activeBug.HP;
        playerBugClass.sprite = BugBox.instance.GetClassUI(activeBug.bugClass);
    }

    private void UpdateEnemyInfo()
    {
        Bug enemyBug = BattleManager.instance.enemyBug[0];
        enemyBugSprite.sprite = BugBox.instance.getBugModel(enemyBug.baseBugIndex, true);
        enemyName.text = BugBox.instance.GetBugName(enemyBug.baseBugIndex);
        enemyLvl.text = "Lvl " + enemyBug.lvl.ToString();
        enemyHpSlider.maxValue = enemyBug.HP;
        enemyHpSlider.value = enemyBug.HP;
        enemyBugClass.sprite = BugBox.instance.GetClassUI(enemyBug.bugClass);
    }

    public void CatchCurrentBug()
    {
        BugBox.instance.AddNewBug();
    }

    private void UpdateHealthBar(int Health,Slider Healthbar)
    {
        int start = (int)Healthbar.value;
        Healthbar.value = Mathf.Lerp(start, Health, .2f*Time.deltaTime);
    }
}
