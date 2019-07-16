using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour{
	[Header("Flooffy")]
	public Transform flooffyPrefab;
	public Transform flooffyFamilly;
	public float spawnDelay = 10;

	[Header("Item")]
	public Transform[] itemPrefab = new Transform[1];

	[Header("UI")]
	public Text flooffyCountTxt;
	public Slider overpopSld;

	[Header("Other")]
	public Transform ground;
	public float radiusCorrector;

	private int flooffyCounter = 2;
	private float worldScale;
	private bool gameover = false;

	private void Awake() {
		worldScale = ground.localScale.x + ground.localScale.z;
		SpawnItem();
		StartCoroutine(NewFlooffy());
		OverpopulationUI();
	}

	void Update(){
		//expand upon button only for debugging
		if(Input.GetButtonDown("Jump")) {
			Expand(5);
			Debug.LogWarning("remove debugging expand");
		}
	}

	//expand the world
	public void Expand(float size) {
		ground.localScale += new Vector3(size, 0, size);
		worldScale = ground.localScale.x + ground.localScale.z;
		OverpopulationUI();
	}

	//spawn an item which expands the world
	public void SpawnItem() {
		int id = Random.Range(0, itemPrefab.Length - 1);
		Instantiate(itemPrefab[id], RandomPos(), Quaternion.identity);
	}

	//check for gameover and spawn flooffy 
	void SpawnFlooffy() {
		if (worldScale > flooffyCounter) {
			Transform newFlooffy = Instantiate(flooffyPrefab, RandomPos(), Quaternion.identity);
			newFlooffy.parent = flooffyFamilly;
			flooffyCounter++;
			flooffyCountTxt.text = "" + flooffyCounter + "/100";
			OverpopulationUI();
		} else {
			gameover = true;
		}
	}

	void OverpopulationUI() {
		overpopSld.value = flooffyCounter / worldScale;
	}

	public Vector3 RandomPos() {
		Vector3 pos = new Vector3(RandomRadius(), 0, RandomRadius());
		
		while (Vector3.Distance(Vector3.zero, pos) >= WorldRadius()) {
			pos = new Vector3(RandomRadius(), 0, RandomRadius());
		}
		pos.y += 5;
		
		return pos;
	}

	private float RandomRadius() {
		return Random.Range(-WorldRadius(), WorldRadius());
	}

	private float WorldRadius() {
		float groundChild = ground.GetChild(0).transform.localScale.x;
		float groundself = ground.localScale.x;

		return (groundself * groundChild)/2 - radiusCorrector;
	}

	//Spawn a new flooffy every couple seconds as long as the game is going on
	IEnumerator NewFlooffy() {	
		while(!gameover) {
			yield return new WaitForSeconds(spawnDelay);
			SpawnFlooffy();
		}
	}
}