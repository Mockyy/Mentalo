using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Tooltip("Le scriptableObject contenant le dialogue")]
    [SerializeField]
    private DialogueText[] dialogue = default;

    [Tooltip("Si le dialogue se lance en entrant dans le trigger ou si il faut appuyer sur la touche de dialogue")]
    [SerializeField]
    private bool isTrigger = default;

    //Indique que le dialogue a déjà été vu pour ne pas le relancer
    private bool wasTriggeredOnce;

    //Le dialogue manager qui permet d'afficher le dialogue
    protected DialogueManager dialogueManager;

    //Si la quete a été complétée
    public bool questIsCompleted = false;

    //L'objet a ramener pour compléter la quête
    [SerializeField] private Transform questObjectNeeded;

    //Le dialogue de récompense de quête
    [SerializeField] private DialogueText[] questRewardDialogue = default;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();  //Récupère la référence du dialogue manager
        wasTriggeredOnce = false;   //Le dialogue n'a pas encore été trigger
    }

    private void OnTriggerStay(Collider other)
    {
        //Si le joueur reste dans la triggerBox, qu'il appuie sur le bouton pour parler et que le dialogue n'est pas un trigger
        if (other.tag == "Player" && Input.GetButtonDown("Talk") && !isTrigger)
        {
            //Si un dialogue n'est PAS en cours et que la quête n'est pas complétée
            if (!dialogueManager.isDialogueOnGoing && !questIsCompleted)
            {
                //Début du dialogue "de base"
                dialogueManager.StartDialogue(dialogue);
            }
            //Sinon si le dialogue n'est pas en cours mais que la quête est complétée
            else if (!dialogueManager.isDialogueOnGoing && questIsCompleted)
            {
                //Début de dialogue de récompense de quête
                dialogueManager.StartDialogue(questRewardDialogue);
            }
            //Sinon si un dialogue est en cours
            else if (dialogueManager.isDialogueOnGoing)
            {
                //On affiche la phrase suivante du dialogue
                dialogueManager.DisplayNextSentence();
            }
        }

        //Si le joueur reste dans la triggerBox et que le dialogue est un trigger qui n'a pas encore été déclenché
        if (other.tag == "Player" && isTrigger && !wasTriggeredOnce)
        {
            //Si le dialogue n'est pas en cours
            if (!dialogueManager.isDialogueOnGoing)
            {
                //On lance le dialogue
                dialogueManager.StartDialogue(dialogue);
            }
            //Sinon si on appuie sur le bouton pour parler
            else if (Input.GetButtonDown("Talk"))
            {
                //Si toutes les phrases de tout les dialogues sont passées
                if (dialogueManager.dialogues.Count == 0 && dialogueManager.sentences.Count == 0)
                {
                    //Le trigger de dialogue ne se relancera pas
                    wasTriggeredOnce = true;
                }

                //On affiche la phrase suivante
                dialogueManager.DisplayNextSentence();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Si le joueur entre dans la triggerBox et que le dialogue n'est pas un trigger
        if (other.tag == "Player" && !isTrigger)
        {
            //On affiche le texte au dessus du pnj
            gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
        }

        //Si l'objet demandé par la quête entre dans la triggerBox
        if (questObjectNeeded != null)
        {
            if (other.transform.name == questObjectNeeded.transform.name)
            {
                Debug.Log("Quest");
                questIsCompleted = true;    //La quête est complétée
                Destroy(other);             //On détruit l'objet demandé
                questObjectNeeded = null;   //On enlève la référence de l'objet
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //si le joueur sort de la triggerBox
        if (other.tag == "Player" && !isTrigger)
        {
            //On enlève l'affichage du texte au dessus
            gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);
        }
    }
}
