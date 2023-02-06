using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Projectile_Manager : MonoBehaviour
{

    [SerializeField] private Transform launchProjectileTransform;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject projectilePrefeb;
    [SerializeField] private float velProjectile;
    

    void Start()
    {
       
        
    }

    
    void Update()
    {
        if(projectile!= null)
        {
            if (Time.timeScale > 0)
            {
                projectile.transform.Translate(Vector2.right * velProjectile * Time.deltaTime);
                projectile.transform.eulerAngles += new Vector3(0, 0, -1);
                if (projectile.transform.position.y < 0) { Destroy(projectile); }
            }
        }
        
    }
    
    void CreateProyectil()
    {
        projectile = Instantiate(projectilePrefeb, launchProjectileTransform.position, launchProjectileTransform.rotation);
    }

    
}
