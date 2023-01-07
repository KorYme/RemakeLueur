using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MrBlackHole))]
public class MrBlackHoleEditor : Editor
{
    bool showPosition = true;

    SerializedProperty scriptProp;
    SerializedProperty worldLightProp;
    SerializedProperty referencesProp;
    SerializedProperty rangeDetectionProp;
    SerializedProperty minMaxIntensityProp;
    SerializedProperty drawGizmosProp;
    SerializedProperty furthestCircleColorProp;
    SerializedProperty closestCircleColorProp;

    private void OnEnable()
    {
        worldLightProp = serializedObject.FindProperty("worldLight");
        referencesProp = serializedObject.FindProperty("references");
        rangeDetectionProp = serializedObject.FindProperty("rangeDetection");
        minMaxIntensityProp = serializedObject.FindProperty("minMaxIntensity");
        drawGizmosProp = serializedObject.FindProperty("drawGizmos");
        furthestCircleColorProp = serializedObject.FindProperty("furthestCircleColor");
        closestCircleColorProp = serializedObject.FindProperty("closestCircleColor");
    }

    public override void OnInspectorGUI()
    {
        MrBlackHole script = (MrBlackHole)target;
        using (new EditorGUI.DisabledScope(true)) EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), GetType(), false);
        
        EditorGUILayout.PropertyField(referencesProp, new GUIContent("References"));
        EditorGUILayout.PropertyField(rangeDetectionProp, new GUIContent("Range Detection"));
        EditorGUILayout.PropertyField(minMaxIntensityProp, new GUIContent("Min Max Intensity"));

        showPosition = EditorGUILayout.BeginFoldoutHeaderGroup(showPosition, "Gizmos");
        if (showPosition)
        {
            EditorGUILayout.PropertyField(drawGizmosProp, new GUIContent("Draw Gizmos"));
            EditorGUILayout.PropertyField(furthestCircleColorProp, new GUIContent("Furthest Circle Color"));
            EditorGUILayout.PropertyField(closestCircleColorProp, new GUIContent("Closest Circle Color"));
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
