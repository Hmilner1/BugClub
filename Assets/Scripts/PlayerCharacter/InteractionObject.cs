using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionObject : MonoBehaviour
{
    [SerializeField]
    public InteractionType typeOfInteration;
    private bool isActive;

    public enum InteractionType
    { 
        Heal,
        Online,
        Dialogue
    }

    private void Start()
    {
        isActive = false;
    }

    private void OnEnable()
    {
        EventManager.instance.OnInteract.AddListener(Interact);
    }

    private void OnDisable()
    {
        EventManager.instance.OnInteract.RemoveListener(Interact);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.instance.InteractOverlap();
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EventManager.instance.InteractStop();
        isActive = false;
    }

    private void Interact()
    {
        if (!isActive) { return; }
        
        if (typeOfInteration == InteractionType.Heal)
        {
            BugBox.instance.HealAll();

        }
        else if (typeOfInteration == InteractionType.Online)
        {
            SceneController.Instance.LoadScene("MultiplayerScene");
            SceneController.Instance.LoadSceneAdditive("Multiplayer Lobby");
        }
    }
}


