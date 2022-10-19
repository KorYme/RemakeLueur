using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float basicDamage;
    [SerializeField] private Animator animator;
    [SerializeField] private AllReferencesObjects references;
    [SerializeField][DrawIf(true, DisablingType.ReadOnly)] private float currentHealth;
    private States cireState;

    public enum States
    {
        Healthy,
        Dead,
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float nbDamage = -1f)
    {
        currentHealth -= nbDamage < 0 ? basicDamage : nbDamage;
        UpdateState();
        UpdateHealthBar();
        if (cireState == States.Dead)
        {
            Death();
        }
    }

    private void Death()
    {
        references.fireBallThrow.enabled = false;
        references.playerMovement.enabled = false;
        references.summonSmallCire.enabled = false;
        animator.SetTrigger("Death");
    }

    public void AnimationAfterDeath()
    {
        Time.timeScale = 0;
        //Display death menu
    }

    private void UpdateState()
    {
        cireState = currentHealth <= 0 ? States.Dead : States.Healthy;
    }

    public void UpdateHealthBar()
    {
        //Behaviour HealthBar
    }
}
