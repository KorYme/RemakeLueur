using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerInputs playerInputs;
    [SerializeField] private AllReferencesObjects references;

    private void Awake()
    {
        playerInputs = new PlayerInputs();

        GetComponent<ReferencesSetter>().ReconnectValues(ReferencesSetter.ReferencesType.InputManager);
        references.playerMovement.enabled = false;
        references.playerMovement.enabled = true;
    }
}
