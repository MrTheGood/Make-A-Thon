using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
public class WayPoints : MonoBehaviour {

		public UnityEngine.AI.NavMeshAgent agent;
	public ThirdPersonCharacter character;

	public enum State 
	{
		PATROL
		
	}

	public State state;
	private bool alive;

	// Variables for patrolling
	public GameObject[] waypoints;
	private int waypointInd = 0;
	public float patrolSpeed = 0.5f;

	// Variables for chasing


	// Use this for initialization
	void Start () 
	{
			agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		character = GetComponent<ThirdPersonCharacter>();

		agent.updatePosition = true;
		agent.updateRotation = false;

	//	waypoints = GameObject.FindGameObjectsWithTag ("Waypoint");
		//waypointInd = Random.Range (0, waypoints.Length);

			state = WayPoints.State.PATROL;

			alive = true;

			// START FSM

	}
		void Update ()
		{
			StartCoroutine(FSM());
		}

	IEnumerator FSM()
	{
		while (alive) 
		{

			switch (state) 
			{
			case State.PATROL:
				Patrol ();
				break;

			}
			yield return null;
		}

	}

	

	void Patrol()
	{
		agent.speed = patrolSpeed;
		if (Vector3.Distance (this.transform.position, waypoints [waypointInd].transform.position) >= 2) 
			{
			agent.SetDestination (waypoints [waypointInd].transform.position);
			character.Move (agent.desiredVelocity, false, false);
			} 
			else if (Vector3.Distance (this.transform.position, waypoints [waypointInd].transform.position) <= 2) 
		{
				waypointInd += 1; 
				if(waypointInd > waypoints.Length)
				{
					waypointInd = 0;
				}
		}
		else 
		{
			character.Move (Vector3.zero, false, false);
		}
	}

		void OnTriggerEnter (Collider coll)
		{
			
		}


}

}
