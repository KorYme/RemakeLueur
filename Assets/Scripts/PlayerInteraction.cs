using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private InputManager inputManager;
    private InputAction interact;

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

    private void OnInteraction(InputAction.CallbackContext ctx) => Interact();

    private void Interact()
    {

    }
}
