using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //AudioListener.volume = 0.5f;

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

    //public void SetMasterVolume(float value)
    //{
    //    AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    //}

    public void VolumeControl(float volumeControl)
    {
        // audio.volume = volumeControl
        ////             var nearsestEnemy = Component.GetComponent<AudioSource>().audio = volumeControl;
        //AudioListener.volume = 0.1F;

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
