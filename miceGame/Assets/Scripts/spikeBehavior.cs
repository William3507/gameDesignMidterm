using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeBehavior : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        playerController pController = other.gameObject.GetComponent<PlatformerController2D>();
        if (pController != null)
        {
            pController.TakeDamage();
            return;
        }
    }
}
