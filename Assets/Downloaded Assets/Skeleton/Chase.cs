using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chase : MonoBehaviour 
{

	public Transform player;
	public Animator anim;


	//Het aanmaken van de Health
	public const int maxHealth = 100;
	public int currentHealth = maxHealth;
	public RectTransform healthBar;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		GetComponent<Chase> ().enabled = true;

	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 direction = player.position - this.transform.position;
		float angle = Vector3.Angle (direction, this.transform.forward);
		//=== Checken of de player binnen een bepaald gebied en het zicht( Angle )van de enemy vandaan is===
		if (Vector3.Distance (player.position, this.transform.position) < 10 && angle < 140)
		{
			// Als het dus minder dan 10 is moet hij de player gaan volgen

			direction.y = 0;

			//hier rotate je met de slurp functie wat iets langzamer is en er natuurlijker uit ziet
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);

			// Animatie "idle" stopt nu omdat er nu gelopen gaat worden
			anim.SetBool ("isIdle", false);

			//als het nog boven deze afstand is
			if (direction.magnitude > 2) 
			{
				//Enemy volgt de player
				this.transform.Translate (0, 0, 0.04f);
				this.transform.LookAt (player);

				if (GetComponent<EnemAI_Patrol>() != null)
					GetComponent<EnemAI_Patrol> ().enabled = false;
				// Animatie "walking" start
				anim.SetBool ("isAttacking", false);
				anim.SetBool ("isWalking", true);
			}

			else 
			{
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isAttacking", true);
			}
		}

		else 
		{
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isAttacking", false);
			//anim.SetBool ("isIdle", true);
			if (GetComponent<EnemAI_Patrol>() != null)
				GetComponent<EnemAI_Patrol> ().enabled = true;
		}

	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		if (currentHealth <= 0) 
		{
			// Vijand is geraakt/ vermoord, Activeer de animatie functie in een script op een ander GameObject.
			if (GetComponent<Dood>() != null)
				GetComponent<Dood> ().Animatie ();

			currentHealth = 0;
			GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<ZwaardDamageScript2> ().Optellen ();

		}

		healthBar.sizeDelta = new Vector2 (currentHealth, healthBar.sizeDelta.y);
	}
}
