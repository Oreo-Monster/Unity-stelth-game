using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    public GameObject player;
    public float altitude;

    void Update(){
        transform.position = new Vector3(player.transform.position.x, altitude, player.transform.position.z);
    }
}
