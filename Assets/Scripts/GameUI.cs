using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour{
    public GameObject gameOver;
    public Transform guards;

    bool pause = false;
    void Start(){
        for(int i = 0; i < guards.childCount; i++){
            guards.GetChild(i).gameObject.GetComponent<Guard>().playerSpotted += OnGameOver;
        }
    }

    // Update is called once per frame
    void Update(){
        if(pause){
            if(Input.GetKeyDown(KeyCode.Space)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void OnGameOver(){
        gameOver.SetActive(true);
        pause = true;
    }
}
