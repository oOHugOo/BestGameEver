using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasing : MonoBehaviour
{
    Animator anim;
    GameObject player;
    public bool chasing = false;
    Vector2 chasingDir;

    EnemyPatrolling enemyPatrolling;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        enemyPatrolling = GetComponent<EnemyPatrolling>();
        //speed = enemyPatrolling.speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the entering collider is the player...
        if ((other.gameObject == player) & (other.GetType() == typeof(CircleCollider2D)))
        {
            chasing = true;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // If the entering collider is the player...
        if ((other.gameObject == player) & (other.GetType() ==typeof(CircleCollider2D)))
        {
            chasing = false;
            anim.speed = 1.0f;

        }
    }

    void isChasing ()
    {

        enemyPatrolling.speed = 8f;
        anim.enabled = true;
        anim.speed =  2.5f * 1.0f;
        chasingDir = player.transform.position - transform.position;
        transform.Translate(chasingDir.normalized * Time.deltaTime * enemyPatrolling.speed);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (chasing)
        {
            isChasing();

        }
    }
}
