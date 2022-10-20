using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DrawIfAttribute))]
public class DrawIfPropertyDrawer : PropertyDrawer
{
    private DrawIfAttribute drawIfAttribute;

    SerializedProperty comparedField;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if ((attribute as DrawIfAttribute).simpleBoolean ? false : !ShowMe(property))
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
                return CompareInt(drawIfAttribute.comparisonType);
            case "float":
            case "double":
                return CompareFloat(drawIfAttribute.comparisonType);
            default:
                Debug.Log("The type " + comparedField.type + " of this field is not supported");
                return false;
        }
    }

    private bool CompareInt(ComparisonType comparisonType)
    {
        return comparisonType switch
        {
            ComparisonType.Equals => comparedField.intValue.Equals(drawIfAttribute.comparedValue),
            ComparisonType.NotEqual => !comparedField.intValue.Equals(drawIfAttribute.comparedValue),
            ComparisonType.GreaterThan => comparedField.intValue > (int)drawIfAttribute.comparedValue,
            ComparisonType.SmallerThan => comparedField.intValue < (int)drawIfAttribute.comparedValue,
            ComparisonType.SmallerOrEqual => comparedField.intValue <= (int)drawIfAttribute.comparedValue,
            ComparisonType.GreaterOrEqual => comparedField.intValue >= (int)drawIfAttribute.comparedValue,
            _ => false,
        };
    }

    private bool CompareFloat(ComparisonType comparisonType)
    {
        return comparisonType switch
        {
            ComparisonType.Equals => comparedField.floatValue.Equals(drawIfAttribute.comparedValue),
            ComparisonType.NotEqual => !comparedField.floatValue.Equals(drawIfAttribute.comparedValue),
            ComparisonType.GreaterThan => comparedField.floatValue > (float)drawIfAttribute.comparedValue,
            ComparisonType.SmallerThan => comparedField.floatValue < (float)drawIfAttribute.comparedValue,
            ComparisonType.SmallerOrEqual => comparedField.floatValue <= (float)drawIfAttribute.comparedValue,
            ComparisonType.GreaterOrEqual => comparedField.floatValue >= (float)drawIfAttribute.comparedValue,
            _ => false,
        };
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        drawIfAttribute = attribute as DrawIfAttribute;
        if ((attribute as DrawIfAttribute).simpleBoolean ? true : ShowMe(property))
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
