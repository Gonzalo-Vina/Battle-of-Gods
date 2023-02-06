using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Projectile : MonoBehaviour
{

    [SerializeField] private int damageProjectile;
    [SerializeField] private bool enemyHit;
    private GameObject player2;
    private IA_Enemy_Movement scriptPlayer2; // Ésto hay que cambiarlo cuando agreguemos el player2, ya que al eliminar el "TestEnemy" nos va a dar error

    private void Start()
    {
        damageProjectile = 20;
        player2 = GameObject.FindGameObjectWithTag("Player2");
        scriptPlayer2 = player2.GetComponent<IA_Enemy_Movement>(); // Ésto hay que cambiarlo cuando agreguemos el player2, ya que al eliminar el "TestEnemy" nos va a dar error
    }

    private void DealDamage(int damage2)
    {
            scriptPlayer2.TakeDamage(damage2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player2"))
        {
            if (scriptPlayer2.invulnerable == false) { DealDamage(damageProjectile); }
        }
    }
}
