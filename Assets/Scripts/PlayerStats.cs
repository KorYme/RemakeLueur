using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
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

    public void TakeDamage(float nbDamage)
    {
        currentHealth -= nbDamage;
        UpdateState();
        if (cireState == States.Dead)
        {
            Death();
        }
    }

    private void Death()
    {
        //Behaviour Death
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
