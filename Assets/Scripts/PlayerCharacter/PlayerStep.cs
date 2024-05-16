using UnityEngine;

public class PlayerStep : MonoBehaviour
{
    #region Events
    public delegate void Step();
    public static event Step OnStep;
    #endregion


    public void StepTaken()
    { 
        OnStep?.Invoke();
    }
}
