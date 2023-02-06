using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fight_GameController : MonoBehaviour
{
    [SerializeField] Player1_Movements scriptPlayer1;
    [SerializeField] IA_Enemy_Movement scriptPlayer2;

    [SerializeField] Slider slideLifePlayer1;
    [SerializeField] Slider slideLifePlayer2;

    [SerializeField] GameObject panelMenu;
    [SerializeField] GameObject panelExit;
    [SerializeField] GameObject panelGameOver;


    [Header("Audio Clips")]
    private AudioSource audioSource;
    private AudioClip audioTemp;
    [SerializeField] AudioClip audioOpenMenu;
    [SerializeField] AudioClip audioExitYes;
    [SerializeField] AudioClip audioExitNo;

    [SerializeField] AudioSource MusicAmbient;
    private bool changeAudioClip;
    [SerializeField] AudioClip musicMain;
    [SerializeField] AudioClip musicFinal;

    [Header("Animator Canvas")]
    [SerializeField] Animator animatorCanvas;

    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;
        Time.timeScale = 1;

    }

    void Start()
    {
        scriptPlayer1 = GameObject.FindObjectOfType<Player1_Movements>();
        scriptPlayer2 = GameObject.FindObjectOfType<IA_Enemy_Movement>();
        scriptPlayer1.enabled= false;
        scriptPlayer2.enabled= false;
        panelMenu.SetActive(false);
        panelExit.SetActive(false);
        panelGameOver.SetActive(false);
        MusicAmbient.clip = musicMain;
        MusicAmbient.Play();
        changeAudioClip = false;
        audioSource = GetComponent<AudioSource>();
        animatorCanvas.Play("FadeOutFight - SceneLoad");
        StartCoroutine("StartScene");
    }

    
    void Update()
    {
        CheckLifePlayers();

        if (panelMenu.activeSelf && !panelGameOver.activeSelf) { Time.timeScale = 0; }
        if (!panelMenu.activeSelf && !panelGameOver.activeSelf) { Time.timeScale = 1; }

        if (Input.GetKeyDown(KeyCode.Escape)) { ChangeStateMenu(); }

        if (panelGameOver.activeSelf && !changeAudioClip) 
        {
            MusicAmbient.Stop();
            MusicAmbient.clip = musicFinal;
            MusicAmbient.Play();
            changeAudioClip = true;
        }
    }
    public void ChangeStateMenu()
    {
        panelMenu.SetActive(!panelMenu.activeSelf);
        panelExit.SetActive(false);
        PlayAudioClip("audioOpenMenu");
    }
    public void PlayAudioClip(string audioClip)
    {
        if (audioClip == "audioOpenMenu") { audioTemp = audioOpenMenu; }
        if (audioClip == "audioExitYes") { audioTemp = audioExitYes; }
        if (audioClip == "audioExitNo") { audioTemp = audioExitNo; }


        audioSource.PlayOneShot(audioTemp);
    }
    public void CheckLifePlayers()
    {
        slideLifePlayer1.value = scriptPlayer1.GetLife();
        slideLifePlayer2.value = scriptPlayer2.GetLife();
    }
    public void ChangeSceneMenuSelection()
    {
        StartCoroutine("LoadScene");
    }
    public void ChangeStateMenuExit()
    {
        panelExit.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSecondsRealtime(2f);
        scriptPlayer1.enabled = true;
        scriptPlayer2.enabled = true;
    }

    IEnumerator LoadScene()
    {
        scriptPlayer1.enabled = false;
        scriptPlayer2.enabled = false;
        PlayerPrefs.SetInt("MenuAgain", 1);
        animatorCanvas.Play("FadeInFight - SceneLoad");
        panelMenu.SetActive(false);
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("MenuSelectionCharacter");
    }
}
