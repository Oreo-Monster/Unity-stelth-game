using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider other) {
        player.SetActive(false);
        print("YOU WON");
    }
}
