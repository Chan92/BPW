using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
	[Header("Movements")]
	public float walkSpeed;
	public float runSpeed;
	public float rotateSpeed;

	[Header("Attacks")]
	public float attackSpeed;
	public float attackDelay;
	public float attackStrength;

	void Start() {
		
    }

	void Update() {
		Movement();
	}

	//moves the character
	private void Movement() {
		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");
		Vector3 dirMove = new Vector3(hor, 0, ver);

		float mouseHor = Input.GetAxis("Mouse X");
		Vector3 dirRot = new Vector3(0, mouseHor, 0);

		transform.root.Translate(dirMove.normalized * MoveSpeed() * Time.deltaTime, Space.Self);
		transform.root.Rotate(dirRot.normalized * rotateSpeed);
		
	}

	/*
	private void Movement() {
		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");
		Vector3 dirMove = new Vector3(0, 0, ver);
		Vector3 dirRot = new Vector3(0, hor, 0);

		transform.root.Translate(dirMove.normalized * MoveSpeed() * Time.deltaTime, Space.Self);
		transform.root.Rotate(dirRot.normalized * rotateSpeed);
	}*/

	private void Attack() {
		// Ray ray = camera.main.screenpointtoray(input.mouseposition);
		//RaycastHit hit;
		//if physycs.raycast(ray.orgin, ray direction, out hit){
		//	}
	}

	//toggles between running and walking speed
	private float MoveSpeed() {
		if(Input.GetButton("Jump")){
			return runSpeed;
		} else {
			return walkSpeed;
		}
	}
}
