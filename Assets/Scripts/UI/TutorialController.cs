using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public List<GameObject> Tabs = new List<GameObject>();

    private void Start()
    {
        foreach (var tab in Tabs)
        {
            tab.gameObject.SetActive(false);
        }
    }

    public void OnClickOpenInfo(GameObject Panel)
    {
        foreach (var tab in Tabs)
        {
            tab.gameObject.SetActive(false);
        }
        Panel.SetActive(true);

    }
}
