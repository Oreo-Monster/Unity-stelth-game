using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform pathHolder;
    public float speed;
    public float waitTime;
    Vector3[] waypoints;
    bool waiting;

    int currentIDX = 0;
    int pointsVisited = 0;
    float stopTime;

    void start(){
        waypoints = new  Vector3[pathHolder.childCount];
        for(int i = 0; i < waypoints.Length; i++){
            waypoints[i] = pathHolder.GetChild(i).position;
        }
        waiting = false;
        stopTime = 0;
        print(waypoints.ToString());
        currentIDX = 0;
    }

   void Update() {
        Vector3 toNextPoint = waypoints[currentIDX] - transform.position;
        if(waiting){
            waiting = Time.time < stopTime + waitTime; 
        }else if(toNextPoint.magnitude < 0.2){
            stopTime = Time.time;
            pointsVisited++;
            currentIDX = pointsVisited % waypoints.Length;
        }else{
            Vector3 direction = toNextPoint.normalized;
            Vector3 velocity = direction * speed;
            transform.Translate(velocity * Time.deltaTime);
        }

    }

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

    // IEnumerator followPath(){
        
    // }    
}