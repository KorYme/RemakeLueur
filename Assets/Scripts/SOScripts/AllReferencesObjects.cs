using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "AllReferencesObjects", menuName = "ScriptableObjects/AllReferences")]
public class AllReferencesObjects : ScriptableObject
{
    public Camera camera;
    public GameObject player;
    public PlayerMovement playerMovement;
    public InputManager inputManager;
    public FireBallThrow fireBallThrow;
    public PlayerInteraction playerInteraction;
    public SummonSmallCire summonSmallCire;
    public PlayerStats playerStats;
    public Light2D mainLight;
    public PauseManager pauseManager;
    public SoundManager soundManager;
}