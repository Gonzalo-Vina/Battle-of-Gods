using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Fight : MonoBehaviour
{
    private int indexCharacter;
    GameObject godPlayer1;
    [SerializeField] private GameObject[] godsPrefabs;
        
    void Start()
    {
        indexCharacter = PlayerPrefs.GetInt("Player1-SelectChar");
        godPlayer1 = Instantiate(godsPrefabs[indexCharacter], transform.position, Quaternion.identity);
        godPlayer1.gameObject.tag = "Player1";
    }

    void Update()
    {
        
    }
}
