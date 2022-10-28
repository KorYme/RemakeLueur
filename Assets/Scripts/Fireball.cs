using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fireball : MonoBehaviour
{
    [SerializeField] private LayerMask liana;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private float timeBeforeDestroy;

    private void Start()
    {
        Destroy(gameObject, timeBeforeDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((obstacles>>collision.gameObject.layer) % 2 == 1)
        {
            //Effets de particules
            Destroy(gameObject);
        }
        else if ((liana >> collision.gameObject.layer) % 2 == 1)
        {
            //Effets de particules
            collision.GetComponent<LianaBehaviour>().FadeOut();
            Destroy(gameObject);
        }
    }
}
