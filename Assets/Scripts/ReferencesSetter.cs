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
        FireBallThrow,
        PlayerInteraction,
        SummonSmallCire,
    }

    [SerializeField] private AllReferencesObjects references;
    [SerializeField] private ReferencesType[] referencesTypes;

    private void Awake()
    {
        foreach (ReferencesType item in referencesTypes)
        {
            ConnectValues(item);
        }
    }

    public void ReconnectValues(ReferencesType item)
    {
        ConnectValues(item);
    }
    
    public void ReconnectAllValues()
    {
        foreach (ReferencesType item in referencesTypes)
        {
            ConnectValues(item);
        }
    }

    public void EnableOrDisableAllValues(bool enable, List<ReferencesType> referencesTypesNoUse = null)
    {
        foreach (ReferencesType item in referencesTypes)
        {
            if (referencesTypesNoUse.Contains(item)) continue;
            switch (item)
            {
                case ReferencesType.Player:
                    references.player.SetActive(enable);
                    return;
                case ReferencesType.Camera:
                    references.camera.enabled = enable;
                    return;
                case ReferencesType.PlayerMovement:
                    references.playerMovement.enabled = enable;
                    return;
                case ReferencesType.InputManager:
                    references.inputManager.enabled = enable;
                    return;
                case ReferencesType.FireBallThrow:
                    references.fireBallThrow.enabled = enable;
                    return;
                case ReferencesType.PlayerInteraction:
                    references.playerInteraction.enabled = enable;
                    return;
                case ReferencesType.SummonSmallCire:
                    references.summonSmallCire.enabled = enable;
                    return;
                default:
                    return;
            }
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
            case ReferencesType.FireBallThrow:
                references.fireBallThrow = GetComponent<FireBallThrow>();
                return;
            case ReferencesType.PlayerInteraction:
                references.playerInteraction = GetComponent<PlayerInteraction>();
                return;
            case ReferencesType.SummonSmallCire:
                references.summonSmallCire = GetComponent<SummonSmallCire>();
                return;
            default:
                return;
        }
    }
}
