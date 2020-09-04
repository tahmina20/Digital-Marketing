using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour {

    [SerializeField]
    GameObject LostScreen, WinScreen, Player;

    [SerializeField]
    private ParkingSlots parkingSlots;

    public void Activate()
    {
        Player.SetActive(true);
    }

    public void Desactivate()
    {
        Player.SetActive(false);
    }

    public void Lose()
    {
        LostScreen.SetActive(true);
    }

    public void Win()
    {
        parkingSlots.SetVictoryTexture();
        WinScreen.SetActive(true);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
