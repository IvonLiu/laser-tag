using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;

public class TrackableList : MonoBehaviour {

	bool alive = true;
	bool disabled = false;

	string myName = "stones";
	//string myName = "chips";

	void Start () {
		Debug.Log ("This is working!");
	}
		
	IEnumerator WaitForCheckRequest(WWW www) {
		yield return www;

		// check for errors
		if (www.error == null) {
			if (string.Equals (www.text, "true")) {	// returns true if you've been shot
				alive = false;
			} else {
				alive = true;
			}
		} else {
			Debug.Log("Check WWW Error: "+ www.error);
		}    
	}

	IEnumerator WaitForShootRequest(WWW www) {
		yield return www;

		// check for errors
		if (www.error == null) {
			Debug.Log("Shoot WWW Ok!: " + www.data);
		} else {
			Debug.Log("Shoot WWW Error: "+ www.error);
		} 
	}

	void disable () {
		disabled = true;
		Debug.Log ("Disabling");
	}

	void enable () {
		disabled = false;
		Debug.Log ("Enabling");
	}

	// Update is called once per frame
	void Update () {

		WWW wwwCheck = new WWW ("http://localhost:5000/check/" + myName);
		StartCoroutine (WaitForCheckRequest (wwwCheck));

		if (!alive) {
			if (!disabled) {
				disable ();
			}
			return;
		} else {
			if (disabled) {
				enable ();
			}
		}

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
					WWW wwwShoot = new WWW ("http://localhost:5000/shoot/"+tb.TrackableName);
					StartCoroutine(WaitForShootRequest(wwwShoot));
				}
				Debug.Log ("Trackable!!!!!!!!: " + tb.TrackableName);
			}
		}
	}
}
