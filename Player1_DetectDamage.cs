using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_DetectDamage : MonoBehaviour
{
    public bool enemyHit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player2")) { enemyHit = true; }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player2")) { enemyHit = false; }
    }
}
