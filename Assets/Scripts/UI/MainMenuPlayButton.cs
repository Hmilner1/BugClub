using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPlayButton : MonoBehaviour
{
    public Vector3 origonalPos = new Vector3(.3f, .3f, .3f);
    public Vector3 bobOffset = new Vector3(.33f, .33f, .33f);
    public float duration = .5f;


    async void Awake()
    {
        await UnityServices.InitializeAsync();
    }

    private void Start()
    {
        Animate();
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

    private void Animate()
    {
        LeanTween.scale(gameObject, origonalPos + bobOffset, duration).setEaseInOutSine().setLoopPingPong();
    }
}
