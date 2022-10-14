using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllReferencesObjects", menuName = "ScriptableObjects/AllReferences")]
public class AllReferencesObjects : ScriptableObject
{
    public Camera camera;
    public GameObject player;
    public PlayerMovement playerMovement;
}
