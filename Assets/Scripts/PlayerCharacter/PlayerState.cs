using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private PlayerStates currentState;

    [SerializeField]
    private GameObject battleCam;
    [SerializeField]
    private GameObject battlePositions;
    [SerializeField]
    private SpriteRenderer enemyPos;

    private enum PlayerStates
    { 
        overWorld,
        battle,
        dialogue,
        menu
    }

    private void OnEnable()
    {
        EventManager.instance.OnBattle.AddListener(BattleMode);
        EventManager.instance.OnOverWorld.AddListener(OverWorld);
    }

    private void OnDisable()
    {
        EventManager.instance.OnBattle.RemoveListener(BattleMode);
        EventManager.instance.OnOverWorld.RemoveListener(OverWorld);
    }

    private void Update()
    {
        switch (currentState) 
        {
            case PlayerStates.overWorld:

                break;
            case PlayerStates.battle:
                break;
            case PlayerStates.dialogue:

                break;
            case PlayerStates.menu:

                break;
        }
    }

    private void BattleMode(bool wild)
    {
        currentState = PlayerStates.battle;
        EventManager.instance.StopMovement();
        EventManager.instance.OpenBattleCanvas();
        battleCam.SetActive(true);
        battlePositions.SetActive(true);
    }

    private void OverWorld()
    { 
        currentState = PlayerStates.overWorld;
        battleCam.SetActive(false);
        battlePositions.SetActive(false);
        EventManager.instance.StartMovement();
        EventManager.instance.CloseBattleCanvas();
    }
}
