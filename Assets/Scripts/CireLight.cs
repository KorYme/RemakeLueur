using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CireLight : MonoBehaviour
{
    [SerializeField] private AllReferencesObjects references;
    [SerializeField] private Light2D personalLight;
    [SerializeField] private float speedAnimation = 2.5f;

    float time = 0;

    private void Update()
    {
        time += Time.deltaTime;
        personalLight.intensity = 1 - references.mainLight.intensity;
        personalLight.pointLightOuterRadius = 0.9f + Mathf.Sin(time*speedAnimation)/10f;
    }
}
