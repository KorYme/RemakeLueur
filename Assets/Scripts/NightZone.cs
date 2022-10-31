using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightZone : MonoBehaviour
{
    [SerializeField] private AllReferencesObjects references;
    [Range(0,1)][SerializeField] private float minimumLight;
    [SerializeField] private float timeTransition;
    private bool isInside;
    private bool justOut;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInside = true;
            justOut = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInside = false;
            justOut = true;
        }
        
    }

    private void Update()
    {
        if (isInside)
        {
            references.mainLight.intensity -= Time.deltaTime / timeTransition;
            if (minimumLight >= references.mainLight.intensity)
            {
                references.mainLight.intensity = minimumLight;
                isInside = false;
            }
        }
        else if (justOut)
        {
            references.mainLight.intensity = Mathf.Clamp(references.mainLight.intensity + (Time.deltaTime / timeTransition), minimumLight, 1);
            if (references.mainLight.intensity == 1)
            {
                justOut = false;
            }
        }
    }
}
