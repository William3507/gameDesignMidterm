using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeBehavior : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        playerController player = collision.gameObject.GetComponent<playerController>();
        if (player != null)
        {
            player.Hurt();
            return;
        }
    }
}
