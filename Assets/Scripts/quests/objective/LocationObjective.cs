using System;
using UnityEngine;

public class LocationObjective : Objective {
	[HideInInspector]public Vector3 location;
	public float range;

	public LocationObjective(GameObject gameObject, string dialog, string objectivedialog, float range) : base(gameObject, dialog, objectivedialog) {
		this.location = gameObject.transform.position;
		this.range = range;
	}

	void Start() {
		location = gameObject.transform.position;
	}

	public new void complete() {
		base.complete();
	}
}
