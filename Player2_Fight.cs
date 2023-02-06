using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2_Fight : MonoBehaviour
{
    private int indexCharacter;
    GameObject godPlayer2;
    [SerializeField] private GameObject[] godsPrefabs;
    void Start()
    {
        indexCharacter = PlayerPrefs.GetInt("Player2-SelectChar");
        godPlayer2 = Instantiate(godsPrefabs[indexCharacter], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
