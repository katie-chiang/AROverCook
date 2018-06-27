using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class iceCreamScript : MonoBehaviour {

    //check what item the user is touching and instantiate bezier if the user is touching the cone
    bool firstTime = true;
    public GameObject curvePrefab;

	// Use this for initialization
	void Start () {
		
	}


	
	// Update is called once per frame
	void Update () {

        if(!firstTime){
            return;
        }

        if(Input.touchCount < 1){
            return;
        }

        Touch touch = Input.GetTouch(0);
        Ray raycast = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit raycastHit;

        if (Physics.Raycast(raycast, out raycastHit)){
            if(raycastHit.transform.tag == "icecream"){

                GameObject curveObject = Instantiate(curvePrefab, raycastHit.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                curveObject.transform.parent = raycastHit.transform;
                firstTime = false;
            }
        }
		
	}
}
