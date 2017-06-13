using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {
	public static WorldManager worldManager;
	public GameObject hub;
	public GameObject forestCombatPrefab;
	public GameObject bossCombatPrefab;
	public GameObject banditCampCombat;

	GameObject forestCombatInstance;
	GameObject bossCombatInstance;
	[HideInInspector]public GameObject hubEnemyReference;

	void Awake() {
		if (worldManager == null)
			worldManager = this;

		if (worldManager != this) {
			Destroy(this);
			return;
		}
		
		loadHub();
	}

	public void loadHub() {
		disableAll();
		hub.SetActive(true);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void loadForestCombat(GameObject hubEnemyReference) {
		disableAll();
		forestCombatInstance = Instantiate(forestCombatPrefab);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		this.hubEnemyReference = hubEnemyReference;
	}

	public void killedEnemy(bool killedEnemy) {
		if (killedEnemy) {
			if (!GameHandler.mainQuest.completed) {
				Objective o = GameHandler.mainQuest.getObjective ();
				if (o.gameObject.GetInstanceID () == hubEnemyReference.GetInstanceID ()) {
					GameHandler.mainQuest.completeObjective ();
					print ("KillObjective Completed.");
				}
			}
			Destroy (hubEnemyReference);
		}
		hubEnemyReference = null;
	}

	public void loadBossCombat(GameObject hubEnemyReference) {
		disableAll();
		bossCombatInstance = Instantiate(bossCombatPrefab);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		this.hubEnemyReference = hubEnemyReference;
	}

	public void loadBanditCampCombat() {
		disableAll();
		banditCampCombat.SetActive(true);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void disableAll() {
		hub.SetActive(false);
		Destroy(forestCombatInstance);
		forestCombatInstance = null;
		Destroy (bossCombatInstance);
		bossCombatInstance = null;
		banditCampCombat.SetActive(false);
	}
}
