using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;


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
    public class CustomBattle : UnityEvent<bool> { }
    public class CustomAttack : UnityEvent<int> { }


    #region Player Events
    public UnityEvent OnStep;
    public CustomBattle OnBattle = new CustomBattle();
    public UnityEvent OnOverWorld;
    public UnityEvent OnStopMovement;
    public UnityEvent OnStartMovement;
    public CustomAttack OnPreformAttack = new CustomAttack();
    #endregion

    #region UI Events
    public UnityEvent OnOpenBattleCanvas;
    public UnityEvent OnCloseBattleCanvas;
    public UnityEvent OnRefreshParty;
    public UnityEvent OnRefreshUI;
    public UnityEvent OnBattleEnd;
    #endregion

    public void Step()
    { 
        OnStep?.Invoke();
    }

    public void BattleStart(bool wild)
    {
        OnBattle?.Invoke(wild);
    }

    public void OverWorld()
    {
        OnOverWorld?.Invoke();
    }

    public void StopMovement()
    {
        OnStopMovement?.Invoke();
    }

    public void StartMovement()
    {
        OnStartMovement?.Invoke();
    }

    public void OpenBattleCanvas()
    {
        OnOpenBattleCanvas?.Invoke();
    }
    public void CloseBattleCanvas()
    {
        OnCloseBattleCanvas?.Invoke();
    }

    public void RefreshParty()
    { 
        OnRefreshParty?.Invoke();
    }

    public void RefreshUI()
    { 
        OnRefreshUI?.Invoke();
    }
    public void BattleEnd()
    {
        OnBattleEnd?.Invoke();
    }

    public void Attack(int Item)
    { 
        OnPreformAttack?.Invoke(Item);
    }

}
