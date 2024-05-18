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
    public class Battle : UnityEvent<bool>
    { 
    }

    #region Player Events
    public UnityEvent OnStep;
    public Battle OnBattle = new Battle();
    public UnityEvent OnOverWorld;
    public UnityEvent OnStopMovement;
    public UnityEvent OnStartMovement;
    #endregion

    #region UI Events
    public UnityEvent OnOpenBattleCanvas;
    public UnityEvent OnCloseBattleCanvas;

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
}
