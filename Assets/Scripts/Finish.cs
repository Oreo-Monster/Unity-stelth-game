using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Finish : MonoBehaviour
{
    public GameObject player;
    public event Action win;

    private void OnTriggerEnter(Collider other) {
        win();
        player.GetComponent<Player>().moving = false;
    }
}
