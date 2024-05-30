using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPlayButton : MonoBehaviour
{
    async void Awake()
    {
        await UnityServices.InitializeAsync();
    }

    private void Update()
    {
        AccountCheck();
    }

    private void AccountCheck()
    {
        if (AuthenticationService.Instance.IsSignedIn == true)
        { 
            this.GetComponent<Button>().interactable= true;
        }
        else 
        {
            this.GetComponent<Button>().interactable = false;
        }
    }
}
