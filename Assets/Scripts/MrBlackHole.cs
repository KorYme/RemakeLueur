using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrBlackHole : MonoBehaviour
{
    [SerializeField] private AllReferencesObjects references;
    [SerializeField] private Vector2 rangeDetection;
    [SerializeField] private Vector2 minMaxIntensity;
    [SerializeField] private bool drawGizmos;
    [DrawIf("drawGizmos", true, ComparisonType.Equals, DisablingType.Draw, DisablingType.ReadOnly)][SerializeField] private Color furthestCircleColor;
    [DrawIf("drawGizmos", true, ComparisonType.Equals, DisablingType.Draw, DisablingType.ReadOnly)][SerializeField] private Color closestCircleColor;

    private void Update()
    {
        if (Vector2.Distance(references.player.transform.position, transform.position) > rangeDetection.y) return;
        references.mainLight.intensity = Mathf.Clamp((Vector2.Distance(references.player.transform.position, transform.position) - rangeDetection.x) / (rangeDetection.y - rangeDetection.x), minMaxIntensity.x, minMaxIntensity.y);
    }

    private void OnValidate()
    {
        if (rangeDetection.x > rangeDetection.y)
        {
            rangeDetection = new Vector2(rangeDetection.y, rangeDetection.x);
        }
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;
        Gizmos.color = furthestCircleColor;
        Gizmos.DrawWireSphere(transform.position, rangeDetection.y);
        Gizmos.color = closestCircleColor;
        Gizmos.DrawWireSphere(transform.position, rangeDetection.x);
    }
}
