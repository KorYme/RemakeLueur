using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    protected InputManager inputManager;
    protected InputAction movement;
    protected InputAction jump;

    private Vector2 movementDirection;
    private bool flipedRight = true;
    private UnityAction AnimationChecks;

    [SerializeField] protected AllReferencesObjects references;
    public Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] protected Transform centerPointGC;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radiusGC;
    public Transform lastCP;

    protected virtual void OnEnable()
    {
        inputManager = references.inputManager;
        movement = inputManager.playerInputs.Player.Movement;
        movement?.Enable();
        jump = inputManager.playerInputs.Player.Jump;
        jump?.Enable();
        jump.performed += OnJump;
    }

    private void OnDisable()
    {
        movement?.Disable();
        movementDirection = Vector2.zero;
        jump?.Disable();
    }

    protected void OnJump(InputAction.CallbackContext ctx) => Jump();

    private void Update()
    {
        AnimationChecks?.Invoke();
        movementDirection = movement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movementDirection.x * speed * Time.fixedDeltaTime, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        if ((rb.velocity.x > 0.01 && !flipedRight) || (rb.velocity.x < -0.01 && flipedRight))
        {
            Flip();
        }
    }

    public void AddCheckLanding()
    {
        AnimationChecks = CheckLanding;
    }

    private void CheckLanding()
    {
        if (rb.velocity.y <= 0)
        {
            animator.SetTrigger("Landing");
            AnimationChecks -= CheckLanding;
        }
    }

    public void AddCheckGround()
    {
        AnimationChecks = CheckGround;
    }

    private void CheckGround()
    {
        if (TouchGround())
        {
            animator.SetTrigger("Floor");
            AnimationChecks -= CheckGround;
        }
    }

    public bool TouchGround()
    {
        if (centerPointGC == null) return false;
        return Physics2D.OverlapCircle(centerPointGC.position, radiusGC, groundLayer);
    }

    private void Jump()
    {
        if (!TouchGround()) return;
        references.soundManager.jumpSound?.Invoke();
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
    }

    private void Flip()
    {
        flipedRight = !flipedRight;
        transform.rotation = flipedRight ? new Quaternion(0, 0, 0, 1) : new Quaternion(0, 1, 0, 0);
    }

    public void SetNewCP(Transform transformCP)
    {
        lastCP = transformCP;
    }

    public virtual void Respawn(bool damaged = true)
    {
        references.playerStats.TakeDamage();
        rb.isKinematic = true;
        transform.position = lastCP.position;
        rb.isKinematic = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centerPointGC.position, radiusGC);
    }
}
