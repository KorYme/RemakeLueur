using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DrawIfAttribute))]
public class DrawIfPropertyDrawer : PropertyDrawer
{
    private DrawIfAttribute drawIfAttribute;

    SerializedProperty comparedField;

    private float propertyHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!ShowMe(property))
        {
            return 0f;
        }
        return base.GetPropertyHeight(property,label);
    }

    public bool ShowMe(SerializedProperty property)
    {
        drawIfAttribute = attribute as DrawIfAttribute;
        string path = property.propertyPath.Contains(".") ? 
            System.IO.Path.ChangeExtension(property.propertyPath, drawIfAttribute.comparedPropertyName) : 
            drawIfAttribute.comparedPropertyName;
        comparedField = property.serializedObject.FindProperty(path);

        if (comparedField == null)
        {
            Debug.LogError("Cannot find property with name: " + path);
            return false;
        }
        switch (comparedField.type)
        {
            case "bool":
                return comparedField.boolValue.Equals(drawIfAttribute.comparedValue);
            case "Enum":
                return comparedField.enumValueIndex.Equals((int)drawIfAttribute.comparedValue);
            case "int":
            case "long":
            case "byte":
            case "short":
            case "uint":
                return comparedField.intValue.Equals(drawIfAttribute.comparedValue);
            case "float":
            case "double":
                return comparedField.floatValue.Equals(drawIfAttribute.comparedValue);
            default:
                Debug.Log("The type " + comparedField.type + " of this field is not supported");
                return false;
        }
    }

    private bool CompareInt(ComparisonType comparisonType)
    {
        switch (comparisonType)
        {
            case ComparisonType.Equals:
                return comparedField.intValue.Equals(drawIfAttribute.comparedValue);
            case ComparisonType.NotEqual:
                return !comparedField.intValue.Equals(drawIfAttribute.comparedValue);
            case ComparisonType.GreaterThan:
                return false;
            case ComparisonType.SmallerThan:
                return false;
            case ComparisonType.SmallerOrEqual:
                return false;
            case ComparisonType.GreaterOrEqual:
                return false;
            default:
                return false;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (ShowMe(property))
        {
            if (drawIfAttribute.disablingType == DisablingType.ReadOnly)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, label);
                GUI.enabled = true;
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }

}
