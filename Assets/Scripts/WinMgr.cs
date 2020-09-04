using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMgr : MonoBehaviour {

    public void OnAnimatorFinish()
    {
        PlayerPrefs.SetInt("progress", SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            PlayerPrefs.SetInt("progress", 0);
            SceneManager.LoadScene(0);
        }
            
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
