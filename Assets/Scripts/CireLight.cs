using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CireLight : MonoBehaviour
{
    [SerializeField] private AllReferencesObjects references;
    [SerializeField] private Light2D personalLight;

    private void Update()
    {
        personalLight.intensity = 1 - references.mainLight.intensity;
    }
}
