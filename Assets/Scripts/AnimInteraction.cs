using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimInteraction : InteractionScript, InteractableObjects
{
    [SerializeField] private Animator thisAnimator;
    [SerializeField] private string triggerName;

    public override void Interact()
    {
        thisAnimator.SetTrigger(triggerName);
    }

    public void InteractInAnimation()
    {
        base.Interact();
    }
}
