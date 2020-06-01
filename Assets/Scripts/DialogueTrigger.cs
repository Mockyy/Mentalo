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

    private void Update()
    {
        //Si un dialogue est en cours et qu'on appuie sur la touche de dialogue
        if (Input.GetButtonDown("Talk") && dialogueManager.isDialogueOnGoing)
        {
            dialogueManager.DisplayNextSentence();  //On affiche la phrase suivante
        }
    }

    //En entrant dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        //Si c'est le joueur et que le dialogue n'est pas un trigger
        if (other.gameObject.CompareTag("Player") && !isTrigger)
        {
            transform.GetChild(0).gameObject.SetActive(true);   //On affiche la touche d'interaction au dessus de l'objet
        }
        //Sinon, si le dialogue est un trigger qui n'a pas été trigger
        else if (isTrigger && !wasTriggeredOnce)
        {
            wasTriggeredOnce = true;   //Pour ne pas retrigger le dialogue
            dialogueManager.StartDialogue(dialogue);    //On commence le dialogue
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Si le joueur quitte un dialogue qui n'est pas un trigger
        if (other.gameObject.tag == "Player" && !isTrigger)
        {
            transform.GetChild(0).gameObject.SetActive(false); //On désactive la touche d'interaction
        }
    }

    //Quand on reste dans la triggerBox
    private void OnTriggerStay(Collider other)
    {
        //Si le joueur est dans une box qui n'est pas un dialogue trigger
        if (other.CompareTag("Player") && !isTrigger)
        {
            //Si on appuie sur la touche de dialogue et que le dialogue n'a pas commencé
            if (Input.GetButtonDown("Talk") && !dialogueManager.isDialogueOnGoing)
            {
                Debug.Log("Dialogue triggered");
                dialogueManager.StartDialogue(dialogue);    //On commence le dialogue
            }
        }
    }
}
