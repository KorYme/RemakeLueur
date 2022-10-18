using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour , InteractableObjects
{
    private enum InteractionBehaviours
    {
        None,
        Destroy,
    }

    [SerializeField] private InteractionBehaviours behaviour = InteractionBehaviours.None;
    public float salut = 0;
    [DrawIf("behaviour", InteractionBehaviours.Destroy, DisablingType.Draw)][SerializeField] private GameObject objectToDestroy;

    public void Interact()
    {
        switch (behaviour)
        {
            case InteractionBehaviours.None:
                return;
            case InteractionBehaviours.Destroy:
                objectToDestroy.SetActive(false);
                return;
        }
    }
}
