using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour {

  
    public Text lastScore;
    public Text highScore;


	// Use this for initialization
	void Start () {

     
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        lastScore.text = "Last score : " + PlayerPrefs.GetFloat("lastScore");

        if (PlayerPrefs.HasKey("Highscore"))
        {
            if(PlayerPrefs.GetFloat("Highscore") < PlayerPrefs.GetFloat("lastScore"))
            {
                PlayerPrefs.SetFloat("Highscore", PlayerPrefs.GetFloat("lastScore"));
            } 

        } else
        {
            PlayerPrefs.SetFloat("Highscore", PlayerPrefs.GetFloat("lastScore"));
        }

        highScore.text = "1. " + PlayerPrefs.GetFloat("Highscore");
    }

    public void change_Scene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
