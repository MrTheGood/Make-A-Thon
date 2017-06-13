using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZwaardSwingScript : MonoBehaviour 
{
	bool zwaardSwing = false;//De zwaard staat standaard stil

	public BoxCollider boxCol;//Dit is om de box collider uit te zetten.


	void Awake()
	{
		
	}


	void Start () 
	{
		boxCol = GetComponent<BoxCollider>();
	}


	void Update () 
	{
		//Wanneer je de linker muisknop in klikt, dan sla je met je zwaard.
		if (Input.GetMouseButtonDown(0) && !gameObject.GetComponent<Animation>().isPlaying) 
		{
			zwaardSwing = true;
		}

		//De afvraging wanneer je slaat, dan zorg je ervoor dat je zwaard de goede richting opslaat en dat hij een timer aanzet.
		if (zwaardSwing == true) 
		{
			//transform.Rotate (Vector3.down * Time.deltaTime * (draaiSnelheid / 10));
			//transform.Rotate (Vector3.right * Time.deltaTime * draaiSnelheid);
			//Dit is om de box collider aan te zetten.
			//Wanneer de timer op 0 staat, zet de rotatie weer terug.
			if (!gameObject.GetComponent<Animation>().isPlaying) {
				zwaardSwing = false;
				gameObject.GetComponent<Animation> ().Play ();
			}

		}

		if (!gameObject.GetComponent<Animation> ().isPlaying) {
			boxCol.enabled = false;//Dit is om de box collider uit te zetten.
		}
		else {
			boxCol.enabled = true;
		}
	}

}
//Gemaakt door: Xavier ten Hove + Met een beetje hulp van Maarten de Goede.