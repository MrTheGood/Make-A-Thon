using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_LOCKER : MonoBehaviour 
{
	void Awake() 
	{
		Application.targetFrameRate = 30;//Lock de framerate.
	}
}
