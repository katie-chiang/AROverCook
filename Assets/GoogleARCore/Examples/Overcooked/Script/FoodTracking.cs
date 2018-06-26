using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//track the position of the food item
public class FoodTracking : MonoBehaviour {

	public bool onPlate = false;
	public bool firstTime = true;
	//public float distance = 0.3f;
	public GameObject plate;
	public GameObject foodPosition;
	public GameObject menuPosition;

	float plateXBound;
	float plateZBound;
	string foodTag;
	//public FoodManager foodManagerScript;

	public delegate void outOfBounds(string foodTag);
	public static event outOfBounds outOfBoundsEvent;

	public delegate void checkMenu(GameObject foodDone);
	public static event checkMenu checkMenuEvent;

	// Use this for initialization
	void Start () {

		plate = GameObject.FindGameObjectWithTag ("plate");
		foodPosition = GameObject.Find ("CheesePosition");
		menuPosition = GameObject.Find ("menuPosition");

		//Debug.Log (foodPosition.name);
		plateXBound = plate.GetComponent<BoxCollider> ().bounds.size.x;
		plateZBound = plate.GetComponent<BoxCollider> ().bounds.size.z;

		foodTag = gameObject.tag;
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log (plate.transform.position);

		float plateUpperX = plate.transform.position.x + plateXBound;
		float plateLowerX = plate.transform.position.x - plateXBound;

		float plateUpperZ = plate.transform.position.z + plateZBound;
		float plateLowerZ = plate.transform.position.z - plateZBound;

		//check if this game object is within the bounds of the plate
		if (plateUpperX > transform.position.x && plateLowerX < transform.position.x &&
		    plateUpperZ > transform.position.z && plateLowerZ < transform.position.z) {

			onPlate = true;

		} else {
			
			onPlate = false;

		}

		if (foodTag == "Cheese" || foodTag == "lettuce" || foodTag == "Steak" || foodTag == "Shrimp") {
			//check if the food item is in range
			if(firstTime && (transform.position.y > foodPosition.transform.position.y + 0.15f || transform.position.y < foodPosition.transform.position.y - 0.15f)){
				if (outOfBoundsEvent != null) {
					outOfBoundsEvent (gameObject.tag);
				}
				firstTime = false;
			}
		}

		if (foodTag == "burger") {

			//check if the burger is in the menu position
			if (menuPosition.transform.position.x + 0.2f > transform.position.x &&
			   menuPosition.transform.position.x - 0.2f < transform.position.x &&
			   menuPosition.transform.position.z + 0.2f > transform.position.z &&
			   menuPosition.transform.position.z - 0.2f < transform.position.z) {

				if (checkMenuEvent != null) {
					checkMenuEvent (gameObject);
				}

			}

		}

		if (foodTag == "taco") {
			
			if (menuPosition.transform.position.x + 0.2f > transform.position.x &&
				menuPosition.transform.position.x - 0.2f < transform.position.x &&
				menuPosition.transform.position.z + 0.2f > transform.position.z &&
				menuPosition.transform.position.z - 0.2f < transform.position.z) {

				if (checkMenuEvent != null) {
					checkMenuEvent (gameObject);
				}

			}
		}
			
	}

}
