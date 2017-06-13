using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest {
	public bool completed = false;
	
	int currentObjective;
	Objective[] objectives;

	public Quest(Objective[] objectives) {
		this.currentObjective = 0;

		foreach (Objective o in objectives) {
			o.gameObject.SetActive (!o.disableIfInactive);
			o.marker.SetActive (false);
		}

		this.objectives = objectives;
		objectives[0].gameObject.SetActive(true);
		objectives[0].marker.SetActive(true);
		GameHandler.showObjectiveDialog(objectives[0].objectivedialog);
	}

	public void completeObjective() {
		Objective objective = objectives[currentObjective];
		objective.gameObject.SetActive(!objective.disableIfInactive);
		objective.marker.SetActive(false);
		objective.complete();
		currentObjective++;

		if (currentObjective >= objectives.Length) {
			completed = true;
			GameHandler.hideObjectiveDialog();
			return;
		}
		objectives[currentObjective].gameObject.SetActive(true);
		objectives[currentObjective].marker.SetActive(true);
		GameHandler.changeObjectiveDialog(objectives[currentObjective].objectivedialog);
	}

	public Objective getObjective() {
		return objectives[currentObjective];
	}
}
