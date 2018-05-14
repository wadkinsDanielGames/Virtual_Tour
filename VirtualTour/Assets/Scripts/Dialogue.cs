using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
//successful push
public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public bool opened;
    public string[] linesOfDialogue;
    public int lines;
    private PlayerController player;
    

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
       // npc = FindObjectOfType<NPCWalk>();
    }

    // Update is called once per frame
    void Update()
    {
        //This will turn off the dialogue box when all the lines have been exhausted, and will reset the starting line number.
        //Also will break the character out of the isTalking sequence that locks movement.
        if (lines >= linesOfDialogue.Length)
        {
            dialogueBox.SetActive(false);
            opened = false;
            lines = 0;
            player.isInteracting = false;
            //npc.talking = false;
            
            
        }

        dialogueText.text = linesOfDialogue[lines];
        
        if (opened == true && Input.GetKeyUp(KeyCode.Space))
        {
                lines++;   
        }
    }


    //This will load in the phrase to the dialogue box.
    public void DisplayBox(string phrase)
    {
        opened = true;
        dialogueBox.SetActive(true);
        dialogueText.text = phrase;
        if (lines != 0)
        {
            lines = 0;
        }
    }

    //This will lock player movement and open the dialogue box.
    public void ShowDialogue()
    {
        opened = true;
        if (lines != 0)
        {
            lines = 0;
        }
        dialogueBox.SetActive(true);
        player.isInteracting = true;
        
    }

}