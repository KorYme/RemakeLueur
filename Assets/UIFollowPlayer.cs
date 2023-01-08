using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class UIFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    void LateUpdate()
    {
        transform.position = player.position;
    }
}
