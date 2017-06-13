using UnityEngine;
using System.Collections;

public class ZoneChanger : MonoBehaviour {

	uGUIMinimap minimap;
	public string zoneName;

	void Start() {
		minimap = GameObject.FindGameObjectWithTag("Minimap").GetComponent<uGUIMinimap>();
	}

	void OnTriggerEnter(Collider col) {
		if(col.CompareTag("Player")) {
			minimap.headerAreaText.text = zoneName;
		}
	}
}
