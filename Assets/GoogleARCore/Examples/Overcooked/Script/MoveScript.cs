
namespace GoogleARCore.Examples.HelloAR{

using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using GoogleARCore.Examples.CloudAnchor;


#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = InstantPreviewInput;
#endif

public class MoveScript : MonoBehaviour {

	bool carrying = false;
	public float distance = 0.6f; 
	public float smooth = 10;

	Transform toCarry;
	RaycastHit raycastHit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log ("touch count: " + Input.touchCount);

		//return if user hasnt touched the screen
		if (Input.touchCount < 1)
		{	
			return;
		}
			
		Touch touch = Input.GetTouch(0);

		//if user is currently carrying the object
		if (carrying) {

			//move the object with the movement of the camera
			toCarry.position = Vector3.Lerp (toCarry.position, Camera.main.transform.position + Camera.main.transform.forward * distance, Time.deltaTime * smooth);

			//drop object if user stopped touching the screen
			if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {

				if (raycastHit.rigidbody == null) {
					
					carrying = false;
					toCarry = null;
					
				} else {
				
					raycastHit.rigidbody.isKinematic = false;
					carrying = false;
					toCarry = null;

				}

			}

		} else {

			//check for ray collision if the user just started touching the screen
			if (touch.phase == TouchPhase.Began) {

				Ray raycast = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);

				//if something is hit
				if (Physics.Raycast (raycast, out raycastHit)) {

					Debug.Log ("raycast hit: " + raycastHit.transform.name);
				
					//move the object if it's this game object
					if (raycastHit.transform.gameObject == gameObject) {

						//distance = raycastHit.distance;
						carrying = true;
						toCarry = raycastHit.transform;
						raycastHit.rigidbody.isKinematic = true;

					} else {

						//if hit something that is not this game object then can't move it so return
						carrying = false;
						return;

					}
				}

			}
		}
	}


}
		

}