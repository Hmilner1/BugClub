using UnityEngine;

public class TutorialLinks : MonoBehaviour
{


    public void OnClickOpenURL(string URL)
    {
        Application.OpenURL(URL);
    }
}
