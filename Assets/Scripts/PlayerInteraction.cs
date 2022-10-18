using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private InputManager inputManager;
    private InputAction interact;

    private InteractableObjects lastTouchedObjects;

    [SerializeField] private AllReferencesObjects references;

    private void OnEnable()
    {
        if (references.inputManager.playerInputs == null) return;
        inputManager = references.inputManager;
        interact = inputManager.playerInputs.Player.Interact;
        interact?.Enable();
        interact.performed += OnInteraction;
    }

    private void OnDisable()
    {
        interact?.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<InteractableObjects>() != null)
        {
            lastTouchedObjects = collision.GetComponent<InteractableObjects>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<InteractableObjects>() == lastTouchedObjects)
        {
            lastTouchedObjects = null;
        }
    }

    private void OnInteraction(InputAction.CallbackContext ctx) => InteractWith();

    private void InteractWith()
    {
        if (lastTouchedObjects == null) return;
        lastTouchedObjects.Interact();
        lastTouchedObjects = null;
    }
}
