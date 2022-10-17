using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class SummonSmallCire : MonoBehaviour
{
    [SerializeField] private AllReferencesObjects references;
    [SerializeField] private ReferencesSetter referencesSetter;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform spawnNewSmallCire;
    [SerializeField] private GameObject smallCireGameObject;
    [SerializeField] private float cdSummonTimer;

    private bool isSummoned;
    private GameObject smallCire;
    private InputAction summon;
    private PlayerMovement playerMovement;
    private Cooldowns cdSummon;

    private void Start()
    {
        cdSummon = new(cdSummonTimer);
    }

    private void Update()
    {
        cdSummon.DecreaseCD();
    }

    private void OnEnable()
    {
        summon = references.inputManager.playerInputs.Player.Summon;
        summon?.Enable();
        summon.performed += OnSummon;
        isSummoned = false;
        playerMovement = references.playerMovement;
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
        else if (playerMovement.TouchGround() && playerMovement.rb.velocity.magnitude <= 0.1f && cdSummon.finished)
        {
            isSummoned = true;
            references.playerMovement.rb.bodyType = RigidbodyType2D.Static;
            EnableScripts(false);
            animator.SetFloat("Speed", 0f);
            animator.SetTrigger("ForceIdle");
            smallCire = Instantiate(smallCireGameObject, spawnNewSmallCire.position, Quaternion.identity);
            smallCire.GetComponent<SmallCireMovement>().InitializeNewPlayer(this);
        }
    }

    public void RetakeControl()
    {
        cdSummon.ResetCD();
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
