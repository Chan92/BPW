using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour{
	public WorldManager worldManager;

	[SerializeField]
	private NavMeshAgent agent;

	private Vector3 destPos;

	void Start() {
		agent = transform.GetComponent<NavMeshAgent>();
		worldManager = FindObjectOfType<WorldManager>();
		destPos = worldManager.RandomPos();
	}

	void Update(){
		Movement();
	}

	void Movement() {
		if (Vector3.Distance(transform.position, destPos) > 6) {
			agent.SetDestination(destPos);
		} else {
			destPos = worldManager.RandomPos();
		}
	}
}
