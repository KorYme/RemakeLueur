using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireBallThrow : MonoBehaviour
{
    [SerializeField] private AllReferencesObjects references;
    [SerializeField] private Transform fireBallSpawn;
    [SerializeField] private GameObject fireBallGameObject;
    [SerializeField] private float fireBallSpeed;

    private float direction;
    private InputManager inputManager;
    private InputAction fire;

    private void Start()
    {
        inputManager = references.inputManager;
        fire = inputManager.playerInputs.Player.Fire;
        fire?.Enable();
        fire.performed += OnFire;
    }

    private void OnFire(InputAction.CallbackContext ctx) => Fire();

    void Fire()
    {
        direction = transform.rotation == new Quaternion(0, 1, 0, 0) ? 1 : -1;
        GameObject fireBall = Instantiate(fireBallGameObject, fireBallSpawn.position, Quaternion.Euler(new Vector3(0,0,-90) * direction) );
        fireBall.GetComponent<Rigidbody2D>().velocity = Vector2.left * fireBallSpeed * direction;
    }
}
