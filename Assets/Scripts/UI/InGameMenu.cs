using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public void OnClickCloudSave()
    { 
        BugBox.instance.CloudSaveBugs();
    }
}
