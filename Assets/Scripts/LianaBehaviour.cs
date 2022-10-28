using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LianaBehaviour : MonoBehaviour
{
    [SerializeField] private AllReferencesObjects references;
    [SerializeField] private Animator animator;
    [SerializeField] private float knockbackForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == references.player)
        {
            if (collision.gameObject.GetComponent<PlayerStats>())
            {
                references.playerStats.TakeDamage();
                Vector2 direction = new Vector2(collision.transform.position.x > transform.position.x ? 1f : -1f, .2f);
                references.playerMovement.KnockBack(direction.normalized * knockbackForce);
            }
            else
            {
                references.playerMovement.Respawn();
            }
        }
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
        enabled = false;
    }
}
