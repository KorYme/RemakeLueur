using System;
using UnityEngine;


/// <summary>
/// Has to be a serialized field
/// Don't draw the field if the condition is not filled
/// Else choose if it has to be readonly or simply drawned
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class DrawIfAttribute : PropertyAttribute
{
    public string comparedPropertyName;
    public object comparedValue;
    public DisablingType disablingType;
    public ComparisonType comparisonType;
    public bool simpleBoolean = false;

    public DrawIfAttribute(string comparedPropertyName, object comparedValue,
        DisablingType disablingType = DisablingType.Draw, ComparisonType comparisonType = ComparisonType.Equals)
    {
        this.comparedPropertyName = comparedPropertyName;
        this.comparedValue = comparedValue;
        this.disablingType = disablingType;
        this.comparisonType = comparisonType;
        simpleBoolean = false;
    }

    public DrawIfAttribute(bool simpleBoolean,
    DisablingType disablingType = DisablingType.Draw)
    {
        this.simpleBoolean = simpleBoolean;
        this.disablingType = disablingType;
    }
}

public enum ComparisonType
{
    Equals = 1,
    NotEqual = 2,
    GreaterThan = 3,
    SmallerThan = 4,
    SmallerOrEqual = 5,
    GreaterOrEqual = 6,
}

public enum DisablingType
{
    Draw = 1,
    ReadOnly = 2,
}
