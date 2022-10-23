using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float timeBeforeFall;
    [SerializeField] private float timeBeforeRespawn;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Cooldowns cdFallingPlatform;
    private Cooldowns cdRespawnPlatform;
    private UnityAction CDManager;
    private bool isFalling;

    private void Start()
    {
        cdFallingPlatform = new Cooldowns(timeBeforeFall);
        cdRespawnPlatform = new Cooldowns(timeBeforeRespawn);
        cdFallingPlatform.ResetCD();
        cdRespawnPlatform.ResetCD();
        rb.bodyType = RigidbodyType2D.Static;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        CDManager?.Invoke();
        if (cdFallingPlatform.finished)
        {
            CDManager -= cdFallingPlatform.DecreaseCD;
            cdFallingPlatform.ResetCD();
            BeginFalling();
        }
        if (cdRespawnPlatform.finished)
        {
            cdRespawnPlatform.ResetCD();
            CDManager -= cdRespawnPlatform.DecreaseCD;
            RespawnPlatform();
        }
    }

    public void BeginFalling()
    {
        animator.SetTrigger("Fall");
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void StartCDRespawn()
    {
        CDManager += cdRespawnPlatform.DecreaseCD;
    }

    public void RespawnPlatform()
    {
        isFalling = false;
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Respawn");
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            CDManager += cdFallingPlatform.DecreaseCD;
            isFalling = true;
        }
    }
}
