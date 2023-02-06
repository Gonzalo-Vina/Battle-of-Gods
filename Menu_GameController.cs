using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_GameController : MonoBehaviour
{
    [Header("Scripts to Players")]
    [SerializeField] Player1_SelectCharacter scriptPlayer1;
    [SerializeField] Player2_SelectCharacter scriptPlayer2;

    [Header("UI Reference")]
    [SerializeField] GameObject buttonStart;
    [SerializeField] GameObject panelMenu;
    [SerializeField] GameObject panelFadeOut;


    [Header("Audio Clips")]
    private AudioSource audioSource;
    private AudioClip audioTemp;
    [SerializeField] AudioClip audioSelection;
    [SerializeField] AudioClip audioStart;
    [SerializeField] AudioClip audioOpenMenu;
    [SerializeField] AudioClip audioExitYes;
    [SerializeField] AudioClip audioExitNo;

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
        buttonStart.SetActive(false);
        panelMenu.SetActive(false);
        scriptPlayer1 = GameObject.FindObjectOfType<Player1_SelectCharacter>();
        scriptPlayer2 = GameObject.FindObjectOfType<Player2_SelectCharacter>();
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("MenuAgain")==1) { animatorCanvas.Play("FadeOutMenu - SceneLoad");PlayerPrefs.SetInt("MenuAgain", 0); }
    }

    
    void Update()
    {
        ActiveButtonStart();

        if(Input.GetKeyUp(KeyCode.Escape)) { ChangeStateMenu(); }
    }
    private void ActiveButtonStart()
    {
        if (scriptPlayer1.indexSelector == 2 && scriptPlayer2.indexSelector == 2) { buttonStart.SetActive(true); }
        else { buttonStart.SetActive(false); }
    }
    public void ChangeStateMenu()
    {
        panelMenu.SetActive(!panelMenu.activeSelf);
        PlayAudioClip("audioOpenMenu");
    }
    public void ChangeScene()
    {
        StartCoroutine(LoadScene());
    }
    public void StopGame()
    {
        Application.Quit();
    }
    
    public void PlayAudioClip(string audioClip)
    {
        if(audioClip == "audioSelection") { audioTemp = audioSelection;}
        if(audioClip == "audioStart") { audioTemp = audioStart; }
        if(audioClip == "audioOpenMenu") { audioTemp = audioOpenMenu; }
        if(audioClip == "audioExitYes") { audioTemp = audioExitYes; }
        if(audioClip == "audioExitNo") { audioTemp = audioExitNo; }


        audioSource.PlayOneShot(audioTemp);
    }

    IEnumerator LoadScene()
    {
        animatorCanvas.Play("FadeInMenu - SceneLoad");
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("FightScene");
    }

}
