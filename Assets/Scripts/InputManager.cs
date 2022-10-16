using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerInputs playerInputs;
    public PlayerInputs smallCireInputs;
    [SerializeField] private AllReferencesObjects references;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        smallCireInputs = new PlayerInputs();

        GetComponent<ReferencesSetter>().ReconnectValues(ReferencesSetter.ReferencesType.InputManager);
        references.playerMovement.enabled = true;
        references.fireBallThrow.enabled = true;
        references.summonSmallCire.enabled = true;
    }
}
