using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class enemySight : MonoBehaviour {

		public UnityEngine.AI.NavMeshAgent agent;
		public ThirdPersonCharacter character;

		public enum State 
		{
			PATROL,
			CHASE,
			INVESTIGATE
		}

		public State state;
		private bool alive;

		// Variables for patrolling
		public GameObject[] waypoints;
		private int waypointInd;
		public float patrolSpeed = 0.5f;

		// Variables for chasing
		public float chaseSpeed = 1f;
		public GameObject target;

		// Variables for investigating
		private Vector3 investigateSpot;
		private float timer = 0;
		public float investigateWait = 10;

		//varibles for sight
		public float heightMultiplier;
		public float sightDist = 10;
		// Use this for initialization
		void Start () 
		{
			agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
			character = GetComponent<ThirdPersonCharacter>();

			agent.updatePosition = true;
			agent.updateRotation = false;

			waypoints = GameObject.FindGameObjectsWithTag ("Waypoint");
			waypointInd = Random.Range (0, waypoints.Length);

			state = enemySight.State.PATROL;

			alive = true;

			heightMultiplier = 1.36f;



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
				case State.CHASE:
					Chase ();
					break;
				case State.INVESTIGATE:
					Investigate ();
					break;
				}
				yield return null;
			}

		}

		void Update ()
		{
			StartCoroutine(FSM());
		}

		void Patrol()
		{
			agent.speed = patrolSpeed;
			if (Vector3.Distance (this.transform.position, waypoints [waypointInd].transform.position) >= 2) {
				agent.SetDestination (waypoints [waypointInd].transform.position);
				character.Move (agent.desiredVelocity, false, false);
			} else if (Vector3.Distance (this.transform.position, waypoints [waypointInd].transform.position) <= 2) 
			{
				waypointInd = Random.Range (0, waypoints.Length);
			}
			else 
			{
				character.Move (Vector3.zero, false, false);
			}
		}

		void Chase()
		{
			agent.speed = chaseSpeed;
			agent.SetDestination (target.transform.position);
			character.Move (agent.desiredVelocity, false, false);

		}

		void Investigate()
		{
			timer += Time.deltaTime;

						agent.SetDestination(this.transform.position);
						character.Move(Vector3.zero, false,false);
						transform.LookAt(investigateSpot);
						if (timer >= investigateWait)
						{
							state = enemySight.State.PATROL;
							timer = 0;
						}
		}
		void OnTriggerEnter (Collider coll)
		{
			if (coll.tag == "Player")
			{
								state = enemySight.State.INVESTIGATE;
				investigateSpot = coll.gameObject.transform.position;
				
			}
		}

		void FixedUpdate()
		{
			RaycastHit hit;
			Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
			Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
			Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);

			if (Physics.Raycast (transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
			{
				if (hit.collider.gameObject.tag == "Player")
				{
					state = enemySight.State.CHASE;
					target = hit.collider.gameObject;
				}
			}

			if (Physics.Raycast (transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
			{
				if (hit.collider.gameObject.tag == "Player")
				{
					state = enemySight.State.CHASE;
					target = hit.collider.gameObject;
				}
			}

			if (Physics.Raycast (transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
			{
				if (hit.collider.gameObject.tag == "Player")
				{
					state = enemySight.State.CHASE;
					target = hit.collider.gameObject;
				}
			}
		}
	}

}
