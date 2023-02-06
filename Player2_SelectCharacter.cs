using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2_SelectCharacter : MonoBehaviour
{
    [SerializeField] private Menu_GameController menuGameController;

    [HideInInspector] public int indexSelector;
    private float movX, movY;
    [SerializeField] private GameObject[] godsPrefabs;
    [SerializeField] Player1_SelectCharacter otherPlayerSelect;
    Color colorAlternative = new Color(1f, 0.7529f, 0.7529f, 1f);
    private GameObject godPlayer2;
    private SpriteRenderer spriteRendererGod;

    //UI
    [SerializeField] GameObject comingSoonSprite;

    void Start()
    {
        indexSelector = 1;
        godPlayer2 = Instantiate(godsPrefabs[indexSelector]);
        PlayerPrefs.SetInt("Player2-SelectChar", indexSelector);
        spriteRendererGod = godPlayer2.GetComponent<SpriteRenderer>();
        spriteRendererGod.flipX = true;
        godPlayer2.transform.position = new Vector3 (godPlayer2.transform.position.x * -1, godPlayer2.transform.position.y, godPlayer2.transform.position.z);
        comingSoonSprite.SetActive(true);

        menuGameController = FindObjectOfType<Menu_GameController>();
    }

    
    void Update()
    {
        movX = Input.GetAxis("Horizontal2");
        movY = Input.GetAxis("Vertical2");


        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            menuGameController.PlayAudioClip("audioSelection");
        }

        if (Input.anyKey) { 
            switch (indexSelector)
        {  
            //Hades
            case 0:
                if (movX > 0 && movY ==0)
                {
                    transform.position = new Vector3(-1.17f, 4.04f, -3.19f);
                    indexSelector = 1;
                    Destroy(godPlayer2);
                    godPlayer2 = Instantiate(godsPrefabs[indexSelector]);
                    PlayerPrefs.SetInt("Player2-SelectChar", indexSelector);
                    spriteRendererGod = godPlayer2.GetComponent<SpriteRenderer>();
                    spriteRendererGod.flipX = true;
                    godPlayer2.transform.position = new Vector3(godPlayer2.transform.position.x * -1, godPlayer2.transform.position.y, godPlayer2.transform.position.z);
                }
                if (movX == 0 && movY <0)
                {
                    transform.position = new Vector3(1.17f, 1.65f, -3.19f);
                    indexSelector = 2;
                    Destroy(godPlayer2);
                    godPlayer2 = Instantiate(godsPrefabs[indexSelector]);
                    PlayerPrefs.SetInt("Player2-SelectChar", indexSelector);
                    spriteRendererGod = godPlayer2.GetComponent<SpriteRenderer>();
                    spriteRendererGod.flipX = true;
                    godPlayer2.transform.position = new Vector3(godPlayer2.transform.position.x * -1, godPlayer2.transform.position.y, godPlayer2.transform.position.z);
                }
                if (otherPlayerSelect.indexSelector == indexSelector)
                {
                    godPlayer2.GetComponent<SpriteRenderer>().color = colorAlternative;
                }
                else
                {
                    godPlayer2.GetComponent<SpriteRenderer>().color = Color.white;
                }
                    comingSoonSprite.SetActive(true);
                    break;
            //Poseidon   
            case 1:
                if (movX < 0 && movY == 0)
                {
                    transform.position = new Vector3(1.17f, 4.04f, -3.19f);
                    indexSelector = 0;
                    Destroy(godPlayer2);
                    godPlayer2 = Instantiate(godsPrefabs[indexSelector]);
                    PlayerPrefs.SetInt("Player2-SelectChar", indexSelector);
                    spriteRendererGod = godPlayer2.GetComponent<SpriteRenderer>();
                    spriteRendererGod.flipX = true;
                    godPlayer2.transform.position = new Vector3(godPlayer2.transform.position.x * -1, godPlayer2.transform.position.y, godPlayer2.transform.position.z);
                }
                if (movX == 0 && movY < 0)
                {
                    transform.position = new Vector3(-1.17f, 1.65f, -3.19f);
                    indexSelector = 3;
                    Destroy(godPlayer2);
                    godPlayer2 = Instantiate(godsPrefabs[indexSelector]);
                    PlayerPrefs.SetInt("Player2-SelectChar", indexSelector);
                    spriteRendererGod = godPlayer2.GetComponent<SpriteRenderer>();
                    spriteRendererGod.flipX = true;
                    godPlayer2.transform.position = new Vector3(godPlayer2.transform.position.x * -1, godPlayer2.transform.position.y, godPlayer2.transform.position.z);
                }
                    if (otherPlayerSelect.indexSelector == indexSelector)
                    {
                        godPlayer2.GetComponent<SpriteRenderer>().color = colorAlternative;
                    }
                    else
                    {
                        godPlayer2.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    comingSoonSprite.SetActive(true);
                    break;
            //Ares
            case 2:
                if (movX > 0 && movY == 0)
                {
                    transform.position = new Vector3(-1.17f, 1.65f, -3.19f);
                    indexSelector = 3;
                    Destroy(godPlayer2);
                    godPlayer2 = Instantiate(godsPrefabs[indexSelector]);
                    PlayerPrefs.SetInt("Player2-SelectChar", indexSelector);
                    spriteRendererGod = godPlayer2.GetComponent<SpriteRenderer>();
                    spriteRendererGod.flipX = true;
                    godPlayer2.transform.position = new Vector3(godPlayer2.transform.position.x * -1, godPlayer2.transform.position.y, godPlayer2.transform.position.z);
                }
                if (movX == 0 && movY > 0)
                {
                    transform.position = new Vector3(1.17f, 4.04f, -3.19f);
                    indexSelector = 0;
                    Destroy(godPlayer2);
                    godPlayer2 = Instantiate(godsPrefabs[indexSelector]);
                    PlayerPrefs.SetInt("Player2-SelectChar", indexSelector);
                    spriteRendererGod = godPlayer2.GetComponent<SpriteRenderer>();
                    spriteRendererGod.flipX = true;
                    godPlayer2.transform.position = new Vector3(godPlayer2.transform.position.x * -1, godPlayer2.transform.position.y, godPlayer2.transform.position.z);
                }
                    if (otherPlayerSelect.indexSelector == indexSelector)
                    {
                        godPlayer2.GetComponent<SpriteRenderer>().color = colorAlternative;
                    }
                    else
                    {
                        godPlayer2.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    comingSoonSprite.SetActive(false);
                    break;
            //Zeus
            case 3:
                if (movX < 0 && movY == 0)
                {
                    transform.position = new Vector3(1.17f, 1.65f, -3.19f);
                    indexSelector = 2;
                    Destroy(godPlayer2);
                    godPlayer2 = Instantiate(godsPrefabs[indexSelector]);
                    PlayerPrefs.SetInt("Player2-SelectChar", indexSelector);
                    spriteRendererGod = godPlayer2.GetComponent<SpriteRenderer>();
                    spriteRendererGod.flipX = true;
                    godPlayer2.transform.position = new Vector3(godPlayer2.transform.position.x * -1, godPlayer2.transform.position.y, godPlayer2.transform.position.z);
                }
                if (movX == 0 && movY > 0)
                {
                    transform.position = new Vector3(-1.17f, 4.04f, -3.19f);
                    indexSelector = 1;
                    Destroy(godPlayer2);
                    godPlayer2 = Instantiate(godsPrefabs[indexSelector]);
                    PlayerPrefs.SetInt("Player2-SelectChar", indexSelector);
                    spriteRendererGod = godPlayer2.GetComponent<SpriteRenderer>();
                    spriteRendererGod.flipX = true;
                    godPlayer2.transform.position = new Vector3(godPlayer2.transform.position.x * -1, godPlayer2.transform.position.y, godPlayer2.transform.position.z);
                }
                    if (otherPlayerSelect.indexSelector == indexSelector)
                    {
                        godPlayer2.GetComponent<SpriteRenderer>().color = colorAlternative;
                    }
                    else
                    {
                        godPlayer2.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    comingSoonSprite.SetActive(true);
                    break;
            default:
                break;
        }
        }
    }
}
