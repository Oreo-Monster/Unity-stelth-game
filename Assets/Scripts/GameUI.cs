using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour{
    public GameObject gameOver;
    public GameObject winScreen;
    public Transform guards;
    public GameObject player;
    public GameObject finish;
    public Text time;

    bool pause = false;
    void Start(){
        for(int i = 0; i < guards.childCount; i++){
            guards.GetChild(i).gameObject.GetComponent<Guard>().playerSpotted += OnGameOver;
        }
        finish.GetComponent<Finish>().win += OnWin;
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
        player.GetComponent<Player>().moving = false;
    }

    void OnWin(){
        winScreen.SetActive(true);
        pause = true;
        time.text = Mathf.RoundToInt(Time.timeSinceLevelLoad).ToString();
    }
}

