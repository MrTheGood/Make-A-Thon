using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EindTitelControler : MonoBehaviour 
{
	public float speed = 0.025f;

	void FixedUpdate () 
	{
		transform.Translate(0f, speed, 0f);
	}
}