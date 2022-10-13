using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputAction movement;
    private InputAction jump;

    private Vector2 movementDirection;
    private bool flipedRight;
    private Transform lastCP;

    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform centerPointGC;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radiusGC;


    private void Awake()
    {
        playerInputs = new PlayerInputs();
        flipedRight = true;
    }

    private void OnEnable()
    {
        movement = playerInputs.Player.Movement;
        movement?.Enable();
        jump = playerInputs.Player.Jump;
        jump?.Enable();
        jump.performed += OnJump;
    }

    private void OnDisable()
    {
        movement?.Disable();
        jump?.Disable();
    }

    private void OnJump(InputAction.CallbackContext ctx) => Jump();

    private void Update()
    {
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

    private void Jump()
    {
        if (!Physics2D.OverlapCircle(centerPointGC.position, radiusGC, groundLayer)) return;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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

    public void Respawn()
    {
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
