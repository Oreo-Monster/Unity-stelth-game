﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainLight : MonoBehaviour
{
    
    public float rotationSpeed;


    void Update(){
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
