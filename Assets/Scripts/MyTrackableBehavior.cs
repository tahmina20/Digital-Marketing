using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MyTrackableBehavior : MonoBehaviour, ITrackableEventHandler {

    [SerializeField]
    GameMgr GameMgr;

    void ITrackableEventHandler.OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if(newStatus == TrackableBehaviour.Status.TRACKED)
        {
            GameMgr.Activate();
        }
        else if(newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            GameMgr.Desactivate();
        }
    }

    // Use this for initialization
    void Start () {
        GetComponent<TrackableBehaviour>().RegisterTrackableEventHandler(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
