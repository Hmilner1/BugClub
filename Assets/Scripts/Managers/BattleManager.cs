using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public BattleState currentState;

    public enum BattleState { start, moveSelection, attack, battleEnd, NON }

    [SerializeField]
    private GameObject battleCam;
    [SerializeField]
    private GameObject battlePositions;
    [SerializeField]
    private GameObject actionSelector;

    [SerializeField]
    private GameObject moveSelector;

    public Bug playerBug { get; private set; }
    public Bug enemyBug { get; private set; }

    [SerializeField]
    private TMP_Text battleText;
    [SerializeField]
    private float battleSpeed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        EventManager.instance.OnBattle.AddListener(BattleBegin);
        EventManager.instance.OnPreformAttack.AddListener(PlayerAttackSelect);
    }

    private void OnDisable()
    {
        EventManager.instance.OnBattle.RemoveListener(BattleBegin);
        EventManager.instance.OnPreformAttack.RemoveListener(PlayerAttackSelect);
    }

    private void Start()
    {
        currentState = BattleState.NON;
    }

    public void PopulateTrainerTeam(Bug Team)
    { 
        enemyBug = Team;
    }

    private void BattleBegin(bool Wild)
    {
        if (Wild)
        {
            enemyBug = BugBox.instance.GetWildBug();
            battleText.text = "You Found " + BugBox.instance.GetBugName(enemyBug.baseBugIndex) + "!";
        }
        currentState = BattleState.start;
        playerBug = PartyManager.instance.playerBugTeam[0];
        StartCoroutine(OpenBattle());
    }

    IEnumerator OpenBattle()
    {
        EventManager.instance.StopMovement();
        EventManager.instance.OpenBattleCanvas();
        battleCam.SetActive(true);
        battlePositions.SetActive(true);
        EventManager.instance.RefreshUI();

        yield return new WaitForSeconds(battleSpeed);

        OpenActionMenu();
        currentState = BattleState.moveSelection;
        StopCoroutine(OpenBattle());
    }

    private void OverWorld()
    {
        battleCam.SetActive(false);
        battlePositions.SetActive(false);
        EventManager.instance.StartMovement();
        EventManager.instance.CloseBattleCanvas();
    }

    private void OpenActionMenu()
    {
        battleText.text = "Chose An Action";
        actionSelector.SetActive(true);
    }

    private void CloseActionMenu()
    {
        actionSelector.SetActive(false);
    }

    private void PlayerAttackSelect(int Attack)
    {
        currentState = BattleState.attack;
        CloseActionMenu();
        moveSelector.SetActive(false);
        BattleItem selectedItem = playerBug.equippedItems[Attack];
        SpeedCheck(selectedItem, EnemyAttackSelect());
    }

    private BattleItem EnemyAttackSelect()
    {
        BattleItem[] enemyItems = new BattleItem[4];
        enemyItems = enemyBug.equippedItems;

        int rnd = Random.Range(0, enemyItems.Length);

        return enemyItems[rnd];
    }

    private void SpeedCheck(BattleItem PlayerAttack, BattleItem EnemyAttack)
    {
        if (playerBug.Speed > enemyBug.Speed)
        {
            StartCoroutine(PreformAttacks(playerBug, enemyBug, PlayerAttack, EnemyAttack));
        }
        else if (playerBug.Speed < enemyBug.Speed)
        {
            StartCoroutine(PreformAttacks(enemyBug, playerBug, EnemyAttack, PlayerAttack));
        }
        else
        {
            int rnd = Random.Range(1, 3);
            if (rnd == 1)
            {
                StartCoroutine(PreformAttacks(playerBug, enemyBug, PlayerAttack, EnemyAttack));
            }
            else if (rnd == 2)
            {
                StartCoroutine(PreformAttacks(enemyBug, playerBug, EnemyAttack, PlayerAttack));
            }
        }
    }

    private IEnumerator PreformAttacks(Bug TurnOneBug, Bug TurnTwoBug, BattleItem TurnOne, BattleItem TurnTwo)
    {
        //Turn 1
        //Update Text
        battleText.text = BugBox.instance.GetBugName(TurnOneBug.baseBugIndex) + " Used " + TurnOne.Name;

        //Damage Fomular
        //0.5 to give more reasonable numbers 
        float damageToDeal = ((0.5f * TurnOne.Damage * TurnOneBug.Attack) / TurnTwoBug.Defence);
        TurnTwoBug.currentHP = TurnTwoBug.currentHP - (int)damageToDeal;

        yield return new WaitForSeconds(battleSpeed);

        //Turn 2 
        if (!EndCheck())
        {
            battleText.text = BugBox.instance.GetBugName(TurnTwoBug.baseBugIndex) + " Used " + TurnTwo.Name;
            damageToDeal = ((0.5f * TurnTwo.Damage * TurnTwoBug.Attack) / TurnOneBug.Defence);
            TurnOneBug.currentHP = TurnOneBug.currentHP - (int)damageToDeal;
        }

        if (EndCheck()) { StopCoroutine(PreformAttacks(TurnOneBug, TurnTwoBug, TurnOne, TurnTwo)); }

        //restarts move selection if game has not ended 
        StopCoroutine(PreformAttacks(TurnOneBug, TurnTwoBug, TurnOne, TurnTwo));
        StartCoroutine(ReOpenActionMenu());
    }

    private IEnumerator ReOpenActionMenu()
    {
        yield return new WaitForSeconds(battleSpeed);
        OpenActionMenu();
        StopCoroutine(ReOpenActionMenu());
    }

    private bool EndCheck()
    {
        if (playerBug.currentHP <= 0)
        {
            battleText.text = "You Lost The Bug Battle";
            OnBattleEnd();
            return true;
        }
        else if (enemyBug.currentHP <= 0)
        {
            battleText.text = "You Won!";
            OnBattleEnd();
            return true;
        }
        return false;
    }

    public void OnClickFight()
    {
        StartCoroutine(Fight());
    }

    IEnumerator Fight()
    {
        yield return new WaitForSeconds(.5f);

        moveSelector.SetActive(true);
        EventManager.instance.RefreshUI();

        StopCoroutine(Fight());
    }

    public void OnClickCatch()
    {
        StartCoroutine(Catch());
    }

    IEnumerator Catch()
    {
        currentState = BattleState.battleEnd;
        CloseActionMenu();
        BugBox.instance.AddNewBug();
        battleText.text = "You Obtained " + BugBox.instance.GetBugName(enemyBug.baseBugIndex);
        yield return new WaitForSeconds(battleSpeed);
        OnBattleEnd();
        StopCoroutine(Catch());
    }

    public void OnClickRun()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        currentState = BattleState.battleEnd;
        CloseActionMenu();
        battleText.text = "You Ran From a Tiny Bug?";
        yield return new WaitForSeconds(battleSpeed);

        OnBattleEnd();
        currentState = BattleState.NON;
        StopCoroutine(Run());
    }

    private void OnBattleEnd()
    {
        StartCoroutine(BattleEnd());
    }

    IEnumerator BattleEnd()
    {
        currentState = BattleState.battleEnd;
        moveSelector.SetActive(false);
        yield return new WaitForSeconds(battleSpeed);

        OverWorld();
        currentState = BattleState.NON;
        StopCoroutine(BattleEnd());
    }
}
