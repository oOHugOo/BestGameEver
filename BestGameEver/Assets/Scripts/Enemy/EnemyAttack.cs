﻿using UnityEngine;
using System.Collections;
using System;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 1f;     // The time in seconds between each attack.
    public int attackDamage = 1;               // The amount of health taken away per attack.


    //Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    CharacterHealth characterHealth;                  // Reference to the player's health.
    //EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.


    void Start() //avant c'était Awake
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        //playerHealth = player.currentHealth;
        characterHealth = player.GetComponent<CharacterHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        //anim = GetComponent<Animator>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Ca collide");
        Console.WriteLine("its colliding.");
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is in range.
            playerInRange = true;
            Debug.Log("Ca touche");
            Console.WriteLine("The variable is set to true.");
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
            Debug.Log("Ca sort");
            Console.WriteLine("The variable is set to false.");
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
        }
    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (characterHealth.currentHealth > 0)
        {
            // ... damage the player.
            characterHealth.TakeDamage(attackDamage);
        }
    }
}
