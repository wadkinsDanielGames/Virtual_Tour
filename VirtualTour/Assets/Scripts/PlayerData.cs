using UnityEngine;

public class PlayerData : MonoBehaviour {

	private Quest workingQuest;

	public void Start(){
		workingQuest = new Quest();

		QuestManager.current.setCurrentlyTrackedQuest(workingQuest);
	}
}