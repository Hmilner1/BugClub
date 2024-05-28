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
    public UnityEvent OnInteract;
    public UnityEvent OnPlayerLost;
    #endregion

    #region UI Events
    public UnityEvent OnOpenBattleCanvas;
    public UnityEvent OnCloseBattleCanvas;
    public UnityEvent OnRefreshParty;
    public UnityEvent OnRefreshUI;
    public UnityEvent OnBattleEnd;
    public UnityEvent OnCloseItemSelection;
    public UnityEvent OnPlayerInteractOverlap;
    public UnityEvent OnplayerStopInteract;
    public UnityEvent OnEnemyKnockedOut;
    public UnityEvent OnPlayerKnockedOut;
    public UnityEvent OnPlayerBugSwapped;
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

    public void OpenItemSelectionMenu()
    {
        OnCloseItemSelection?.Invoke();
    }

    public void InteractOverlap()
    {
        OnPlayerInteractOverlap?.Invoke();
    }

    public void InteractStop()
    {
        OnplayerStopInteract?.Invoke();
    }

    public void Interact()
    {
        OnInteract?.Invoke();
    }

    public void EnemyKnockedout()
    { 
        OnEnemyKnockedOut?.Invoke();
    }

    public void PlayerKnockedout()
    {
        OnPlayerKnockedOut?.Invoke();
    }

    public void PlayerBugSwapped()
    {
        OnPlayerBugSwapped?.Invoke();
    }

    public void PlayerLost()
    { 
        OnPlayerLost?.Invoke();
    }
}
