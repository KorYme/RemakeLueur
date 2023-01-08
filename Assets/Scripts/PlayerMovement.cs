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

    protected bool jumpDelay;

    private Vector2 movementDirection;
    private bool flipedRight = true;
    private UnityAction AnimationChecks;
    private Vector2 velocity;

    [SerializeField] protected AllReferencesObjects references;
    public Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private float speed;
    [Range(0f,1f)][SerializeField] private float movementSmoothness;
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
        jumpDelay = false;
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
        rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(movementDirection.x * speed * Time.fixedDeltaTime, rb.velocity.y),
            ref velocity, movementSmoothness);
        animator.SetFloat("Speed", Mathf.Abs(movementDirection.x));
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
        if (jumpDelay) return;
        if (!TouchGround()) return;
        StartCoroutine(JumpDelayCoroutine());
        references.soundManager.jumpSound?.Invoke();
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
    }

    IEnumerator JumpDelayCoroutine()
    {
        jumpDelay = true;
        yield return new WaitForSeconds(0.5f);
        jumpDelay = false;
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

    public void KnockBack(Vector2 direction)
    {
        if (!enabled) return;
        Debug.Log(direction);
        rb.AddForce(direction, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centerPointGC.position, radiusGC);
    }
}
