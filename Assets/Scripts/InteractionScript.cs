using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour , InteractableObjects
{
    private enum Behaviours
    {
        None,
        Destroy,
    }

    [SerializeField] private Behaviours behaviour = Behaviours.None;
    [SerializeField] private GameObject waterFall;

    public void Interact()
    {
        switch (behaviour)
        {
            case Behaviours.None:
                return;
            case Behaviours.Destroy:
                waterFall.SetActive(false);
                return;
        }
    }
}
