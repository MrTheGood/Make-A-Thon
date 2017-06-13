using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {
	public static Quest mainQuest;
	public static GameHandler gameHandler;
	public static GameObject dialogObject;

	public GameObject dialog;
	public GameObject objectiveDialog;
	public Objective[] mainObjectives;

	private static bool paused;


	// Use this for initialization
	void Awake () {
		//Make sure the game object is this.
		if (gameHandler == null)
			gameHandler = this;

		if (gameHandler != this) {
			Destroy(this);
			return;
		}

		mainQuest = new Quest(mainObjectives);

		dialogObject = dialog;
	}
	
	// Update is called once per frame
	void Update () {
		if (paused) {
			if (Input.anyKey) {
				hideDialog();
			}
		}
	}

	public static void showDialog(string dialog) {
		paused = true;
		dialogObject.SetActive(true);
		dialogObject.GetComponentInChildren<Text>().text = dialog;
	}

	public static void hideDialog() {
		dialogObject.SetActive(false);
		paused = false;
	}

	public static bool isPaused() {
		return paused;
	}


	public static void showObjectiveDialog(string dialog) {
		gameHandler.objectiveDialog.SetActive(true);
		gameHandler.objectiveDialog.GetComponentInChildren<Text>().text = dialog;
	}

	public static void changeObjectiveDialog(string dialog) {
		gameHandler.objectiveDialog.GetComponentInChildren<Text>().text = dialog;
	}

	public static void hideObjectiveDialog() {
		gameHandler.objectiveDialog.SetActive(false);
	}
}
