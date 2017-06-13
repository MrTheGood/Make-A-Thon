using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Objective : MonoBehaviour {
	public new GameObject gameObject;
	public GameObject marker;
	public string objectivedialog;
	public string dialog;
	public bool disableIfInactive = false;
	public bool disableAfterCompletion = false;

	public Objective(GameObject gameObject, string dialog, string objectivedialog) {
		this.gameObject = gameObject;
		this.dialog = Regex.Unescape(dialog);
		this.objectivedialog = Regex.Unescape(objectivedialog);
	}

	public void complete() {
		GameHandler.showDialog(dialog);

		if (disableAfterCompletion)
			gameObject.SetActive(false);
	}
}