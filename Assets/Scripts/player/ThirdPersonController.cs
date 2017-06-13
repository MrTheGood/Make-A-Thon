using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThirdPersonController : MonoBehaviour {
	NavMeshAgent agent;
	GameObject MainCamera;
	public float pathEndThreshold = 0.1f;
	private bool hasPath = false;
	Vector3 targetDestination;  // Position to walk to
	GameObject targetObject;    // Object to interact with when reaching a position

	Animation anim;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		agent.angularSpeed = 2000;
		agent.speed = 6;
		anim = GetComponent<Animation> ();
		targetDestination = Vector3.zero;
		MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		MainCamera.transform.rotation = Quaternion.Euler(55,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameHandler.isPaused())
			return;

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;

			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
				//Set the target object if gameObject != null
				if (hit.collider.gameObject != null) {
					targetObject = hit.collider.gameObject;
				}
				targetDestination = hit.point;
				if (TPCSounds.audio.clip != TPCSounds.tpcSounds.run && !TPCSounds.audio.isPlaying) {
					TPCSounds.PlaySoundLoop (TPCSounds.tpcSounds.run, 0.7f);
				}
				anim["run"].speed = 1.7f;
				anim.CrossFade ("run");
			}
		}
		agent.destination = targetDestination;

		if (AtEndOfPath()) {
			anim.CrossFade("idle1");
			TPCSounds.StopSoundLoop ();
			onReachPosition ();
		}

		if (Input.GetKeyDown (KeyCode.Y)) {
			TPCSounds.PlayExtraSound(TPCSounds.tpcSounds.yeah, 1f);
		}

		MainCamera.transform.position = new Vector3 (transform.position.x - 1, transform.position.y + 12, transform.position.z - 8);
		AtEndOfPath ();
	}

	/// <summary>
	/// Executed when the hub-player reaches a position
	/// 
	/// sets targetPosition to Vector3.zero
	/// sets targetObject to null
	/// </summary>
	void onReachPosition() {
		if (targetObject != null) {
			foreach (InteractObjective v in NPCManager.npcManager.villagers) {
				if (v.gameObject.GetInstanceID() == targetObject.GetInstanceID()) {
					v.complete();
				}
			}

			if (targetObject.tag == "Vijand") {
				WorldManager.worldManager.loadForestCombat(targetObject);
				return;
			}
		}

		if (GameHandler.mainQuest.completed) {
			targetObject = null;
			targetDestination = Vector3.zero;
			return;
		}

		//Get the current mainquest objective
		Objective objective = GameHandler.mainQuest.getObjective();


		//Is the target position set?
		if (targetDestination == Vector3.zero) {
			targetObject = null;
			return;
		}

		string type = objective.GetType().ToString();
		//If the objective is a location objective
		if (type == "LocationObjective") {
			Vector3 p = transform.position;
			Vector3 op = ((LocationObjective)objective).location;
			//If the player is near an objective.
			if (Vector3.Distance(((LocationObjective)objective).location, transform.position) <= ((LocationObjective)objective).range) {
				GameHandler.mainQuest.completeObjective();
			}
		}

		if (targetObject == null) {
			targetDestination = Vector3.zero;
			return;
		}


		if (targetObject.tag == "Galazar" && objective.gameObject.GetInstanceID() == targetObject.GetInstanceID()) {
			WorldManager.worldManager.loadBossCombat (targetObject);
		}

		if (type == "InteractObjective") {
			if (objective.gameObject.GetInstanceID() == targetObject.GetInstanceID()) {
				GameHandler.mainQuest.completeObjective();
			}
		}
		
		targetObject = null;
		targetDestination = Vector3.zero;
	}

	bool AtEndOfPath() {
		hasPath |= agent.hasPath;
		if (hasPath && agent.remainingDistance <= agent.stoppingDistance + pathEndThreshold ) {
			// Arrived
			hasPath = false;
			anim.CrossFade("idle1");
			return true;
		}
		return false;
	}
}
