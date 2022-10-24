using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public enum SCENES
    {
        EntryMenu,
        GameOverMenu,
        Level1,
    }

    public void GoToScene(SCENES scene)
    {
        switch (scene)
        {
            case SCENES.EntryMenu:
                SceneManager.LoadScene("EntryMenu");
                return;
            case SCENES.GameOverMenu:
                SceneManager.LoadScene("GameOverMenu");
                return;
            case SCENES.Level1:
                SceneManager.LoadScene("Level1");
                return;
            default:
                return;
        }
    }
}
