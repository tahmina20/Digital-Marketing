using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingSlots : MonoBehaviour {

    [SerializeField]
    Texture greenTexture;
    [SerializeField]
    Texture whiteTexture;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetVictoryTexture()
    {
        GetComponent<Renderer>().material.mainTexture = greenTexture;
    }

    public void SetInitialTexture()
    {
        GetComponent<Renderer>().material.mainTexture = whiteTexture;
    }
}
