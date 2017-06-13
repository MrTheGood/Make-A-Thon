using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

	public AudioClip saw;

	// Use this for initialization
	void Start () 
	{
		GetComponent<AudioSource> ().playOnAwake = false;

		GetComponent<AudioSource> ().clip = saw;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Vijand") 
		{
			GetComponent<AudioSource> ().Play ();
		}
	}
}

