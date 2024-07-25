using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerEncounterOnline : NetworkBehaviour
{
    bool inGrass = false;

    private void OnEnable()
    {
        //EventManager.instance.OnStep.AddListener(PlayerStepTaken);
    }

    private void OnDisable()
    {
       // EventManager.instance.OnStep.RemoveListener(PlayerStepTaken);
    }

    public void PlayerStepTaken()
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
            EncounterTable table = other.GetComponent<EncounterTable>();
            EncounterManager encounterMan = GameObject.Find("EncounterManager").GetComponent<EncounterManager>();
            encounterMan.UpdateBugList(table.Encouters);
            encounterMan.UpdateMaxLvl(table.MinLvl, table.MaxLvl);
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
