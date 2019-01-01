using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float xMargin = 1f; // Distance in the x axis the player can move before the camera follows.
    public float yMargin = 1f; // Distance in the y axis the player can move before the camera follows.
    public float xSmooth = 8f; // How smoothly the camera catches up with it's target movement in the x axis.
    public float ySmooth = 8f; // How smoothly the camera catches up with it's target movement in the y axis.
    public Vector2 maxXandY;   // The maximum x and y coordinate the camera can have.
    public Vector2 minXandY;   // The minimum x and y coordinate the camera can have.

    private Transform player;  //Reference to the player's transform.

    private void Awake()
    {
        // Setting the reference.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    bool CheckXMargin()
    {
        // Return true if the distance between the camera and the player in the x axis is greater than x margin.
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }


    bool CheckYMargin()
    {
        // Return true if the distance between the camera and the player in the y axis is greater than y margin.
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
    }

   
	void FixedUpdate ()
    {
        TrackPLayer();
	}


    void TrackPLayer()
    {
        // By default the target x and y coordinates of the camera are it's current x and y coordinates.
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        // If the player has moved beyond the x margin...
        if (CheckXMargin())
            // ... the target x coordinate should be a Lerp (linear interpolation) between the camera's current x position and the player's current x position. 
            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
        
        // If the player has moved beyond the x margin...
        if (CheckYMargin())
            // ... the target y coordinate should be a Lerp (linear interpolation) between the camera's current y position and the player's current y position. 
            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);

        // The target x and y coordinate should not be larger than the mayimum or smaller than the minimum.
        targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);
        targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);

        // Set the camera's position to the target position with the same z component.
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}



