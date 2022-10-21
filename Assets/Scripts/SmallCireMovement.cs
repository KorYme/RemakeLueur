using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCireMovement : PlayerMovement
{
    private SummonSmallCire player;

    protected override void OnEnable()
    {
        inputManager = references.inputManager;
        movement = inputManager.smallCireInputs.Player.Movement;
        movement?.Enable();
        jump = inputManager.smallCireInputs.Player.Jump;
        jump?.Enable();
        jump.performed += OnJump;
    }

    public void InitializeNewPlayer(SummonSmallCire summonSmallCire)
    {
        player = summonSmallCire;
        jumpSound = summonSmallCire.GetComponent<PlayerMovement>().jumpSound;
    }

    public override void Respawn(bool damaged = true)
    {
        player.RetakeControl();
        if (damaged)
        {
            references.playerStats.TakeDamage();
        }
        Destroy(gameObject);
    }
}
