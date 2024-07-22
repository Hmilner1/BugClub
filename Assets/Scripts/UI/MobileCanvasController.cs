using UnityEngine;

public class MobileCanvasController : MonoBehaviour
{
    [SerializeField]
    private Canvas mobileCanvas;

    private void OnEnable()
    {
        EventManager.instance.OnSettingsUpdated.AddListener(UpdateUI);
    }

    private void OnDisable()
    {
        EventManager.instance.OnSettingsUpdated.RemoveListener(UpdateUI);
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        SettingsData data = SettingsManager.Instance.LoadData();

        if (data.OnScreenControls == true)
        {
            mobileCanvas.enabled = true;
        }
        else if (data.OnScreenControls == false)
        {
            mobileCanvas.enabled = false;

        }
    }
}
