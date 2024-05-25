using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField]
    public InteractionType typeOfInteration;

    [SerializeField]
    public Interaction interaction;

    [SerializeField]
    public InteractionHeal healInteraction;

    public enum InteractionType
    { 
        Heal,
        Dialogue
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (typeOfInteration == InteractionType.Heal)
            {
                BugBox.instance.HealAll();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }


}


