using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    protected InputManager inputManager;
    protected InputAction interact;

    protected InteractableObjects lastTouchedObjects;

    [SerializeField] protected AllReferencesObjects references;

    protected virtual void OnEnable()
    {
        inputManager = references.inputManager;
        interact = inputManager.playerInputs.Player.Interact;
        interact?.Enable();
        interact.performed += OnInteraction;
    }

    protected void OnDisable()
    {
        interact?.Disable();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<InteractableObjects>() != null)
        {
            lastTouchedObjects = collision.GetComponent<InteractableObjects>();
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<InteractableObjects>() == lastTouchedObjects)
        {
            lastTouchedObjects = null;
        }
    }

    protected void OnInteraction(InputAction.CallbackContext ctx) => InteractWith();

    protected void InteractWith()
    {
        if (lastTouchedObjects == null) return;
        lastTouchedObjects.Interact();
        lastTouchedObjects = null;
    }
}
