using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name == "Game" && Input.GetKey("escape")) { 
            change_Scene("MainMenu");
        }
    }

    public void change_Scene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

