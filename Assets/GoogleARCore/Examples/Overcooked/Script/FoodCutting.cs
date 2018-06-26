using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCutting : MonoBehaviour {


	int numCuts = 0;
	bool done = false;
	Rigidbody uncutFoodrb = null;

	public delegate void FinishProcess(GameObject unprocessedFood);
	public static event FinishProcess FinishProcessEvent;

	// Use this for initialization
	void Start () {
		uncutFoodrb = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!done) {

			if (numCuts >= 7) {


				if (FinishProcessEvent != null) {
					FinishProcessEvent (gameObject);
				}

				done = true;
			}

		}

	}

	void OnCollisionEnter(Collision col){

		if (!done) {

			if (col.gameObject.tag == "knife") {
				numCuts++;
			} else if (col.gameObject.tag == "cuttingBoard") {
				//cheese touch the cutting board so make it static
				uncutFoodrb.constraints = RigidbodyConstraints.FreezeAll;	
			} 
		}
	
	}
}
