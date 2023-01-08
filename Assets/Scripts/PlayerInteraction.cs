using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    protected InputManager inputManager;
    protected InputAction interact;

    protected InteractableObjects lastTouchedObjects;

    [SerializeField] protected AllReferencesObjects references;

    [SerializeField] private List<GameObject> images = new List<GameObject>();

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
            images[0].SetActive(true);
        }
        else if (collision.CompareTag("ZoneA"))
        {
            images[1].SetActive(true);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<InteractableObjects>() == lastTouchedObjects)
        {
            lastTouchedObjects = null;
            images[0].SetActive(false);
        }
        else if (collision.CompareTag("ZoneA"))
        {
            images[1].SetActive(false);
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
