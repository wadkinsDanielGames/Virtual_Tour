using System.Collections.Generic;

public class Quest{

	private Dictionary<int, bool> requirements;
	private bool isCompleted;

	public Quest(){
		requirements = new Dictionary<int, bool>();		
		setUpQuestDefault();
		isCompleted = false;
	}

	public void updateRequirements(int questObjectID){

		if (requirements.ContainsKey(questObjectID)){
			requirements[questObjectID] = true;
		}
	}

	private void setUpQuestDefault(){

		requirements.Add(1, false);
		requirements.Add(2, false);
		requirements.Add(3, false);
		requirements.Add(4, false);
		requirements.Add(5, false);
		requirements.Add(6, false);
		requirements.Add(7, false);
		requirements.Add(8, false);
		requirements.Add(9, false);
		requirements.Add(10, false);
		requirements.Add(11, false);
		requirements.Add(12, false);
		requirements.Add(13, false);
		requirements.Add(14, false);
		requirements.Add(15, false);

	}

	public bool isQuestDone(){

		int questCompleted = 0;

		foreach(KeyValuePair<int, bool> kvp in requirements){
			
			if(kvp.Value == true){
				questCompleted++;

			}else if (kvp.Value == false){
				break;
			}
		}

		if(questCompleted == requirements.Count){
			isCompleted = true;
		}

		return isCompleted;
	}

	public Dictionary<int,bool> getRequirements(){
		return requirements;
	}
}