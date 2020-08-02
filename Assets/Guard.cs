/*
Emerson Wright
August 2020
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
This script controls the guard in the world, which move around a given path of empty game objects
*/


public class Guard : MonoBehaviour{
    //The path is a group of empty game objects which the guard will walk to in the world
    public Transform pathHolder;
    public float speed;
    public float rotationSpeed;
    //How long the guard will wait after reaching a waypoint
    public float waitTime;
    //used to store the location of all the way points in order
    Vector3[] waypoints;
    void Start(){
        //Filling the waypoints array with the position of the each waypoint
        waypoints = new Vector3[pathHolder.childCount];
        for(int i = 0; i < waypoints.Length; i++){
            waypoints[i] = pathHolder.GetChild(i).position;
            //The y value is not needed, so it is set to the guard height
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        StartCoroutine(followPath(waypoints));
    }

    //This method is used to draw Gizmos to visulize the path in the game editor
    void OnDrawGizmos() {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach(Transform waypoint in pathHolder){
            Gizmos.DrawSphere(waypoint.position, 0.3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
    }

    //Used to have the guard follow along the path, stop at each waypoint for a set amount of time, then turn the next waypoint and move there

    IEnumerator followPath(Vector3[] waypoints){
        //The guard will start at the first waypoint in his path
        transform.position = waypoints[0];
        //They will be going to the next waypoint
        int currentIDX = 1;
        Vector3 targetWaypoint = waypoints[currentIDX];
        Vector3 direction = targetWaypoint - transform.position;
        direction.Normalize();
        //Setting the guard to face the direction of there first target
        transform.forward = direction;
        //Loop that runs every frame
        while(true){
            //Move towards the target waypoint
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            //check if there
            if(transform.position == targetWaypoint){
                //target the next waypoint
                currentIDX = (currentIDX +1) % waypoints.Length;
                targetWaypoint = waypoints[currentIDX];
                //Guard will wait the wait time before moving
                yield return new WaitForSeconds(waitTime);
                //finding new direction
                direction = targetWaypoint - transform.position;
                direction.Normalize();
                bool pointingToTarget = false;
                //While roating to target
                while(!pointingToTarget){
                    //Rotating twards the target
                    transform.forward = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0);
                    //Checking if the rotation has been compleated
                    pointingToTarget = Vector3.Cross(direction, transform.forward).magnitude < 0.01f;
                    yield return null;
                }
            }
            yield return null;
        }
    }    
}