using System;
using UnityEngine;

public class InteractObjective : Objective {

	public InteractObjective(GameObject gameObject, String dialog, string objectivedialog) : base(gameObject, dialog, objectivedialog) { }
	public InteractObjective(GameObject gameObject, String dialog) : base(gameObject, dialog, "") { }

	public new void complete() {
		base.complete();
	}
}
