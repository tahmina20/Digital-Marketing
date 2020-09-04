using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public void Awake()
    {
        if (!PlayerPrefs.HasKey("progress"))
            PlayerPrefs.SetInt("progress", 0);
    }

    public void Play () {
            SceneManager.LoadScene(PlayerPrefs.GetInt("progress") + 1);
	}
	
	public void Quit () {
        Application.Quit();
	}
}
