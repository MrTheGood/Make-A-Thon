using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dood2 : MonoBehaviour {
	
	//public Animator anim;

	// Use this for initialization
	void Start () 
	{
		//anim = GetComponent<Animator> ();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	
	}

	public void Animatie()
	{
		//anim.SetBool ("isDead", true);


		gameObject.GetComponent<Collider> ().enabled = false;

		Destroy (GetComponent<Chase> ());


	}
		
}
