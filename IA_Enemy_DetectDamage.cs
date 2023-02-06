using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Enemy_DetectDamage : MonoBehaviour
{
    public bool enemyHit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1")) { enemyHit = true; }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1")) { enemyHit = false; }
    }
}
