using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencesSetter : MonoBehaviour
{
    public enum ReferencesType
    {
        Camera,
        Player,
        PlayerMovement,
        InputManager,
    }

    [SerializeField] private AllReferencesObjects references;
    [SerializeField] private ReferencesType[] objects;

    private void Awake()
    {
        foreach (ReferencesType item in objects)
        {
            ConnectValues(item);
        }
    }

    public void ReconnectValues(ReferencesType item)
    {
        ConnectValues(item);
    }
    
    public void ReconnectValues(ReferencesType[] items)
    {
        foreach (ReferencesType item in items)
        {
            ConnectValues(item);
        }
    }

    private void ConnectValues(ReferencesType item)
    {
        switch (item)
        {
            case ReferencesType.Player:
                references.player = gameObject;
                return;
            case ReferencesType.Camera:
                references.camera = GetComponent<Camera>();
                return;
            case ReferencesType.PlayerMovement:
                references.playerMovement = GetComponent<PlayerMovement>();
                return;
            case ReferencesType.InputManager:
                references.inputManager = GetComponent<InputManager>();
                return;
            default:
                return;
        }
    }
}
