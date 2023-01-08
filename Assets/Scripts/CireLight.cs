using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CireLight : MonoBehaviour
{
    [SerializeField] private AllReferencesObjects references;
    [SerializeField] private Light2D personalLight;
    [SerializeField] private float speedAnimation = 2.5f;
    [SerializeField] private float deltaAnimation = 0.1f;

    float time;
    float initialSize;

    private void Awake()
    {
        initialSize = personalLight.pointLightOuterRadius;
        time = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
        personalLight.intensity = 1 - references.mainLight.intensity;
        personalLight.pointLightOuterRadius = initialSize + Mathf.Sin(time*speedAnimation)*deltaAnimation;
    }
}
