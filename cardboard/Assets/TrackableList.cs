using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;

public class TrackableList : MonoBehaviour {

	string myName = "stones";
	//string myName = "chips";

	void Start () {
		Debug.Log ("This is working!");
		WWW www = new WWW ("http://localhost:5000/");
		StartCoroutine(WaitForRequest(www));
		WWW ww2 = new WWW ("http://localhost:5000/test");
		StartCoroutine (WaitForRequest (ww2));
	}
		
	IEnumerator WaitForRequest(WWW www) {
		yield return www;

		// check for errors
		if (www.error == null) {
			Debug.Log("WWW Ok!: " + www.data);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log ("Starting something hello world!!");
		// Get the Vuforia StateManager
		StateManager sm = TrackerManager.Instance.GetStateManager ();

		// Query the StateManager to retrieve the list of
		// currently 'active' trackables 
		//(i.e. the ones currently being tracked by Vuforia)
		IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours ();

		// Iterate through the list of active trackables
		//Debug.Log ("List of trackables currently active (tracked): ");
		foreach (TrackableBehaviour tb in activeTrackables) {
			if (!string.Equals (myName, tb.TrackableName)) {
				if (Input.GetButtonDown ("Fire1")) {
					Debug.Log ("firing at " + tb.TrackableName);
				}
				Debug.Log ("Trackable!!!!!!!!: " + tb.TrackableName);
			}
		}
	}
}
