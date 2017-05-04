using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class AIControl : MonoBehaviour {

    public Transform[] waypoints;
    public bool crouch = false;
    public bool jump = false;
    public float stoppingDistance = 5f;

    private Player character;
    private int currentPoint = 0;
    private float distance = 0;

	// Use this for initialization
	void Start () {
        character = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        moveToWaypoint();
	}

    void moveToWaypoint ()
    {
        // Get our movement direction
        float move = Mathf.Clamp(GetWaypointPos().x, -1, 1);
        character.Move(move, crouch, jump);
        jump = false;
     }

    Vector3 GetWaypointPos()
    {
        // Get distance from position to waypoint
        Vector3 waypointPos = waypoints[currentPoint].position;
        distance = (transform.position - waypointPos).x;
        // Check if I'm close to Stoppingdistance
        if(distance <= stoppingDistance)
        {
            // Go to next waypoint
            currentPoint++; //Go to next Waypoint
        }
        // Check if currentPoint is outside waypoints length
        if(currentPoint >= waypoints.Length)
        {
            // Reset currentPoint
            currentPoint = 0;
        }
        // Return the waypoint
        return waypoints[currentPoint].position;
    }
}
