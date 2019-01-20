using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{

    /// <summary>
    /// There are 2 Parts in this Scripts, one is /*** in comments ***/, the other is running.
    /// 1) The First one creates random patrol points using positions and Vector3.
    /// 2) The Second one uses patrolPoints you have to assign in the Inspector from EmptyGameObject transforms. This allows to manually set the points
    /// and thus check for specific scenario that may be problematic. Do not forget to comment the first one when using the second one.
    /// </summary


    /// /***  
    
        //1) First Part - The patrolPoints are set randomly in a specific Range or Area of patrolling in the Start() function.

    Vector3[] patrolPoints = new Vector3[9];
    Vector3 currentPatrolPoint;

    public int patrollingRangeX = 30;
    public int patrollingRangeY = 30;
    public float speed =4f;
    int currentPatrolIndex;
    Vector2 patrolPointDir;

    int pauseEveryXPoint = 2;   // private?
    float pauseTimer = 0f;      // private?
    private Animator anim;
    EnemyChasing enemyChasing;
    bool chasing;
    EnemyAttack enemyAttack;


    // Start is called before the first frame update
    void Start()
    {
        // Create patrolling points around This, randomly in the This range
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float xPos = Random.Range(transform.position.x - patrollingRangeX, transform.position.x + patrollingRangeX);
            float yPos = Random.Range(transform.position.y - patrollingRangeY, transform.position.y + patrollingRangeY);
            patrolPoints[i] = new Vector3(xPos, yPos,0f);
            //Debug.Log(i);

        }

        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        anim = GetComponent<Animator>();
        enemyChasing = GetComponent<EnemyChasing>();
        chasing = enemyChasing.chasing;
        enemyAttack = GetComponent<EnemyAttack>();
        anim.SetBool("Attacking", false); 


    }


    void isPatrolling()
    {
        speed = 4f;
        anim.enabled = true;
        patrolPointDir = currentPatrolPoint - transform.position;
        transform.Translate(patrolPointDir.normalized * Time.deltaTime * speed);

        //check to see if we have reached the patrol point
        if (Vector2.Distance(transform.position, currentPatrolPoint) < .1f)
        {

            // the pause is one less step ahead.
            pauseEveryXPoint--;
            //Fait une pause dans sa ronde tous les 2 points.
            if (pauseEveryXPoint == 0)
            {
                pauseEveryXPoint = 2;
                pauseTimer = 3f;
                // set le direction du mouvement à 0 ( par reussis a faire "patrolPointDir = (0,0); )
                //On aurait pu set Speed = 0 aussi mais bon comme le patrolPointDir se fait de toutes façon update, autant use lui.
                patrolPointDir -= patrolPointDir;
                anim.enabled = false;

            }

            //We have reached the patrol point - get the next one
            //check to see if we have anymore patrol points - if not go back to the beginning
            if (currentPatrolIndex + 1 < patrolPoints.Length)
            {
                currentPatrolIndex++;

            }
            else
            {
                currentPatrolIndex = 0;
            }
            currentPatrolPoint = patrolPoints[currentPatrolIndex];

        }
    }




    // Update is called once per frame
    void Update() {
        // updtae boolean chasing from EnemeyChasing script (or class)
        chasing = enemyChasing.chasing;
        
        // if not taking a break during the patrol, nor chasing, nor in range for attack
        if (pauseTimer <= 0f & chasing == false & enemyAttack.playerInRange == false) {
                isPatrolling();
        }
        else
        {
            pauseTimer -= Time.deltaTime;
        }
    }

    /// ***/

   
    
    /***
     
     //2) Second Part - use for specific scenario and/or check - The patrolPoints have to be set manually in the inspector.


       public Transform[] patrolPoints;
       Transform currentPatrolPoint;

        //public int patrollingRangeX = 30;
        //public int patrollingRangeY = 30;
        public float speed;
        int currentPatrolIndex;
        Vector2 patrolPointDir;

        int pauseEveryXPoint = 2;   // private?
        float pauseTimer = 0f;      // private?
        private Animator anim;


        // Start is called before the first frame update
        void Start()
        {
           
            currentPatrolIndex = 0;
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
            anim = GetComponent<Animator>();

        }

        // Update is called once per frame
        void Update()
        {

            if (pauseTimer <= 0)
            {

                anim.enabled = true;
                patrolPointDir = currentPatrolPoint.position - transform.position;
                transform.Translate(patrolPointDir.normalized * Time.deltaTime * speed);
             
                //check to see if we have reached the patrol point
                if (Vector2.Distance(transform.position, currentPatrolPoint.position) < .1f)
                {
                    // the pause is one less step ahead.
                    pauseEveryXPoint--;
                    //Fait une pause dans sa ronde tous les deux points.
                    if (pauseEveryXPoint == 0)
                    {
                        pauseEveryXPoint = 2;
                        pauseTimer = 3f;
                        // set le direction du mouvement à 0 ( par reussis a faire "patrolPointDir = (0,0); )
                        //On aurait pu set Speed = 0 aussi mais bon comme le patrolPointDir se fait de toutes façon update, autant use lui.
                        patrolPointDir -= patrolPointDir;
                        anim.enabled = false;

                    }


                    //We have reached the patrol point - get th next one
                    //check to see if we have anymore patrol points - if not go back to the beginning
                    if (currentPatrolIndex + 1 < patrolPoints.Length)
                    {
                        currentPatrolIndex++;

                    }
                    else
                    {
                        currentPatrolIndex = 0;
                    }
                    currentPatrolPoint = patrolPoints[currentPatrolIndex];
                }
            }
            else
            {
                pauseTimer -= Time.deltaTime;
            }
        }

         ***/
}
