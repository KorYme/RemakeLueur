using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCireInteraction : PlayerInteraction
{
    protected override void OnEnable()
    {
        inputManager = references.inputManager;
        interact = inputManager.smallCireInputs.Player.Interact;
        interact?.Enable();
        interact.performed += OnInteraction;
    }
}
