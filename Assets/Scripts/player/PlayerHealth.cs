using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	//variabele declareren
    private float CurHealth; 
	public float curHealth {
        get { return CurHealth; }
        set {
        	CurHealth = value;
            if(CurHealth > maxHealth) {  
                CurHealth = maxHealth; 	//current health = max health
            }
            if(CurHealth < 0) { 		//als de health op 0 is
                Die();					// is de speler dood
                curHealth = 0;
            }
        }
    }
	public GameObject GameOverPanel;
	public GameObject ClickToGo;
	private int secondsAfterLosing;

    [SerializeField]
    private float MaxHealth;			//maximale health
	public float maxHealth {
        get { return MaxHealth; }
        set { MaxHealth = value; }
    }

    [SerializeField]
    private float HealthRegenSpeed;		//health automatisch erbij
	public float healthRegenSpeed {
        get { return HealthRegenSpeed; }
        set { HealthRegenSpeed = value; }
    }
		
	void Start () {
        curHealth = maxHealth; //als de applicatie start, begint de health op het maximale
		GameOverPanel.SetActive(false);
		secondsAfterLosing = 0;
	}
		
	void Update () {

		curHealth += Time.deltaTime * healthRegenSpeed; //op verloop van tijd zal er weer iets van health toegevoegd worden.

		uGUIHealthController.Instance.UpdateVitals();	//update de health

		if (GameOverPanel.activeInHierarchy) {
			if (secondsAfterLosing > 350) {
				ClickToGo.SetActive (true);
			} else {
				secondsAfterLosing++;
			}
		}

		//Dit wordt al uitgevoerd in ZwaardGamageScript2. 
	/*	if (ClickToGo.activeInHierarchy) {
			if (Input.GetMouseButton(0)) {
				WorldManager.worldManager.loadHub ();
			}
		}*/
	}

	void OnTriggerEnter (Collider col)
	{
		//Als de player in contact komt met de enemy
		if (col.gameObject.tag == "Sword") 
		{
			curHealth -= Random.Range (30, 60); 			//gaat er random tussen 10-20 health af
			uGUIHealthController.Instance.UpdateVitals ();	//update de health
		}

		//Als de player in contact komt met de potion
		if (col.gameObject.tag == "Potion") 
		{
			curHealth += 20;								//komt er 20 health bij
			uGUIHealthController.Instance.UpdateVitals ();	//update de health
		}
	}

	//Functie speler dood
    public void Die() {
        Debug.Log("Player died."); //in console staat dat de player dood is
		GameOverPanel.SetActive(true);
    }
}
