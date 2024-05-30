using System.Text;
using System;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class AccountManager : MonoBehaviour
{
    public static AccountManager instance;

    [SerializeField]
    TMP_Text idText;
    [SerializeField]
    TMP_Text exceptionText;
    [SerializeField]
    GameObject signOutButton;
    [SerializeField]
    GameObject signInButton;

    async void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        await UnityServices.InitializeAsync();
        PlayerAccountService.Instance.SignedIn += SignInWithUnity;
        await SignInAsync();
        
    }

    async Task SignInAsync()
    {
        if (AuthenticationService.Instance.SessionTokenExists)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            signInButton.SetActive(false);
            UpdateUI();
        }
    }

    public async void StartSignInAsync()
    {
        if (PlayerAccountService.Instance.IsSignedIn)
        {
            SignInWithUnity();
            return;
        }

        try
        {
            await PlayerAccountService.Instance.StartSignInAsync();
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            SetException(ex);
        }

        
    }

    public void SignOut()
    {
        signOutButton.SetActive(false);
        signInButton.SetActive(true);
        AuthenticationService.Instance.SignOut();
        PlayerAccountService.Instance.SignOut();
        AuthenticationService.Instance.ClearSessionToken();
        UpdateUI();

    }

    public void OpenAccountPortal()
    {
        Application.OpenURL(PlayerAccountService.Instance.AccountPortalUrl);
    }

    async void SignInWithUnity()
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(PlayerAccountService.Instance.AccessToken);
            UpdateUI();
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            SetException(ex);
        }

    }

    void UpdateUI()
    {
        var statusBuilder = new StringBuilder();

        if (AuthenticationService.Instance.IsSignedIn)
        {
            signOutButton.SetActive(true);
            signInButton.SetActive(false);
            statusBuilder.AppendLine($"PlayerId: <b>{AuthenticationService.Instance.PlayerId}</b>");
        }

        idText.text = statusBuilder.ToString();
        SetException(null);
    }

    void SetException(Exception ex)
    {
        exceptionText.text = ex != null ? $"{ex.GetType().Name}: {ex.Message}" : "";
    }

    public bool SignedIn()
    {
        if (AuthenticationService.Instance.PlayerId == null)
        {
            return false;
        }
        else if (AuthenticationService.Instance.PlayerId != null)
        {
            return true;
        }
        return false;
    }

}