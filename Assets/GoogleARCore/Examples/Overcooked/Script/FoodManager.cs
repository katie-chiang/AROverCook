using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour {

	public GameObject cheesePosition;
	public GameObject steakPosition;
	public GameObject lettucePosition;
	public GameObject menuPosition;
	public GameObject shrimpPosition;
	public GameObject plate;

	//dictionary that stores the food in the scene
	//has the name of the gameobject as key
	public Dictionary<int, GameObject> foodDictionary = new Dictionary<int, GameObject>();

	//temporary lists for better organization in UI
	public List<GameObject> unprocessedPrefabList = new List<GameObject>();
	public List<GameObject> processedPrefabList = new List<GameObject>();
	public List<GameObject> equipmentsPrefabList = new List<GameObject>();

	//dictionary for prefab that's processed
	//this is used to decide what to instantiate after an object is destroyed
	private Dictionary<string, GameObject> unprocessedPrefabDictionary = new Dictionary<string, GameObject>();
	//has the tag of the game object as key
	private Dictionary<string, GameObject> processedPrefabDictionary = new Dictionary<string, GameObject>();

	//the parent that everything will be under
	Transform kitchen;

	// Use this for initialization
	void Start () {

		//get the parent that all food items will be under
		kitchen = equipmentsPrefabList[0].transform.parent;

		//instantiate the initial unprocessed food and organize prefabs into the dictionary
//		foreach (GameObject item in UnprocessedPrefabList) {
//			GameObject tempObject = Instantiate(item, item.transform.position, Quaternion.identity);
//			tempObject.transform.parent = kitchen;
//			foodDictionary.Add(tempObject.name, tempObject);
//		}

		GameObject tempObject = Instantiate(unprocessedPrefabList[0], steakPosition.transform.position, Quaternion.identity);
		tempObject.transform.parent = steakPosition.transform;
		unprocessedPrefabDictionary.Add (unprocessedPrefabList [0].tag, unprocessedPrefabList [0]);
		foodDictionary.Add(tempObject.GetInstanceID(), tempObject);

		tempObject = Instantiate(unprocessedPrefabList[1], cheesePosition.transform.position, Quaternion.identity);
		tempObject.transform.parent = cheesePosition.transform;
		unprocessedPrefabDictionary.Add (unprocessedPrefabList [1].tag, unprocessedPrefabList [1]);
		foodDictionary.Add(tempObject.GetInstanceID(), tempObject);

		tempObject = Instantiate(unprocessedPrefabList[2], lettucePosition.transform.position, Quaternion.identity);
		tempObject.transform.parent = lettucePosition.transform;
		unprocessedPrefabDictionary.Add (unprocessedPrefabList [2].tag, unprocessedPrefabList [2]);
		foodDictionary.Add(tempObject.GetInstanceID(), tempObject);

		tempObject = Instantiate(unprocessedPrefabList[3], shrimpPosition.transform.position, Quaternion.identity);
		tempObject.transform.parent = shrimpPosition.transform;
		unprocessedPrefabDictionary.Add (unprocessedPrefabList [3].tag, unprocessedPrefabList [3]);
		foodDictionary.Add(tempObject.GetInstanceID(), tempObject);

		//organize prefabs into the dictionary with tags as the key
		foreach (GameObject item in processedPrefabList) {
			processedPrefabDictionary.Add (item.tag, item);
		}

		FoodCooking.FinishProcessEvent += replaceFood;
		FoodCutting.FinishProcessEvent += replaceFood;
		BurgerScript.FinishBurgerEvent += createBurger;
		TacoScript.FinishTacoEvent += createTaco;
		FoodTracking.outOfBoundsEvent += produceFood;
		GameManager.nextMenuEvent += destroyFinishedFood;

		//give burger script the access to this script
		gameObject.GetComponent<BurgerScript>().foodManagerScript = this;
		gameObject.GetComponent<TacoScript> ().foodManagerScript = this;
	}

	// Update is called once per frame
	// check if food is moved 
	void Update () {

	}

	void replaceFood(GameObject unprocessedFood){

		Vector3 pos = unprocessedFood.transform.position;

		string tagToInstantiate = " ";

		//this for loop is used to get the tag (the key) of the food that needs to be instantiated
		//processed food name needs to contain the name of unprocessed food
		//ex: unprocessed: Steak -> processed: cookedSteak
		foreach (var item in processedPrefabDictionary) {

			//check which item contains the tag of the unprocessed food
			if(item.Value.tag.Contains(unprocessedFood.tag)){
				tagToInstantiate = item.Value.tag;
				break;
			}

		}

		if (tagToInstantiate != " ") {
			//the prefab to instatiate has the same tag as the gameobject to be destroyed
			GameObject toInstantiate = processedPrefabDictionary [tagToInstantiate];

			//remove the object to destroy from the dictionary and destroy it
			GameObject toDestroy = foodDictionary [unprocessedFood.GetInstanceID()];
			foodDictionary.Remove (unprocessedFood.GetInstanceID()); 
			Destroy (toDestroy);

			//instantiate the new object and add it in the dictionary
			GameObject tempObject = Instantiate (toInstantiate,pos,Quaternion.identity);
			tempObject.transform.parent = kitchen;
			foodDictionary.Add(tempObject.GetInstanceID(), tempObject);
		}

	}

	void createBurger(GameObject meat, GameObject lettuce, GameObject cheese){

		Vector3 pos = plate.transform.position;

		GameObject toDestroy = foodDictionary [meat.GetInstanceID()];
		foodDictionary.Remove (meat.GetInstanceID());
		Destroy (toDestroy);

		toDestroy = foodDictionary [lettuce.GetInstanceID()];
		foodDictionary.Remove (lettuce.GetInstanceID());
		Destroy (toDestroy);

		toDestroy = foodDictionary [cheese.GetInstanceID()];
		foodDictionary.Remove (cheese.GetInstanceID());
		Destroy (toDestroy);

		GameObject tempObject = Instantiate (processedPrefabDictionary ["burger"], pos, Quaternion.identity);
		tempObject.transform.parent = kitchen;
		foodDictionary.Add (tempObject.GetInstanceID(), tempObject);

		//Debug.Log ("# of food on scene: " + foodDictionary.Count);
	}

	void createTaco(GameObject shrimp, GameObject lettuce, GameObject cheese){

		Vector3 pos = plate.transform.position;

		foodDictionary.Remove (shrimp.GetInstanceID());
		Destroy (shrimp);

		foodDictionary.Remove (lettuce.GetInstanceID());
		Destroy (lettuce);

		foodDictionary.Remove (cheese.GetInstanceID());
		Destroy (cheese);

		GameObject tempObject = Instantiate (processedPrefabDictionary ["taco"], pos, Quaternion.identity);
		tempObject.transform.parent = kitchen;
		foodDictionary.Add (tempObject.GetInstanceID(), tempObject);

		Debug.Log ("# of food in scene: " + foodDictionary.Count);
	}

	void produceFood(string foodTag){
		GameObject prefabObject = unprocessedPrefabDictionary [foodTag];
		GameObject instantiatePosition;
		if (cheesePosition.name.Contains (foodTag)) {
			instantiatePosition = cheesePosition;
		} else if (lettucePosition.name.Contains (foodTag)) {
			instantiatePosition = lettucePosition;
		} else if(shrimpPosition.name.Contains(foodTag)){
			instantiatePosition = shrimpPosition;
		}else {
			instantiatePosition = steakPosition;
		}
		GameObject tempObject = Instantiate(prefabObject, instantiatePosition.transform.position, Quaternion.identity);
		tempObject.transform.parent = instantiatePosition.transform;
		foodDictionary.Add (tempObject.GetInstanceID (), tempObject);
	}

	void destroyFinishedFood(GameObject foodDone){

		foodDictionary.Remove(foodDone.GetInstanceID());
		Destroy (foodDone);

	}
}
