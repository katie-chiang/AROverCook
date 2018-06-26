using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public List<GameObject> menuPrefabs = new List<GameObject> ();

	GameObject menuPosition;
	GameObject currentMenu;
	GameObject kitchen;

	bool firstTime = true;

	public delegate void nextMenu(GameObject foodDone);
	public static event nextMenu nextMenuEvent;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {

//		kitchen = GameObject.Find ("kitchen_01");
//		if (kitchen != null) {
//			Debug.Log (kitchen.name);
//		}
//
//		Debug.Log ("here");

		//wait for the kitchen to be set up to initialize things
		if (firstTime && (kitchen = GameObject.Find ("kitchen_01")) != null) {

			FoodTracking.checkMenuEvent += updateMenu;
			menuPosition = GameObject.Find ("menuPosition");

			currentMenu = Instantiate (menuPrefabs [0], menuPosition.transform.position , Quaternion.identity, menuPosition.transform);
			currentMenu.transform.localEulerAngles = new Vector3(90, 0, 0);
			currentMenu.transform.localPosition = currentMenu.transform.localPosition + new Vector3 (-0.05f, 0.05f, 0.05f);

			//set up the first menu
			firstTime = false;
		}

	}

	void updateMenu(GameObject foodDone){

		//check if it's the correct item
		if (currentMenu.tag == foodDone.tag) {

			//destroy the food
			if (nextMenuEvent != null) {
				nextMenuEvent (foodDone);
			}

			Destroy (currentMenu);
			currentMenu = Instantiate (menuPrefabs [1], menuPosition.transform.position , Quaternion.identity, menuPosition.transform);
			currentMenu.transform.localEulerAngles = new Vector3(90, 0, 0);
			currentMenu.transform.localPosition = currentMenu.transform.localPosition + new Vector3 (-0.05f, 0.05f, 0.05f);
		}
	}
}
