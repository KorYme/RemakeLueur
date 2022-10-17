using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class SummonSmallCire : MonoBehaviour
{
    [SerializeField] private AllReferencesObjects references;
    [SerializeField] private ReferencesSetter referencesSetter;
    [SerializeField] private Transform spawnNewSmallCire;
    [SerializeField] private GameObject smallCireGameObject;
    private GameObject smallCire;

    private bool isSummoned;
    private InputAction summon;

    private void OnEnable()
    {
        summon = references.inputManager.playerInputs.Player.Summon;
        summon?.Enable();
        summon.performed += OnSummon;
        isSummoned = false;
    }

    private void OnDisable()
    {
        summon?.Disable();
    }

    private void OnSummon(InputAction.CallbackContext ctx) => SummonAndGiveControl();

    public void SummonAndGiveControl()
    {
        if (isSummoned)
        {
            smallCire.GetComponent<SmallCireMovement>().Respawn();
        }
        else
        {
            isSummoned = true;
            references.playerMovement.rb.bodyType = RigidbodyType2D.Static;
            EnableScripts(false);
            GetComponent<Animator>().SetFloat("Speed", 0f);
            smallCire = Instantiate(smallCireGameObject, spawnNewSmallCire.position, Quaternion.identity);
            smallCire.GetComponent<SmallCireMovement>().InitializeNewPlayer(this);
        }
    }

    public void RetakeControl()
    {
        references.playerMovement.rb.bodyType = RigidbodyType2D.Dynamic;
        referencesSetter.ReconnectAllValues();
        EnableScripts(true);
        isSummoned = false;
    }

    private void EnableScripts(bool enable)
    {
        references.playerMovement.enabled = enable;
        references.fireBallThrow.enabled = enable;
        //references.playerInteraction.enabled = enable;
    }
}
