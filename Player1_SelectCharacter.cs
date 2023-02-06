using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_SelectCharacter : MonoBehaviour
{
    [SerializeField] private Menu_GameController menuGameController;

    [HideInInspector] public int indexSelector;
    private float movX, movY;
    [SerializeField] GameObject[] godsPrefabs;
    [SerializeField] Player2_SelectCharacter otherPlayerSelect;
    Color colorAlternative = new Color(0.5098f, 0.7450f, 1f, 1f);
    GameObject godPlayer1;

    //UI
    [SerializeField] GameObject comingSoonSprite;

    void Start()
    {
        indexSelector = 0;
        godPlayer1 = Instantiate(godsPrefabs[indexSelector]);
        PlayerPrefs.SetInt("Player1-SelectChar", indexSelector);
        comingSoonSprite.SetActive(true);
        menuGameController = FindObjectOfType<Menu_GameController>();
    }

    
    void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movY = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        { 
            menuGameController.PlayAudioClip("audioSelection"); 
        }


        if (Input.anyKey)
        {
            switch (indexSelector)
            {
                //Hades
                case 0:
                    if (movX > 0 && movY == 0)
                    {
                        transform.position = new Vector3(-1.17f, 4.04f, -3.19f);
                        indexSelector = 1;
                        Destroy(godPlayer1);
                        godPlayer1 = Instantiate(godsPrefabs[indexSelector]);
                        PlayerPrefs.SetInt("Player1-SelectChar", indexSelector);
                    }
                    if (movX == 0 && movY < 0)
                    {
                        transform.position = new Vector3(1.17f, 1.65f, -3.19f);
                        indexSelector = 2;
                        Destroy(godPlayer1);
                        godPlayer1 = Instantiate(godsPrefabs[indexSelector]);
                        PlayerPrefs.SetInt("Player1-SelectChar", indexSelector);
                    }
                    if (otherPlayerSelect.indexSelector == indexSelector)
                    {
                        godPlayer1.GetComponent<SpriteRenderer>().color = colorAlternative;
                    }
                    else
                    {
                        godPlayer1.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    comingSoonSprite.SetActive(true);
                    break;
                //Poseidon   
                case 1:
                    if (movX < 0 && movY == 0)
                    {
                        transform.position = new Vector3(1.17f, 4.04f, -3.19f);
                        indexSelector = 0;
                        Destroy(godPlayer1);
                        godPlayer1 = Instantiate(godsPrefabs[indexSelector]);
                        PlayerPrefs.SetInt("Player1-SelectChar", indexSelector);
                    }
                    if (movX == 0 && movY < 0)
                    {
                        transform.position = new Vector3(-1.17f, 1.65f, -3.19f);
                        indexSelector = 3;
                        Destroy(godPlayer1);
                        godPlayer1 = Instantiate(godsPrefabs[indexSelector]);
                        PlayerPrefs.SetInt("Player1-SelectChar", indexSelector);
                    }
                    if (otherPlayerSelect.indexSelector == indexSelector)
                    {
                        godPlayer1.GetComponent<SpriteRenderer>().color = colorAlternative;
                    }
                    else
                    {
                        godPlayer1.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    comingSoonSprite.SetActive(true);
                    break;
                //Ares
                case 2:
                    if (movX > 0 && movY == 0)
                    {
                        transform.position = new Vector3(-1.17f, 1.65f, -3.19f);
                        indexSelector = 3;
                        Destroy(godPlayer1);
                        godPlayer1 = Instantiate(godsPrefabs[indexSelector]);
                        PlayerPrefs.SetInt("Player1-SelectChar", indexSelector);
                    }
                    if (movX == 0 && movY > 0)
                    {
                        transform.position = new Vector3(1.17f, 4.04f, -3.19f);
                        indexSelector = 0;
                        Destroy(godPlayer1);
                        godPlayer1 = Instantiate(godsPrefabs[indexSelector]);
                        PlayerPrefs.SetInt("Player1-SelectChar", indexSelector);
                    }
                    if (otherPlayerSelect.indexSelector == indexSelector)
                    {
                        godPlayer1.GetComponent<SpriteRenderer>().color = colorAlternative;
                    }
                    else
                    {
                        godPlayer1.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    comingSoonSprite.SetActive(false);
                    break;
                //Zeus
                case 3:
                    if (movX < 0 && movY == 0)
                    {
                        transform.position = new Vector3(1.17f, 1.65f, -3.19f);
                        indexSelector = 2;
                        Destroy(godPlayer1);
                        godPlayer1 = Instantiate(godsPrefabs[indexSelector]);
                        PlayerPrefs.SetInt("Player1-SelectChar", indexSelector);
                    }
                    if (movX == 0 && movY > 0)
                    {
                        transform.position = new Vector3(-1.17f, 4.04f, -3.19f);
                        indexSelector = 1;
                        Destroy(godPlayer1);
                        godPlayer1 = Instantiate(godsPrefabs[indexSelector]);
                        PlayerPrefs.SetInt("Player1-SelectChar", indexSelector);
                    }
                    if (otherPlayerSelect.indexSelector == indexSelector)
                    {
                        godPlayer1.GetComponent<SpriteRenderer>().color = colorAlternative;
                    }
                    else
                    {
                        godPlayer1.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    comingSoonSprite.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        

    }

    
}
