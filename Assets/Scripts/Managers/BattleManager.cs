using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public List<Bug> playerBug { get; private set; }
    public List<Bug> enemyBug { get; private set; }

    [SerializeField]
    private TMP_Text battleText;
    [SerializeField]
    private float battleSpeed;
    [SerializeField]
    private GameObject PartyPanel;
    [SerializeField]
    private Button runButton;
    [SerializeField]
    private Button catchButton;

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
        EventManager.instance.OnPlayerBugSwapped.AddListener(SwapPlayerBug);
    }

    private void OnDisable()
    {
        EventManager.instance.OnBattle.RemoveListener(BattleBegin);
        EventManager.instance.OnPreformAttack.RemoveListener(PlayerAttackSelect);
        EventManager.instance.OnPlayerBugSwapped.RemoveListener(SwapPlayerBug);
    }

    private void Start()
    {
        currentState = BattleState.NON;
    }

    public void PopulateTrainerTeam(List<Bug> Team)
    { 
        enemyBug = Team;
    }

    private void BattleBegin(bool Wild)
    {
        if (Wild)
        {
            enemyBug = new List<Bug> { };
            enemyBug.Add(BugBox.instance.GetWildBug());
            battleText.text = "You Found " + BugBox.instance.GetBugName(enemyBug[0].baseBugIndex) + "!";
            runButton.interactable = true;
            catchButton.interactable = true;
        }
        else 
        {
            runButton.interactable = false;
            catchButton.interactable = false;
            battleText.text = "You Are Challenged To A Bug Battle!";
        }
        currentState = BattleState.start;
        playerBug = PartyManager.instance.playerBugTeam;
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
        BattleItem selectedItem = playerBug[0].equippedItems[Attack];
        SpeedCheck(selectedItem, EnemyAttackSelect());
    }

    private BattleItem EnemyAttackSelect()
    {
        BattleItem[] enemyItems = new BattleItem[4];
        enemyItems = enemyBug[0].equippedItems;

        int rnd = Random.Range(0, enemyItems.Length);

        return enemyItems[rnd];
    }

    private void SpeedCheck(BattleItem PlayerAttack, BattleItem EnemyAttack)
    {
        if (playerBug[0].Speed > enemyBug[0].Speed)
        {
            StartCoroutine(PreformAttacks(playerBug[0], enemyBug[0], PlayerAttack, EnemyAttack));
        }
        else if (playerBug[0].Speed < enemyBug[0].Speed)
        {
            StartCoroutine(PreformAttacks(enemyBug[0], playerBug[0], EnemyAttack, PlayerAttack));
        }
        else
        {
            int rnd = Random.Range(1, 3);
            if (rnd == 1)
            {
                StartCoroutine(PreformAttacks(playerBug[0], enemyBug[0], PlayerAttack, EnemyAttack));
            }
            else if (rnd == 2)
            {
                StartCoroutine(PreformAttacks(enemyBug[0], playerBug[0], EnemyAttack, PlayerAttack));
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
        if (!KnockoutCheck())
        {
            battleText.text = BugBox.instance.GetBugName(TurnTwoBug.baseBugIndex) + " Used " + TurnTwo.Name;
            damageToDeal = ((0.5f * TurnTwo.Damage * TurnTwoBug.Attack) / TurnOneBug.Defence);
            TurnOneBug.currentHP = TurnOneBug.currentHP - (int)damageToDeal;
            if (KnockoutCheck()) { StopCoroutine(PreformAttacks(TurnOneBug, TurnTwoBug, TurnOne, TurnTwo)); }
        }


        //restarts move selection if game has not ended 
        StopCoroutine(PreformAttacks(TurnOneBug, TurnTwoBug, TurnOne, TurnTwo));
        StartCoroutine(ReOpenActionMenu());
    }

    private IEnumerator EnemyPreformAttackOnly(BattleItem ItemUsed)
    {
        yield return new WaitForSeconds(battleSpeed);

        battleText.text = BugBox.instance.GetBugName(enemyBug[0].baseBugIndex) + " Used " + ItemUsed.Name;
        float damageToDeal = ((0.5f * ItemUsed.Damage * enemyBug[0].Attack) / playerBug[0].Defence);
        playerBug[0].currentHP = playerBug[0].currentHP - (int)damageToDeal;
        if (KnockoutCheck()) { StopCoroutine(EnemyPreformAttackOnly(ItemUsed)); }
        StopCoroutine(EnemyPreformAttackOnly(ItemUsed));
        StartCoroutine(ReOpenActionMenu());
    }

    private IEnumerator ReOpenActionMenu()
    {
        yield return new WaitForSeconds(battleSpeed);
        OpenActionMenu();
        StopCoroutine(ReOpenActionMenu());
    }

    private bool KnockoutCheck()
    {
        if (playerBug[0].currentHP <= 0)
        {
            battleText.text = BugBox.instance.GetBugName(playerBug[0].baseBugIndex) + "Has Been KnockedOut";
            if (!EndCheck())
            {
                PartyPanel.SetActive(true);
                PartyCanvasController canvasController = PartyPanel.GetComponent<PartyCanvasController>();
                canvasController.UpdateBattleParty();
            }
            return true;
        }
        else if (enemyBug[0].currentHP <= 0)
        {
            battleText.text = BugBox.instance.GetBugName(enemyBug[0].baseBugIndex) + "Has Been KnockedOut";
            if (!EndCheck())
            {
                SwapEnemyBug();
            }
            return true;
        }
        return false;
    }

    private bool EndCheck()
    {
        int playerBugAmount = playerBug.Count;
        int enemyBugAmount = enemyBug.Count;
        Debug.Log(playerBug.Count);
        foreach (Bug bug in playerBug)
        {
            if (bug.currentHP <= 0)
            {
                playerBugAmount -= 1;
                Debug.Log(playerBugAmount);

                if (playerBugAmount == 0)
                {
                    battleText.text = "You Lost The Bug Battle";
                    OnBattleEnd();
                    EventManager.instance.PlayerLost();
                    return true;
                }
            }
        }
        foreach (Bug bug in enemyBug)
        {
            if (bug.currentHP <= 0)
            {
                enemyBugAmount -= 1;
                if (enemyBugAmount == 0)
                {
                    battleText.text = "You Won!";
                    OnBattleEnd();
                    return true;
                }
            }
        }
        return false;
    }

    private void SwapEnemyBug()
    {
        if (enemyBug.Count > 1)
        { 
            Bug tempBug = enemyBug[0];
            for (int i = 0; i < enemyBug.Count; i++)
            {
                if (enemyBug[i].currentHP > 0)
                {
                    enemyBug[0] = enemyBug[i];
                    enemyBug[i] = tempBug;
                    EventManager.instance.EnemyKnockedout();
                    return;
                }
            }
        }
    }

    private void SwapPlayerBug()
    {
        if (playerBug[0].currentHP <= 0)
        {
            playerBug[0] = PartyManager.instance.playerBugTeam[0];
            PartyCanvasController canvasController = PartyPanel.GetComponent<PartyCanvasController>();
            canvasController.RemoveBattleParty();
            PartyPanel.SetActive(false);
        }
        else
        {
            playerBug[0] = PartyManager.instance.playerBugTeam[0];
            PartyCanvasController canvasController = PartyPanel.GetComponent<PartyCanvasController>();
            canvasController.RemoveBattleParty();
            PartyPanel.SetActive(false);
            StartCoroutine(EnemyPreformAttackOnly(EnemyAttackSelect()));
        }
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
        battleText.text = "You Obtained " + BugBox.instance.GetBugName(enemyBug[0].baseBugIndex);
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
