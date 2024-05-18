using UnityEngine;

public class PlayerStep : MonoBehaviour
{

    public void StepTaken()
    {
        EventManager.instance.Step();
    }
}
