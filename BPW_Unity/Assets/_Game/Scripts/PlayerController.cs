using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
	[Header("Movements")]
	public Transform cam;
	public float walkSpeed;
	public float runSpeed;
	public float rotateSpeedH;
	public float rotateSpeedV;

	[Header("Attacks")]
	public float attackSpeed;
	public float attackDelay;
	public float attackStrength;

	[Header("Other")]
	public WorldManager worldManager;
	public Transform hand;
	private bool hasPickup = false;

	private void Start() {
		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = true;
	}

	void Update() {
		Movement();
	}

	//moves the character
	private void Movement() {
		//Movements
		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");
		Vector3 dirMove = new Vector3(hor, 0, ver);

		transform.Translate(dirMove.normalized * MoveSpeed() * Time.deltaTime, Space.Self);

		//Rotations
		float mouseHor = Input.GetAxis("Mouse X");
		float mouseVer = Input.GetAxis("Mouse Y");
		
		if(Mathf.Abs(mouseHor) >= Mathf.Abs(mouseVer)) {
			Vector3 rotHor = new Vector3(0, mouseHor, 0);
			transform.localEulerAngles += rotHor;
		} else if(Mathf.Abs(mouseHor) < Mathf.Abs(mouseVer)) {
			Vector3 rotVer = new Vector3(-mouseVer, 0, 0);
			cam.localEulerAngles += rotVer;
		} else {
			//transform.root.rotation = Quaternion.identity;
		}
	}

	//pickup items to expand the world
	private void Pickup(Transform item) {
		item.parent = hand;
		item.position = Vector3.zero;
		hasPickup = true;
		worldManager.SpawnItem();
	}

	private void OfferItem() {
		//remove item
		worldManager.Expand(5);
	}

	//toggles between running and walking speed
	private float MoveSpeed() {
		if(Input.GetButton("Jump")){
			return runSpeed;
		} else {
			return walkSpeed;
		}
	}

	private void OnTriggerEnter(Collider other) {
		if(other.tag == "Item") {
			Pickup(other.transform);
		}
	}
}
