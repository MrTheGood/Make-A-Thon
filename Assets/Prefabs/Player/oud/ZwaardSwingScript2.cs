using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZwaardSwingScript2 : MonoBehaviour 
{
	public Quaternion startRotatie;//Rotatie Declaratie
	float draaiSnelheid = 500f;//De draaisnelheid
	bool zwaardSwing = false;//De zwaard staat standaard stil
	float timer = 0.35f;//Timer voor de swing van het zwaard


	void Start () 
	{
		transform.rotation = startRotatie;//Geef aan in een public hoe het zwaard moet staan als je niet swingt.
	}


	void FixedUpdate () 
	{
		//Wanneer je de linker muisknop in klikt, dan sla je met je zwaard.
		if (Input.GetMouseButtonDown(0)) 
		{
			zwaardSwing = true;
		}

		//De afvraging wanneer je slaat, dan zorg je ervoor dat je zwaard de goede richting opslaat en dat hij een timer aanzet.
		if (zwaardSwing == true) 
		{
			transform.Rotate (Vector3.down * Time.deltaTime * (draaiSnelheid / 10));
			transform.Rotate (Vector3.right * Time.deltaTime * draaiSnelheid);

			timer -= Time.deltaTime;
			//Wanneer de timer op 0 staat, zet de rotatie weer terug.
			if (timer < 0) 
			{
				timer = 0.2f;
				zwaardSwing = false;
				transform.rotation = startRotatie;
			}
		}
	}

}
//Gemaakt door: Xavier ten Hove + Met een beetje hulp van Maarten de Goede.