using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

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
}
