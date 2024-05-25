using UnityEngine;

public class InteractionHeal : Interaction
{

    public override void Interact()
    {
        BugBox.instance.HealAll();
    }
}
