using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerUI>(out PlayerUI player))
        {
            player.StartChangeScreenBrightness(1f);
        }
        else if (collision.gameObject.TryGetComponent<LooseRespawn>(out LooseRespawn looseRespawn))
        {
            looseRespawn.Respawn();
        }
    }
}
