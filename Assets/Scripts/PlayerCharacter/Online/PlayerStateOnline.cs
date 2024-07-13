using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStateOnline : NetworkBehaviour
{
    public static PlayerStates currentState;

    public enum PlayerStates
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
        if (!IsOwner) { return; }
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
        if (!IsOwner) { return; }

        currentState = PlayerStates.battle;
    }

    private void OverWorld()
    {
        if (!IsOwner) { return; }
        currentState = PlayerStates.overWorld;
    }

}
