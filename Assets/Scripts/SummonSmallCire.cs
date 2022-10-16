using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SummonSmallCire : MonoBehaviour
{
    [SerializeField] private AllReferencesObjects references;
    [SerializeField] private ReferencesSetter referencesSetter;
    [SerializeField] private Transform spawnNewSmallCire;
    [SerializeField] private GameObject smallCireGameObject;

    private InputAction summon;

    private void OnEnable()
    {
        summon = references.inputManager.playerInputs.Player.Summon;
        summon?.Enable();
        summon.performed += OnSummon;
    }

    private void OnDisable()
    {
        summon?.Disable();
    }

    private void OnSummon(InputAction.CallbackContext ctx) => SummonAndGiveControl();

    public void SummonAndGiveControl()
    {
        referencesSetter.EnableOrDisableAllValues(false, new List<ReferencesSetter.ReferencesType> { ReferencesSetter.ReferencesType.Player, ReferencesSetter.ReferencesType.SummonSmallCire });
        GameObject smallCire = Instantiate(smallCireGameObject, spawnNewSmallCire.position, Quaternion.identity);
        smallCire.GetComponent<SmallCireMovement>().InitializeNewPlayer(this);
    }

    public void RetakeControl()
    {
        referencesSetter.ReconnectAllValues();
        referencesSetter.EnableOrDisableAllValues(true);
    }
}
