using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionObject : MonoBehaviour
{
    [SerializeField]
    public InteractionType typeOfInteration;

    public enum InteractionType
    { 
        Heal,
        Online,
        Dialogue
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
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EventManager.instance.InteractStop();
    }

    private void Interact()
    {
        switch (typeOfInteration)
        {
            case InteractionType.Heal:
                BugBox.instance.HealAll();
                break;
            case InteractionType.Online:
                SceneController.Instance.LoadScene("MultiplayerScene");
                break;
            case InteractionType.Dialogue:
                break;
        }
    }


}


