using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MrBlackHole : MonoBehaviour
{
    [SerializeField] private Light2D worldLight;
    [SerializeField] private Transform cire;
    [SerializeField] private Vector2 rangeDetection;
    [SerializeField] private bool drawGizmos;

    private void Update()
    {
        worldLight.intensity = Mathf.Clamp((Vector2.Distance(cire.position, transform.position) - rangeDetection.x) / (rangeDetection.y - rangeDetection.x), 0, 1);
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeDetection.y);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeDetection.x);
    }
}
