using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerScript : MonoBehaviour {

	//to assemble burger need to put plate + meat + cheese + lettuce
	//so check the position of those objects

	//this was assigned in the food manager script
	public FoodManager foodManagerScript;

	GameObject burger;
	GameObject meat;
	GameObject cheese;
	GameObject lettuce;

	//pass in the ingredients used to make the burger so we can destroy it
	public delegate void FinishBurger(GameObject meat, GameObject lettuce, GameObject cheese);
	public static event FinishBurger FinishBurgerEvent;

	Dictionary<int, GameObject> foodInScene;


	bool cookedMeatReady = false;
	bool lettuceReady = false;
	bool cutCheeseReady = false;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		//get the list of food cuurently in the scene
		foodInScene = foodManagerScript.foodDictionary;

		//loop through the dictioanry to check if the ingredients we need are in the scene
		foreach(var item in foodInScene){

			//Debug.Log ("lettuce bool: " + lettuceReady);

			if (item.Value.tag == "CookedSteak") {

				if (item.Value.GetComponent<FoodTracking> ().onPlate) {
					cookedMeatReady = true;
					meat = item.Value;
				}

			}
			if (item.Value.tag == "CutCheese") {

				if (item.Value.GetComponent<FoodTracking> ().onPlate) {
					cutCheeseReady = true;
					cheese = item.Value;
				}

			}
			if (item.Value.tag == "lettuce") {

				if (item.Value.GetComponent<FoodTracking> ().onPlate) {
					lettuceReady = true;
					lettuce = item.Value;
				}

			}
		}

		if (cookedMeatReady && cutCheeseReady && lettuceReady) {
			
				if (FinishBurgerEvent != null) {
					FinishBurgerEvent (meat, cheese, lettuce);
				}

		}

		cookedMeatReady = false;
		lettuceReady = false;
		cutCheeseReady = false;


	}

}
