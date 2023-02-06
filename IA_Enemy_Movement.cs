using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Enemy_Movement : MonoBehaviour
{


    #region Components of the Gameobject
    //private float inputX;
    private Rigidbody rigidbodyGO;
    private Animator animator;
    private bool isGrounded;
    private bool isJumping;
    private bool isWalking;
    private bool isAttacking1;
    private bool isAttacking2;
    [SerializeField] private bool isDefending;
    private float timeBetweenAttacks;
    private float timeInvulnerable;
    [SerializeField] private SpriteRenderer spriteRendenderGod;
    [SerializeField] private Color originalColor;
    [SerializeField] private Color alternativeColor;
    private bool lookingRigth;
    private float distancePlayer1;
    private AudioSource audioSource;
    #endregion

    #region Clips del Animator
    const string ARES_IDLE = "Ares - Idle";
    const string ARES_WALK = "Ares - Walk";
    const string ARES_JUMP = "Ares - Jump";
    const string ARES_BLEND_UP = "Ares - BlendUp";
    const string ARES_BLEND_DOWN = "Ares - BlendDown";
    const string ARES_ATTACK1 = "Ares - Attack1";
    const string ARES_ATTACK2 = "Ares - Attack2";
    const string ARES_ATTACK_AIR = "Ares - AttackAir";
    #endregion

    [Header("Atributos del jugador")]
    [SerializeField] private int playerLife;
    [SerializeField] private float velMovement;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] public bool invulnerable;

    [Header("Parametros sobre el daño de los ataques")]
    [SerializeField] private IA_Enemy_DetectDamage scriptDamageArea;
    private int damageAttack1 = 10;

    private GameObject player1;
    private Player1_Movements scriptPlayer1; // Ésto hay que cambiarlo cuando agreguemos el player2, ya que al eliminar el "TestEnemy" nos va a dar error

    [Header("UI")]
    [SerializeField] GameObject panelGameOver;

    [Header("Audioclips")]
    [SerializeField] AudioClip audioAttack1;
    [SerializeField] AudioClip audioTakeHit;
    [SerializeField] AudioClip audioDeath;


    void Start()
    {
        #region Start Components of the GameObject
        rigidbodyGO = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        isGrounded = true;
        isJumping = false;
        isWalking = false;
        isAttacking1 = false;
        isDefending = false;
        playerLife = 100;
        invulnerable = false;
        originalColor = spriteRendenderGod.color;
        alternativeColor = originalColor;
        alternativeColor.a = 0.9f;
        lookingRigth = false;

        velMovement = 2f;
        fuerzaSalto = 1f;

        audioSource = GetComponent<AudioSource>(); 
        #endregion

        player1 = GameObject.FindGameObjectWithTag("Player1");
        scriptPlayer1 = player1.GetComponent<Player1_Movements>(); // Ésto hay que cambiarlo cuando agreguemos el player2, ya que al eliminar el "TestEnemy" nos va a dar error
        
    }

    private void Update()
    {
        //Distancia al jugador
        distancePlayer1 = Mathf.Abs(this.transform.position.x - player1.transform.position.x);

        Attack();

        TestInvulnerable();


        #region Set to Animation Clips
        animator.SetBool("isAttacking", isAttacking1);
        if (!isWalking && isGrounded && !isAttacking1 && !isAttacking2 && !isJumping && !isDefending) { ChangeClipAnimator(ARES_IDLE); }
        if (isWalking && isGrounded && !isAttacking1 && !isAttacking2 && !isJumping && !isDefending) { ChangeClipAnimator(ARES_WALK); }
        if (isJumping && isAttacking1) { ChangeClipAnimator(ARES_ATTACK_AIR); }
        if (isJumping && !isAttacking1) { ChangeClipAnimator(ARES_JUMP); }
        if (isGrounded && isAttacking1 && !isDefending) { ChangeClipAnimator(ARES_ATTACK1); }
        if (isGrounded && isAttacking2 && !isDefending) { ChangeClipAnimator(ARES_ATTACK2); }
        if (isGrounded && isDefending && !isAttacking1) { ChangeClipAnimator(ARES_BLEND_UP); }
        #endregion
    }
    void FixedUpdate()
    {
        Move();
        //Jump();
        TestGround();
    }

    void Move()
    {
        if (!isAttacking1 && !isDefending && !isAttacking2)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player1.transform.position.x, 0), 1f * Time.deltaTime);
            isWalking = true;
        }
        else if (isAttacking1 && !isGrounded)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player1.transform.position.x, 0), 1f * Time.deltaTime);
            isWalking = true;
        }
        else if (isAttacking2 && !isGrounded)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player1.transform.position.x, 0), 1f * Time.deltaTime);
            isWalking = true;
        }
        else if (isDefending && !isGrounded)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player1.transform.position.x, 0), 1f * Time.deltaTime);
            isWalking = true;
        }

        if (player1.transform.position.x > this.transform.position.x) { lookingRigth = false; LookEnemy(); }
        if (player1.transform.position.x < this.transform.position.x) { lookingRigth = true; LookEnemy(); }

        
    }
    
    void Attack()
    {
        //Attack 1
        if (distancePlayer1 <= 2.2f && timeBetweenAttacks <= 0 && !isAttacking1 && !isAttacking2 && isGrounded)
        {
            isAttacking1 = true;
            timeBetweenAttacks = 1;
            audioSource.PlayOneShot(audioAttack1);
            if (scriptPlayer1.invulnerable == false) { DealDamage(damageAttack1); }
        }

        if (timeBetweenAttacks > 0)
        {
            timeBetweenAttacks -= Time.deltaTime;
        }

        if (timeBetweenAttacks <= 0)
        {
            isAttacking1 = false;
            isAttacking2 = false;
        }
    }

   
    void TestGround()
    {
        isGrounded = Physics.SphereCast(transform.position + Vector3.up, 0.13f, Vector3.down, out RaycastHit raycastHit, 1f);
    }
    void ChangeClipAnimator(string newClip)
    {
        animator.Play(newClip);
    }
    public void TakeDamage(int damage1)
    {
        if (!invulnerable)
        { 
            playerLife -= damage1;
            if (playerLife >= 10) { audioSource.PlayOneShot(audioTakeHit); }
            if (playerLife < 10) { audioSource.PlayOneShot(audioDeath); }
            timeBetweenAttacks = 1f; // Ésto lo hacemos para que la IA no ataque cuando nosotros lo atacamos.
        }
        invulnerable = true;
        timeInvulnerable = 0.5f;
        StartCoroutine("FeedBackDamage");

        if (playerLife <= 0) { Death(); }
    }
    private void DealDamage(int damage2)
    {
        if (scriptDamageArea.enemyHit)
        {
            scriptPlayer1.TakeDamage(damage2);
        }
    }
    private void TestInvulnerable()
    {
        if (timeInvulnerable > 0)
        {
            timeInvulnerable -= Time.deltaTime;
        }

        if (timeInvulnerable <= 0)
        {
            if (!isDefending) { invulnerable = false; }
            StopCoroutine("FeedBackDamage");
            spriteRendenderGod.color = originalColor;
        }
    }
    private void Death()
    {
        this.enabled = false;
        scriptPlayer1.enabled = false;
        panelGameOver.SetActive(true);
    }
    private void LookEnemy()
    {
        if (lookingRigth)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            
        }
        if (!lookingRigth)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            
        }
    }
    public int GetLife()
    {
        return playerLife;
    }
    IEnumerator FeedBackDamage()
    {
        while (true)
        {
            spriteRendenderGod.color = alternativeColor;
            yield return new WaitForSeconds(0.05f);
            spriteRendenderGod.color = originalColor;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
