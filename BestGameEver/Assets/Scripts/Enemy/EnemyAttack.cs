using UnityEngine;
using System.Collections;
using System;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 2f;       // The time in seconds between each attack.
    public int attackDamage = 1;                // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    CharacterHealth characterHealth;            // Reference to the player's health.
    //EnemyHealth enemyHealth;                  // Reference to this enemy's health.
    public bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.

    EnemyPatrolling enemyPatrolling;            // We will need the speed of the mob = "Enemy"
    EnemyChasing enemyChasing;
    Vector2 attackDir;


    void Start() //avant c'était Awake
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(player);
        //playerHealth = player.currentHealth;
        characterHealth = player.GetComponent<CharacterHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();

        enemyPatrolling = GetComponent<EnemyPatrolling>();
        enemyChasing = GetComponent<EnemyChasing>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Ca collide");
        
        // If the entering collider is the player...
        if ((other.gameObject == player) & (other.GetType() == typeof(CapsuleCollider2D)))
        {
            // ... the player is in range.
            playerInRange = true;
            //Debug.Log("Ca touche");
            
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        // If the exiting collider is the player...
        if ((other.gameObject == player) & (other.GetType() == typeof(CapsuleCollider2D)))
        {
            // ... the player is no longer in range for attacking.
            playerInRange = false;
            //Debug.Log("Ca sort");

            //But it should still be in range for chasing
            enemyChasing.chasing = true;
            anim.SetBool("Attacking",false);
            
        }
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange) //&& enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack();
            

        }

        // If the player has zero or less health...
        if (characterHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            //anim.SetTrigger("PlayerDead");
            anim.SetBool("Attacking", false);
        }
    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;
        // the enemy stop to run in order to attack...
        enemyPatrolling.speed = 0f;
        // ... he is therefore not chasing anymore
        enemyChasing.chasing = false;

        // If the player has health to lose...
        if (characterHealth.currentHealth > 0)
        {
            // ... damage the player.
            characterHealth.TakeDamage(attackDamage);

            // Set the animation Attacking
            attackDir = (player.transform.position - transform.position).normalized;
            anim.SetFloat("playerDirectionX", attackDir.x);
            anim.SetFloat("playerDirectionY", attackDir.y);
            anim.SetBool("Attacking", true);
        }

       // anim.SetBool("Attacking", false);  
    }
}
