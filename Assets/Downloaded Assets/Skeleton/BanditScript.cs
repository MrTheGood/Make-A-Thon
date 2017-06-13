using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditScript : MonoBehaviour {

	public Transform player;
	public Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () 
	{
		Vector3 direction = player.position - this.transform.position;

		//=== Checken of de player binnen een bepaald gebied en het zicht( Angle )van de enemy vandaan is===
		if (Vector3.Distance (player.position, this.transform.position) < 40)
		{ 
			// Als het dus minder dan 10 is moet hij de player gaan volgen

			direction.y = 0;

			//hier rotate je met de slurp functie wat iets langzamer is en er natuurlijker uit ziet
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), -0.5f);

			// Animatie "idle" stopt nu omdat er nu gelopen gaat worden
			anim.SetBool ("isIdle", false);

			//als het nog boven deze afstand is
			if (direction.magnitude > 20) 
			{
				//Enemy volgt de player
				this.transform.Translate (0, 0, -0.02f);
				this.transform.Rotate (0,180,0);

				// Animatie "walking" start
				anim.SetBool ("isWalking", true);
				anim.SetBool ("idIdle", false);
			} 

			else 
			{
				anim.SetBool ("isRunning", true);
				anim.SetBool ("isWalking", false);
			}
		} 

		else 
		{
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isRunning", false);
		}
	}
}
