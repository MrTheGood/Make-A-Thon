using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class uGUIHealthController : MonoBehaviour {

	//Declareren van de sprite health
    private List<Image> HealthSprites;
	public List<Image> healthSprites {
        get {
            if(HealthSprites == null) {
                HealthSprites = new List<Image>();											
                GameObject[] sprites = GameObject.FindGameObjectsWithTag("HealthSprite");
                foreach(GameObject obj in sprites) {
                    HealthSprites.Add(obj.GetComponent<Image>());
                }
            }
            return HealthSprites;
        }
        set { HealthSprites = value; }
    }

	public PlayerHealth player { get; set; }

    private static uGUIHealthController instance;
    public static uGUIHealthController Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<uGUIHealthController> ();
                if (instance == null) {
                    GameObject obj = new GameObject ();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    instance = obj.AddComponent<uGUIHealthController> ();
                }
            }
            return instance;
        }
    }

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>(); //wanneer het spel start, haal de gameObject met de tag Player op
	}

	public void UpdateVitals() {
		for(int i = 0; i < healthSprites.Count; i++) {
			
			healthSprites[i].fillAmount = player.curHealth/player.maxHealth;
		}
	}
}
