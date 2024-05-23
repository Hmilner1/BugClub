using System.Collections;
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

    private void BattleBegin(bool Wild)
    {
        currentState = BattleState.start;
        playerBug = PartyManager.instance.playerBugTeam[0];
        enemyBug = BugBox.instance.GetWildBug();
        battleText.text = "You Found " + BugBox.instance.GetBugName(enemyBug.baseBugIndex) + "!";
        StartCoroutine(OpenBattle());
    }

    IEnumerator OpenBattle()
    {
        EventManager.instance.StopMovement();
        EventManager.instance.OpenBattleCanvas();
        EventManager.instance.RefreshUI();
        battleCam.SetActive(true);
        battlePositions.SetActive(true);

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

    private void SpeedCheck(BattleItem PlayerAttack, BattleItem EnemyAttack)
    {
        if (playerBug.Speed > enemyBug.Speed)
        {
            StartCoroutine(PreformAttacks(playerBug,enemyBug, PlayerAttack, EnemyAttack));
        }
        else
        {
            StartCoroutine(PreformAttacks(enemyBug,playerBug,EnemyAttack,PlayerAttack));
        }
    }

    private IEnumerator PreformAttacks(Bug TurnOneBug,Bug TurnTwoBug,BattleItem TurnOne, BattleItem TurnTwo)
    {
        battleText.text = BugBox.instance.GetBugName(TurnOneBug.baseBugIndex) + " Used " + TurnOne.Name;
        TurnTwoBug.currentHP = TurnOneBug.currentHP - TurnOne.Damage;
        if (EndCheck()) { StopCoroutine(PreformAttacks(TurnOneBug, TurnTwoBug, TurnOne, TurnTwo)); }

        yield return new WaitForSeconds(battleSpeed);

        if (!EndCheck())
        {
            battleText.text = BugBox.instance.GetBugName(TurnTwoBug.baseBugIndex) + " Used " + TurnTwo.Name;

            TurnOneBug.currentHP = TurnOneBug.currentHP - TurnTwo.Damage;
        }
        if (EndCheck()) { StopCoroutine(PreformAttacks(TurnOneBug, TurnTwoBug, TurnOne, TurnTwo)); }

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
