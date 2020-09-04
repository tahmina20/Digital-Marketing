using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNameUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = "Stage " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.ToString();
	}
	
}
