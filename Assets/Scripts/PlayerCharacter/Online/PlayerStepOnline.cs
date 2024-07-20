using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStepOnline : NetworkBehaviour
{
    [SerializeField]
    private PlayerEncounterOnline playerEncounterOnline;
    public void StepTaken()
    {
        if (!IsOwner) { return; }
        // EventManager.instance.Step();
        playerEncounterOnline.PlayerStepTaken();
    }
}
