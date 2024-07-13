using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerEncounterOnline : NetworkBehaviour
{
    bool inGrass = false;

    private void OnEnable()
    {
        EventManager.instance.OnStep.AddListener(PlayerStepTaken);
    }

    private void OnDisable()
    {
        EventManager.instance.OnStep.RemoveListener(PlayerStepTaken);
    }

    private void PlayerStepTaken()
    {
        if (!IsOwner) { return; }
        if (!inGrass) { return; }

        if (Random.Range(1, 100) <= 15)
        {
            EventManager.instance.BattleStart(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) { return; }
        if (other.tag == "Grass")
        {
            inGrass = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsOwner) { return; }
        if (other.tag == "grass")
        {
            inGrass = true;
        }
        else
        {
            inGrass = false;
        }
    }
}
