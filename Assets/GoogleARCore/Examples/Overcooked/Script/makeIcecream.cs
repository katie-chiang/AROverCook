using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class makeIcecream : MonoBehaviour {

    bool firstTime = true;
    public GameObject curvePrefab;
    public float distanceToCone = 0; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (!firstTime)
        {
            return;
        }

        if (Input.touchCount < 1)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);

        //changed for debugging with mouse
        //if(!Input.GetMouseButtonDown(0)){
        //    return;
        //}
        //Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);


        Ray raycast = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit raycastHit;

        if (Physics.Raycast(raycast, out raycastHit))
        {   
            //if user touch the icecream cone, then instantiate bezier to make icecream
            if (raycastHit.transform == transform)
            {
                distanceToCone = raycastHit.distance;
                GameObject curveObject = Instantiate(curvePrefab, raycastHit.transform.position, Quaternion.identity);
                curveObject.transform.parent = transform;
                firstTime = false;
            }
        }	
    }
}
