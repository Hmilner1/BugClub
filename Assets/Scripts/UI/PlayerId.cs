using TMPro;
using Unity.Services.Authentication;
using UnityEngine;

public class PlayerId : MonoBehaviour
{
    [SerializeField]
    private TMP_Text playerID;

    private void Start()
    {
        if (AuthenticationService.Instance == null)
        {
            playerID.text += "Not Logged In";
        }
        else
        {
            playerID.text += AuthenticationService.Instance.PlayerId;
        }
    }
}
