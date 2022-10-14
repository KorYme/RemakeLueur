using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerInputs playerInputs;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        GetComponent<ReferencesSetter>().ReconnectValues(ReferencesSetter.ReferencesType.InputManager);
    }
}
