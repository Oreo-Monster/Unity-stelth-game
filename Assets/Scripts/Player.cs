/*
Emerson Wright
August 2020
Baised off Intro to game dev seires by Sebastion Lague
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Script for player control
*/

public class Player : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    //Used to smooth out movements
    public float smoothMoveTime = 0.1f;
    
    float angle;
    float smoothInputMagnitude;
    float smoothMoveVelocity;

    Vector3 velocity;
    Rigidbody rb;
    public bool moving = true;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }
    void Update(){
        //Getting user inputs
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        float inputMagnitude = inputDirection.magnitude;
        //Smoothing out speed changes
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);
        //Finding rotation angle needed
        float targetAngle = 90 - Mathf.Atan2(inputDirection.z, inputDirection.x) * Mathf.Rad2Deg;
        //Smoothing out angle
        angle = Mathf.LerpAngle(angle, targetAngle,Time.deltaTime * rotationSpeed * inputMagnitude);
        //calculating finial velocity (the player always moves in the forward direction, and the model rotates to turn)
        velocity = (transform.forward.normalized) * speed * smoothInputMagnitude;
    }

    //Updating the rigid body model
    void FixedUpdate() {
        if(moving){
            rb.MoveRotation(Quaternion.Euler(Vector3.up * angle));
            rb.MovePosition(rb.position + velocity * Time.deltaTime);
        }
        
    }


}
