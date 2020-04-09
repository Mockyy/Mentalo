using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private DialogueText dialogue;

    [SerializeField]
    private bool isTrigger;

    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isTrigger)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isTrigger)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isTrigger)
        {
            if (Input.GetButtonDown("Talk") && !dialogueManager.isDialogueOnGoing)
            {
                dialogueManager.StartDialogue(dialogue);
            }
            if (Input.GetButtonDown("Talk") && dialogueManager.isDialogueOnGoing)
            {
                dialogueManager.DisplayNextSentence();
            }
        }
        else if (isTrigger)
        {
            if (Input.GetButtonDown("Talk") && dialogueManager.isDialogueOnGoing)
            {
                dialogueManager.DisplayNextSentence();
            }
        }
    }
}
