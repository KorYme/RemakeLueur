using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour , InteractableObjects
{
    private enum InteractionBehaviours
    {
        None,
        Destroy,
        Animation,
        ScriptChanges,
        GameObjectChanges,
    }

    [SerializeField] private InteractionBehaviours behaviour = InteractionBehaviours.None;

    [DrawIf("behaviour", InteractionBehaviours.Destroy, ComparisonType.Equals, DisablingType.Draw)]
    [SerializeField] private GameObject objectToDestroy;

    [DrawIf("behaviour", InteractionBehaviours.Animation, ComparisonType.Equals, DisablingType.Draw)]
    [SerializeField] private Animator animator;
    [DrawIf("behaviour", InteractionBehaviours.Animation, ComparisonType.Equals, DisablingType.Draw)]
    [SerializeField] private string triggerToPlay;

    [DrawIf("behaviour", InteractionBehaviours.ScriptChanges, ComparisonType.Equals, DisablingType.Draw)]
    [SerializeField] private MonoBehaviour script;

    [DrawIf("behaviour", InteractionBehaviours.GameObjectChanges, ComparisonType.Equals, DisablingType.Draw)]
    [SerializeField] private GameObject objectToChange;

    [DrawIf("behaviour", new object[] { InteractionBehaviours.ScriptChanges, InteractionBehaviours.GameObjectChanges }, ComparisonType.Equals, DisablingType.Draw)]
    [SerializeField] private bool enable;

    public void Interact()
    {
        switch (behaviour)
        {
            case InteractionBehaviours.None:
                return;
            case InteractionBehaviours.Destroy:
                objectToDestroy.SetActive(false);
                return;
            case InteractionBehaviours.Animation:
                animator.SetTrigger(triggerToPlay);
                return;
            case InteractionBehaviours.ScriptChanges:
                script.enabled = enable;
                return;
            case InteractionBehaviours.GameObjectChanges:
                objectToChange.SetActive(enable);
                return;
        }
    }
}
