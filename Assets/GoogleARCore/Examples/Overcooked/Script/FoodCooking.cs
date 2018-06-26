using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is attached on the unprocessed food (raw food to be cooked)

public class FoodCooking : MonoBehaviour {

	bool touchPan = false;

	bool done = false;

	float time = 5f;

	public delegate void FinishProcess(GameObject unprocessedFood);
	public static event FinishProcess FinishProcessEvent;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		if (!done) {

			//check if the steak is touching the pan, if it is then start timer
			if (touchPan) {
			
				time = time - Time.deltaTime;

				//if time is up, then enable the cooked steak and disable the uncooked steak (done cooking)
				if (time < 0) {

					if (FinishProcessEvent != null) {
						FinishProcessEvent (gameObject);
					}

					done = true;
				}

			} else {

				//if not touching pan, then reset timer
				time = time;
			}
		}

	}

	//check for collision between uncooked steak and pan
	void OnCollisionEnter(Collision col){

		//Debug.Log ("colliding with: " + col.transform.name);

		if (col.gameObject.tag == "pan") {

			touchPan = true;
			//Debug.Log ("touchPan: " + touchPan);


		} else {
			
			touchPan = false;
			//Debug.Log ("touchPan: " + touchPan);

		}

	}	
}
