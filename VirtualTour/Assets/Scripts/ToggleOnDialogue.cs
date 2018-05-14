using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnDialogue : MonoBehaviour {
    public GameObject DialogueHolder;

    private void OnTriggerEnter(Collider other)
    {
        DialogueHolder.SetActive(true);
    }

}
