using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    private InputAction pause;
    private InputManager inputManager;
    [SerializeField] private AllReferencesObjects references;
    [SerializeField] private GameObject pauseMenu;


    private void OnEnable()
    {
        if (references.inputManager.playerInputs == null) return;
        inputManager = references.inputManager;
        pause = inputManager.playerInputs.Player.Pause;
        pause?.Enable();
        pause.performed += OnPause;
    }

    private void OnDisable()
    {
        pause?.Disable();
    }

    private void OnPause(InputAction.CallbackContext ctx) => Pause();

    public void Pause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = 1 - Time.timeScale;
    }
}
