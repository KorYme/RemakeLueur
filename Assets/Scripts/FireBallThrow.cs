using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;

public class FireBallThrow : MonoBehaviour
{
    [SerializeField] private AllReferencesObjects references;
    [SerializeField] private Transform fireBallSpawn;
    [SerializeField] private GameObject fireBallGameObject;
    [SerializeField] private float fireBallSpeed;
    [SerializeField] private float cdTimeFire;

    private float direction;
    private InputManager inputManager;
    private InputAction fire;
    private UnityAction CoolDownAction;
    private Cooldowns cdFire;

    private void Start()
    {
        InitializeInputs();
    }

    void InitializeInputs()
    {
        if (references.inputManager.playerInputs == null) return;
        inputManager = references.inputManager;
        fire = inputManager.playerInputs.Player.Fire;
        fire?.Enable();
        fire.performed += OnFire;
        cdFire = new Cooldowns(cdTimeFire);
    }

    private void Update()
    {
        CoolDownAction?.Invoke();
        if (cdFire.finished && CoolDownAction != null)
        {
            CoolDownAction -= cdFire.DecreaseCD;
        }
    }

    private void OnFire(InputAction.CallbackContext ctx) => Fire();

    void Fire()
    {
        if (!cdFire.finished) return;
        direction = transform.rotation == new Quaternion(0, 1, 0, 0) ? 1 : -1;
        GameObject fireBall = Instantiate(fireBallGameObject, fireBallSpawn.position, Quaternion.Euler(new Vector3(0,0,-90) * direction) );
        fireBall.GetComponent<Rigidbody2D>().velocity = Vector2.left * fireBallSpeed * direction;
        cdFire.ResetCD();
        CoolDownAction += cdFire.DecreaseCD;
    }
}
