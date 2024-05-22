using System.Collections;
using UnityEngine;
using static PlayerState;

public class BattleManager : MonoBehaviour
{
    public BattleState currentState;

    public enum BattleState { start, moveSelection, attack, battleEnd, NON}

    [SerializeField]
    private GameObject battleCam;
    [SerializeField]
    private GameObject battlePositions;
    [SerializeField]
    private GameObject actionSelector;

    private void OnEnable()
    {
        EventManager.instance.OnBattle.AddListener(BattleBegin);
    }

    private void OnDisable()
    {
        EventManager.instance.OnBattle.RemoveListener(BattleBegin);
    }

    private void Start()
    {
        currentState= BattleState.NON;
    }

    private void BattleBegin(bool Wild)
    { 
        currentState= BattleState.start;
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

        StopCoroutine(Run());
    }

}
