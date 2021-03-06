﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To move the player lol
/// If we get some lags --> do an update function and a fixed update with only rb.movePosition in it.
/// </summary>

public class PlayerController : MonoBehaviour
{
    /***public float speed = 6f;             // The speed that the player will move at.
    float camRayLength = 100f;             // The length of the ray from the camera into the scene.
    Vector3 movement;                      // The vector to store the direction of the player's movement.
    ***/

    private Animator anim;                      // Reference to the animator component.
    Rigidbody2D playerRigidbody;          // Reference to the player's rigidbody.
    //int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.

    public float speed;
    private Vector2 movement;
    

    void Awake()
    {
        // Create a layer mask for the floor layer.
        //floorMask = LayerMask.GetMask("Floor");
        //anim = GetComponent<Animator>();
        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move the player around the scene.
        Move(h, v);

        // Turn the player to face the mouse cursor.
        //Turning();

        // Animate the player.
        // Animating(h, v);
        anim.SetFloat("SpeedX", h);
        anim.SetFloat("SpeedY", v);

        //if (h > 0f || v > 0f || h < 0f || v < 0f) //A la base c'était : (h != 0 || v != 0)
        if (h !=0 || v!=0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);    
        }
    }

    void Move(float h, float v)
    {
       // Set the movement vector based on the axis input.
       // movement.Set(h, v, 0f);
        movement.Set(h, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(playerRigidbody.position + movement);
    }

    /*
    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }
*/
    /*
    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }*/
}