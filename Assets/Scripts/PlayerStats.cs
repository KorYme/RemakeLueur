using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float basicDamage;
    [SerializeField] private Animator animator;
    [SerializeField] private Image healthBar;
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
        references.menuManager.GoToScene("GameOverMenu");
    }

    private void UpdateState()
    {
        cireState = currentHealth <= 0 ? States.Dead : States.Healthy;
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
