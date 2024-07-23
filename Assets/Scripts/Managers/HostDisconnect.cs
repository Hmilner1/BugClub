using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostDisconnect : NetworkBehaviour
{
    private void Start()
    {
        if (!IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += HandleHostDisconnect;
        }
    }

    private void HandleHostDisconnect(ulong clientId)
    {
        if (clientId != NetworkManager.Singleton.LocalClientId)
        {
            NetworkManager.Singleton.Shutdown();
            SceneController.Instance.LoadMainGame();
        }
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null && !IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleHostDisconnect;
        }
    }
}

