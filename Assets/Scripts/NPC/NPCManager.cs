using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {
	public static NPCManager npcManager;
	public GameObject lewisLeventhorp;
	public GameObject edmondWylde;
	public GameObject thomasGrifford;
	public GameObject rogerGrifford;
	public GameObject hughBarnes;
	public GameObject davidMorley;
	public GameObject raynardCoffyn;
	public GameObject piersMason;

	public Objective[] villagers;

	void Start() {
		if (npcManager == null)
			npcManager = this;
		if (npcManager != this) {
			Destroy(this);
			return;
		}

		List<Objective> vil = new List<Objective>();
		vil.Add(new InteractObjective(lewisLeventhorp, "Lewis Leventhorp: \nSomeone needs to do something about these monsters.."));
		vil.Add(new InteractObjective(edmondWylde, "Edmond Wylde: \nIf you're looking for Agnes, she's inside washing the dishes."));
		vil.Add(new InteractObjective(thomasGrifford, "Thomas Grifford: \nRoger and I have been adventuring together for ages but it became too dangerous so we stopped."));
		vil.Add(new InteractObjective(rogerGrifford, "Roger Grifford: \nHuh? Oh, hello. You probably want to talk to my brother Thomas. Everyone does."));
		vil.Add(new InteractObjective(hughBarnes, "Hugh Barnes: \nMoonbright is especially beautiful today, isn't it?"));
		vil.Add(new InteractObjective(davidMorley, "David Morley: \nI wish Raynard was here.."));
		vil.Add(new InteractObjective(raynardCoffyn, "Raynard Coffyn: \nHmm? Sorry, I was thinking about David.."));
		vil.Add(new InteractObjective(piersMason, "Piers Mason: \nBack in my day we just didn't care about the undead.. Or we did. I don't remember."));
		villagers = vil.ToArray();
	}
}
