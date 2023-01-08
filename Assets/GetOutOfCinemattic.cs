using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GetOutOfCinemattic : MonoBehaviour
{
    [SerializeField] private InputActionReference skipPlayButtons;
    [SerializeField] private string nextLevel;
    [SerializeField] private VideoPlayer videoPlayer;

    private void Awake()
    {
        if (videoPlayer.clip == null)
        {
            SceneManager.LoadScene(nextLevel);
        }
        skipPlayButtons.action.Enable();
        videoPlayer.loopPointReached += LaunchLevel;
        skipPlayButtons.action.started += LaunchLevel;
    }

    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= LaunchLevel;
        skipPlayButtons.action.started -= LaunchLevel;
        skipPlayButtons.action.Disable();
    }

    private void LaunchLevel(UnityEngine.Video.VideoPlayer source)
    {
        SceneManager.LoadScene(nextLevel);
    }

    private void LaunchLevel(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene(nextLevel);
    }
}
