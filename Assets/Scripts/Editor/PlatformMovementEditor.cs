using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlatformMovement))]
public class PlatformMovementEditor : Editor
{
    int index;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlatformMovement platformMovement = (PlatformMovement)target;
        GUILayout.BeginHorizontal();
        index = EditorGUILayout.IntSlider(index, 0, ((PlatformMovement)target).movementPoints.Count - 1);
        if (GUILayout.Button("Try Position"))
        {
            platformMovement.SetPositionPlatform(index);
        }
        if (GUILayout.Button("Update"))
        {
            platformMovement.UpdatePosition(index);
            platformMovement.SetPositionPlatform(index);
        }
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Create new point"))
        {
            platformMovement.CreateNewPoint();
            platformMovement.SetPositionPlatform(index);
        }
    }
}

