using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//The names of these states can change 
public enum DialogueState {INTRO,INFORMATION,FIELD,EXIT };

public class DialogueSystem : MonoBehaviour {

    public int questObjectID;

    public List<string> professorResponses = new List<string>(); //List of the professor's responses - edit values in the editor
    public List<string> playerResponse = new List<string>(); //List of the player's responses - edit values in the editor
    public List<Text> buttonText = new List<Text>(); //List of the text elements of the buttons - will be set from player responses
    public Text professorReponse;

    public GameObject canvasHolder;
    public bool active = false;
    public DialogueState _dialogueState;

    public static event Action<bool> inMenu;
    public static event Action<int> spokeWithNpc;
    
    //on awake set the button texts
    private void Awake()
    {
        for (int i = 0; i < buttonText.Count; i++) {
            buttonText[i].text = playerResponse[i];
        }
    }

    void Start () {
        _dialogueState = DialogueState.INTRO;
        canvasHolder.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        SwapState();
	}

    void SwapState() {

        switch (_dialogueState) {
            case DialogueState.INTRO:
                professorReponse.text = professorResponses[(int)_dialogueState];
                break;

            case DialogueState.INFORMATION:
                professorReponse.text = professorResponses[(int)_dialogueState];
                break;

            case DialogueState.FIELD:
                professorReponse.text = professorResponses[(int)_dialogueState];
                break;

            case DialogueState.EXIT:
                professorReponse.text = professorResponses[(int)_dialogueState];
                //send a false notifcation out to the player that we are not in the menu
                if (inMenu != null)
                {
                    inMenu(false);
                }
                break;

        }
    }

    //when player enters trigger open up the canvas
    private void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.tag == "Player") {
            if(Input.GetKeyDown(KeyCode.E)) {
                if( active == false){
                    _dialogueState = DialogueState.INTRO;
                //send a true notifcation out to the player that we are in the menu
                    if (inMenu!= null) {
                        Cursor.lockState = CursorLockMode.None;
                        inMenu(true);
                    }
                    //show the canvas
                    canvasHolder.SetActive(true);
                    //probably send a notification to the player that they are in the menu
                    if(spokeWithNpc != null){
                        Debug.Log(questObjectID + "Dialogue");
                        spokeWithNpc(questObjectID);  
                    }
                    active = true;

                } else {
                    Cursor.lockState = CursorLockMode.Locked;
                    canvasHolder.SetActive(false);
                    _dialogueState = DialogueState.EXIT;
                    active = false;
                }
           
                
            }
            
        }
    }

    //close canvas when player leaves
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            active = false;
            //hide the canvas
            canvasHolder.SetActive(false);
            _dialogueState = DialogueState.EXIT;

            //probably send a notification to the player that they are in the menu
        }
    }

    public void Button1() {
        _dialogueState = DialogueState.INTRO;
    }

    public void Button2()
    {
        _dialogueState = DialogueState.INFORMATION;
    }

    public void Button3()
    {
        _dialogueState = DialogueState.FIELD;
    }

    public void Button4()
    {
        _dialogueState = DialogueState.EXIT;
    }

}
