using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator anim;
    Vector3 lastPosition= Vector3.zero;
    Vector3 speed;



    // Start is called before the first frame update
    void Start()
    {

        anim = gameObject.GetComponent<Animator>();
        lastPosition = transform.position;
        
   
      

    }

    // Update is called once per frame
    void Update()
    {
        speed = transform.position - lastPosition;
        lastPosition = transform.position;
        //Debug.Log("speedX = "+speed.x + " /speedY = " +speed.y +"/ speedZ = "+speed.z );
        
   

        anim.SetFloat("SpeedX", speed.x);
        anim.SetFloat("SpeedY", speed.y);
    }
}
