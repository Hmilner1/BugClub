using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class EncounterCalculator : MonoBehaviour
{
    bool inGrass = false;
    [SerializeField]
    private GameObject battleCam;
    [SerializeField]
    private GameObject battlePositions;



    private void OnEnable()
    {
        PlayerStep.OnStep += PlayerStepTaken;
    }

    private void OnDisable()
    {
        PlayerStep.OnStep -= PlayerStepTaken;
    }

    private void PlayerStepTaken()
    {
        if (!inGrass) { return; }

        if (Random.Range(1, 100) <= 30)
        {
            Debug.Log("Wild Encounter");
           
            
            battlePositions.SetActive(true);
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
