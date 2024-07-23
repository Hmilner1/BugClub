using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostDisconnect : NetworkBehaviour
{
    private void Start()
    {
        // Subscribe to the host disconnection event (only on clients)
        if (!IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += HandleHostDisconnect;
        }
    }

    private void HandleHostDisconnect(ulong clientId)
    {
        if (clientId != NetworkManager.Singleton.LocalClientId) // Check if it's the host
        {
            // Load the new scene (replace "NewSceneName" with your actual scene name)
            SceneController.Instance.LoadMainGame();
        }
    }

    // Unsubscribe from the event when the component is destroyed to prevent memory leaks
    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null && !IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleHostDisconnect;
        }
    }
}

