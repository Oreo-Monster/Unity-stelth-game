/*
Emerson Wright
August 2020
Baised off Intro to game dev seires by Sebastion Lague
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

    public GameObject player;
    public float speed;
    public float rotationSpeed;
    //How long the guard will wait after reaching a waypoint
    public float waitTime;
    //used to store the location of all the way points in order
    Vector3[] waypoints;

    public Light spotLight;
    public float veiwDistance;
    float veiwAngle;
    void Start(){
        //Filling the waypoints array with the position of the each waypoint
        waypoints = new Vector3[pathHolder.childCount];
        for(int i = 0; i < waypoints.Length; i++){
            waypoints[i] = pathHolder.GetChild(i).position;
            //The y value is not needed, so it is set to the guard height
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        StartCoroutine(followPath(waypoints));

        veiwAngle = spotLight.spotAngle;
    }

    private void Update() {
        if (playerInSight(player)){
            spotLight.color = Color.red;
        }else{
            spotLight.color = Color.yellow;
        }
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
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * veiwDistance);
    }

    //Used to have the guard follow along the path, stop at each waypoint for a set amount of time, then turn the next waypoint and move there

    IEnumerator followPath(Vector3[] waypoints){
        //The guard will start at the first waypoint in his path
        transform.position = waypoints[0];
        //They will be going to the next waypoint
        int currentIDX = 1;
        Vector3 targetWaypoint = waypoints[currentIDX];
        //Facing first target
        transform.LookAt(targetWaypoint);
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
                //Faceing next target
                yield return StartCoroutine(turnToTarget(targetWaypoint));
            }
            yield return null;
        }
    }

    //Method used to turn the guard to face a target
    IEnumerator turnToTarget(Vector3 lookTarget){
        //Getting the direction
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        //Finding the angle
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;
        //While the are farther than 0.01 degrees apart
        while(Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.01f){
            //Finding how much to move the guard this frame
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            //Rotating the guard the required amount
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }

    }

    bool playerInSight(GameObject player){

        Vector3 toPlayer = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
        if(toPlayer.magnitude < veiwDistance){
            if(Vector3.Angle(toPlayer, transform.forward) < veiwAngle/2){
               Ray ray = new Ray(transform.position, toPlayer);
               RaycastHit hitInfo;
               if(Physics.Raycast(ray, out hitInfo)){
                   Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
                   print("hit");
                    return hitInfo.collider.gameObject.tag == "Player";
               } 
            }
        }
        return false;
    }

}