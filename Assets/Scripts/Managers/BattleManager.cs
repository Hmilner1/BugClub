using System.Collections;
using System.Collections.Generic;
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

    private Bug PlayerBug;
    private Bug EnemyBug;

    public int playerCurrentHealth;
    public int enemyCurrentHealth;

    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
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
        EventManager.instance.OnPreformAttack.AddListener(AttackSelected);
        EventManager.instance.OnBattleEnd.AddListener(OnBattleEnd);
    }

    private void OnDisable()
    {
        EventManager.instance.OnBattle.RemoveListener(BattleBegin);
        EventManager.instance.OnPreformAttack.RemoveListener(AttackSelected);
        EventManager.instance.OnBattleEnd.RemoveListener(OnBattleEnd);
    }

    private void Start()
    {
        currentState = BattleState.NON;
    }

    private void BattleBegin(bool Wild)
    {
        currentState = BattleState.start;
        PlayerBug = PartyManager.instance.playerBugTeam[0];
        EnemyBug = BugBox.instance.GetWildBug();
        playerCurrentHealth = PlayerBug.HP;
        enemyCurrentHealth = EnemyBug.HP;
        StartCoroutine(OpenBattle());
    }

    IEnumerator OpenBattle()
    {
        EventManager.instance.StopMovement();
        EventManager.instance.OpenBattleCanvas();
        battleCam.SetActive(true);
        battlePositions.SetActive(true);

        yield return new WaitForSeconds(.5f);

        OpenCloseActions();
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

    private void OpenCloseActions()
    {
        if (actionSelector.activeSelf == false)
        {
            actionSelector.SetActive(true);
        }
        else
        {
            actionSelector.SetActive(false);
        }
    }

    private void AttackSelected(int damage)
    {
        currentState = BattleState.attack;
        OpenCloseActions();
        SelectEnemyAttack();
        enemyCurrentHealth = enemyCurrentHealth - damage;
    }

    private void SelectEnemyAttack()
    {
        BattleItem[] enemyItems = new BattleItem[4];
        enemyItems = EnemyBug.equippedItems;

        int rnd = Random.Range(0, enemyItems.Length);
        playerCurrentHealth = playerCurrentHealth - enemyItems[rnd].Damage;
    }

    public void OnClickFight()
    {
        StartCoroutine(Fight());
    }

    IEnumerator Fight()
    {
        OpenCloseActions();
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
        OpenCloseActions();
        BugBox.instance.AddNewBug();
        yield return new WaitForSeconds(.5f);

        OverWorld();

        StopCoroutine(Catch());
    }

    public void OnClickRun()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        currentState = BattleState.battleEnd;
        OpenCloseActions();
        yield return new WaitForSeconds(.5f);

        OverWorld();
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
        yield return new WaitForSeconds(.5f);

        OverWorld();
        currentState = BattleState.NON;
        StopCoroutine(BattleEnd());
    }
}
