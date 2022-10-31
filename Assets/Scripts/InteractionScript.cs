using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionScript : MonoBehaviour , InteractableObjects
{
    protected enum InteractionBehaviours
    {
        None,
        Destroy,
        Animation,
        ScriptChanges,
        GameObjectChanges,
        ChangeLevel,
    }

    [SerializeField] protected InteractionBehaviours behaviour = InteractionBehaviours.None;

    [DrawIf("behaviour", InteractionBehaviours.Destroy, ComparisonType.Equals, DisablingType.Draw)]
    [SerializeField] protected GameObject objectToDestroy;

    [DrawIf("behaviour", InteractionBehaviours.Animation, ComparisonType.Equals, DisablingType.Draw)]
    [SerializeField] protected Animator animator;
    [DrawIf("behaviour", InteractionBehaviours.Animation, ComparisonType.Equals, DisablingType.Draw)]
    [SerializeField] protected string triggerToPlay;

    [DrawIf("behaviour", InteractionBehaviours.ScriptChanges, ComparisonType.Equals, DisablingType.Draw)]
    [SerializeField] protected MonoBehaviour script;

    [DrawIf("behaviour", InteractionBehaviours.GameObjectChanges, ComparisonType.Equals, DisablingType.Draw)]
    [SerializeField] protected GameObject objectToChange;

    [DrawIf("behaviour", new object[] { InteractionBehaviours.ScriptChanges, InteractionBehaviours.GameObjectChanges }, ComparisonType.Equals, DisablingType.Draw)]
    [SerializeField] protected bool enable;

    [DrawIf("behaviour", InteractionBehaviours.ChangeLevel, ComparisonType.Equals, DisablingType.Draw)]
    [SerializeField] protected string levelToLoad;

    public virtual void Interact()
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
            case InteractionBehaviours.ChangeLevel:
                SceneManager.LoadScene(levelToLoad);
                return;
        }
    }
}
