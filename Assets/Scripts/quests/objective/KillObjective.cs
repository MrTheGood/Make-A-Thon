using System;
using UnityEngine;

public class KillObjective : Objective {

	public KillObjective(GameObject gameObject, string dialog, string objectivedialog) : base(gameObject, dialog, objectivedialog) {}

	public new void complete() {
		base.complete();
	}
}
