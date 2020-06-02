using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Tooltip("Le scriptableObject contenant le dialogue")]
    [SerializeField]
    private DialogueText dialogue;

    [Tooltip("Si le dialogue se lance en entrant dans le trigger ou si il faut appuyer sur la touche de dialogue")]
    [SerializeField]
    private bool isTrigger;

    //Indique que le dialogue a déjà été vu pour ne pas le relancer
    private bool wasTriggeredOnce;

    //Le dialogue manager qui permet d'afficher le dialogue
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();  //Récupère la référence du dialogue manager
        wasTriggeredOnce = false;   //Le dialogue n'a pas encore été trigger
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetButtonDown("Talk") && !isTrigger)
        {
            if (!dialogueManager.isDialogueOnGoing)
            {
                dialogueManager.StartDialogue(dialogue);
            }
            else
            {
                dialogueManager.DisplayNextSentence();
            }
        }

        if (other.tag == "Player" && isTrigger && !wasTriggeredOnce)
        {
            if (!dialogueManager.isDialogueOnGoing)
            {
                dialogueManager.StartDialogue(dialogue);
            }
            else if (Input.GetButtonDown("Talk"))
            {
                if (dialogueManager.sentences.Count == 0)
                {
                    wasTriggeredOnce = true;
                }

                dialogueManager.DisplayNextSentence();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isTrigger)
        {
            gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !isTrigger)
        {
            gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);
        }
    }
}
