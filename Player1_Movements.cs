using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Movements : MonoBehaviour
{
    #region Components of the Gameobject
    private float inputX;
    private Rigidbody rigidbodyGO;
    private Animator animator;
    private bool isGrounded;
    private bool isJumping;
    private bool isAttacking1;
    private bool isAttacking2;
    [SerializeField]private bool isDefending;
    private float timeBetweenAttacks;
    private float timeInvulnerable;
    [SerializeField] private SpriteRenderer spriteRendenderGod;
    [SerializeField] private Color originalColor;
    [SerializeField] private Color alternativeColor;
    private bool lookingRigth;
    [SerializeField] private LayerMask _layerMask;
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
    [SerializeField] private float velMovement = 2f;
    [SerializeField] private float fuerzaSalto = 1f;
    [SerializeField] public bool invulnerable;

    [Header ("Parametros sobre el daño de los ataques")]
    [SerializeField] private Player1_DetectDamage scriptDamageArea;
    private int damageAttack1 = 10;

    private GameObject player2;
    private IA_Enemy_Movement scriptPlayer2; // Ésto hay que cambiarlo cuando agreguemos el player2, ya que al eliminar el "TestEnemy" nos va a dar error

    [Header("UI")]
    [SerializeField] GameObject panelGameOver;

    [Header("Audioclips")]
    [SerializeField] AudioClip audioAttack1;
    [SerializeField] AudioClip audioAttack2;
    [SerializeField] AudioClip audioShieldDefending;
    [SerializeField] AudioClip audioTakeHit;
    [SerializeField] AudioClip audioDeath;
    
    void Start()
    {
        #region Start Components of the GameObject
        rigidbodyGO = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        isGrounded = true;
        isJumping = false;
        isAttacking1 = false;
        isDefending = false;
        playerLife = 100;
        invulnerable= false;
        originalColor = spriteRendenderGod.color;
        alternativeColor = originalColor;
        alternativeColor.a = 0.9f;
        lookingRigth = true;
        audioSource = GetComponent<AudioSource>();
        #endregion

        player2 = GameObject.FindGameObjectWithTag("Player2");
        scriptPlayer2 = player2.GetComponent<IA_Enemy_Movement>(); // Ésto hay que cambiarlo cuando agreguemos el player2, ya que al eliminar el "TestEnemy" nos va a dar error
    }

    private void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        if (!isAttacking1) { isDefending = Input.GetButton("Fire2"); }
        if (isDefending) { invulnerable= true; }

        Attack();

        TestInvulnerable();
        
        if(!isGrounded) { velMovement = 3f; }
        if(isGrounded) { velMovement = 2f; }


        #region Set to Animation Clips
        animator.SetBool("isAttacking", isAttacking1);
        if(inputX == 0 && isGrounded && !isAttacking1 && !isAttacking2 && !isJumping && !isDefending) { ChangeClipAnimator(ARES_IDLE); }
        if(inputX != 0 && isGrounded && !isAttacking1 && !isAttacking2 && !isJumping && !isDefending) { ChangeClipAnimator(ARES_WALK); }
        if(isJumping && isAttacking1) { ChangeClipAnimator(ARES_ATTACK_AIR); }
        if(isJumping && !isAttacking1) { ChangeClipAnimator(ARES_JUMP); }
        if(isGrounded && isAttacking1 && !isDefending) { ChangeClipAnimator(ARES_ATTACK1); }
        if(isGrounded && isAttacking2 && !isDefending) { ChangeClipAnimator(ARES_ATTACK2); }
        if(isGrounded && isDefending && !isAttacking1) { ChangeClipAnimator(ARES_BLEND_UP); }
        #endregion
    }
    void FixedUpdate()
    {
        Move();
        Jump();
        TestGround();
    }

    void Move()
    {
        if (!isAttacking1 && !isDefending && !isAttacking2)
        {
            rigidbodyGO.MovePosition(rigidbodyGO.position + new Vector3(inputX, 0, 0) * velMovement * Time.fixedDeltaTime);
        } 
        else if (isAttacking1 && !isGrounded)
        {
            rigidbodyGO.MovePosition(rigidbodyGO.position + new Vector3(inputX, 0, 0) * velMovement * Time.fixedDeltaTime);
        }
        else if (isAttacking2 && !isGrounded)
        {
            rigidbodyGO.MovePosition(rigidbodyGO.position + new Vector3(inputX, 0, 0) * velMovement * Time.fixedDeltaTime);
        }
        else if (isDefending && !isGrounded)
        {
            rigidbodyGO.MovePosition(rigidbodyGO.position + new Vector3(inputX, 0, 0) * velMovement * Time.fixedDeltaTime);
        }

        if (player2.transform.position.x > this.transform.position.x) { lookingRigth = true; LookEnemy(); }
        if (player2.transform.position.x < this.transform.position.x) { lookingRigth = false; LookEnemy(); }
    }
    void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded && timeBetweenAttacks <= 0){
            isJumping = true;
            rigidbodyGO.AddForce(new Vector3(0, fuerzaSalto, 0), ForceMode.Impulse);
        } else
        {
            isJumping = false;
        }
    }
    void Attack()
    {
        //Attack 1
        if (Input.GetButtonDown("Fire1") && timeBetweenAttacks <= 0 && !isAttacking1 && !isAttacking2 && isGrounded)
        {
            isAttacking1 = true;
            timeBetweenAttacks = 1;
            audioSource.PlayOneShot(audioAttack1);
            if (scriptPlayer2.invulnerable == false) { DealDamage(damageAttack1); }
        }

        //Attack 2
        if (Input.GetButtonDown("Fire3") && timeBetweenAttacks <= 0 && !isAttacking1 && !isAttacking2 && isGrounded)
        {
            isAttacking2 = true;
            timeBetweenAttacks = 1.3f;
            audioSource.PlayOneShot(audioAttack2);
        }

        //Attack in Air
        if (!isGrounded)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                isAttacking1 = true;
                timeBetweenAttacks = 1;
            }
            if (isAttacking1)
            {
                if (scriptPlayer2.invulnerable == false)
                {
                    DealDamage(damageAttack1);
                }
                else
                {
                    isAttacking1 = false;
                }
            }
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
        isGrounded = Physics.SphereCast(transform.position + Vector3.up, 0.13f, Vector3.down, out RaycastHit raycastHit, 1f, _layerMask); //El ultimo 3 corresponde al Index de la Layer detectada como "Ground"
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
            scriptPlayer2.TakeDamage(damage2);
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
        this.enabled= false;
        scriptPlayer2.enabled= false;
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
