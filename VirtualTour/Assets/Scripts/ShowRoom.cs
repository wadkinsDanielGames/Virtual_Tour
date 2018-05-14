using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(BoxCollider))]

public class ShowRoom : MonoBehaviour {
    //Player collides with box
    //Player presses a button
    //Image pops up
    //Player is locked in place
    //Player presses button and picture goes away
    //in place lock releases

    public RawImage roomPicturePanel;
    public Texture roomPicture;
    private bool inside;

	// Use this for initialization
	void Start () {
        roomPicturePanel.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (inside) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (roomPicturePanel.enabled == false)
                {
                    roomPicturePanel.enabled = true;
                    roomPicturePanel.texture = roomPicture;
                }
                else { roomPicturePanel.enabled = false; }
            }

        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inside = false;
        }
    }
}
