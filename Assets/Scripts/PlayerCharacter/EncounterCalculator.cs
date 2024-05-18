using UnityEngine;

public class EncounterCalculator : MonoBehaviour
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
        if (!inGrass) { return; }

        if (Random.Range(1, 100) <= 15)
        {
            EventManager.instance.Battle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grass")
        {
            inGrass = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "grass") 
        {
            inGrass = true;
        }
        else 
        {
            inGrass= false;
        }
    }
  
}
