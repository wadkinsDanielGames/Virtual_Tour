using UnityEngine;
using System.Collections;
using System;
public class DialogueInteraction : MonoBehaviour
{

    public static event Action<int> spokeWithNpc;

    public int questObjectID;

    private Dialogue dialogueManager;
    public string[] linesOfDialogue;
    public bool isColliding = false;

    public string[] alternativeDialogue;
    public bool level2CompleteDialogue = false;

    bool monsterLocated = false;
    // Use this for initialization
    void Start()
    {
        dialogueManager = FindObjectOfType<Dialogue>();
  
    }

    // Update is called once per frame
    void Update()
    {
        //If you're standing in the speech zone and press space, this will initiate the dialogue sequence.
        if (isColliding == true && monsterLocated == false)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (dialogueManager.opened == false)
                {
                    if (alternativeDialogue.Length == 0) 
                    { 
                        dialogueManager.linesOfDialogue = linesOfDialogue;
                        dialogueManager.lines = 0;
                        dialogueManager.ShowDialogue();

                        spokeWithNpc(questObjectID);
 
                    }
                    else
                    {
                    
                        dialogueManager.linesOfDialogue = alternativeDialogue;
                        dialogueManager.lines = 0;
                        dialogueManager.ShowDialogue();

                        spokeWithNpc(questObjectID);
                        
                    }
                }
            }
        }
    }



    //If you're within the collisionbox range, you can initiate the dialogue sequence.
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isColliding = true;
        }
    }

    //If you leave the collisionbox, you cannot initiate the dialogue sequence.
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isColliding = false;
        }
    }
}
