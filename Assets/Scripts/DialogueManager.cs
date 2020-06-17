using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Tooltip("Le textRenderer qui affiche le nom de la personne qui parle")]
    [SerializeField]
    private TextMeshProUGUI nameText;

    [Tooltip("Le textRenderer qui affiche ce que dit la personne qui parle")]
    [SerializeField]    
    private TextMeshProUGUI sentenceText; 

    public Queue<string> sentences;    //Les phrases à afficher
    public Queue<DialogueText> dialogues;

    [Tooltip("L'animation de la boite de dialogue")]
    [SerializeField]
    private Animator animator;

    public bool isDialogueOnGoing = false;  //Si un dialogue est en cours

    private int dialogueProgress; //L'indix du dialogue qu'on lit

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogues = new Queue<DialogueText>();
    }

    //Charge le dialogue dans la queue et bloque les inputs de déplacement
    public void StartDialogue(DialogueText[] dialogue)
    {
        Debug.Log("Start Dialogue");

        FindObjectOfType<PlayerMovement>().canMove = false; //Bloque les déplacement

        dialogues.Clear();  //Vide la queue des dialogues

        //Pour chaque object Dialogue dans la conversation
        foreach (DialogueText text in dialogue)
        {
            dialogues.Enqueue(text);    //Met le dialogue dans la queue
        }

        DialogueText currentDialogue = dialogues.Dequeue(); //On sors le premier dialogue de la queue

        nameText.text = currentDialogue.name;  //L'affichage prend le nom de la personne qui parle

        sentences.Clear();  //Vide la queue des phrases

        //Pour chaque phrase dans le dialogue
        foreach (string sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);    //Met la phrase dans la queue
        }
        
        animator.SetBool("isOpen", true);   //Animation de la boite de dialogue (ouverture)

        isDialogueOnGoing = true;   //Le dialogue est en cours
        
        DisplayNextSentence();  //Affiche la première phrase
    }

    //Affiche la phrase suivante
    public void DisplayNextSentence()
    {
        //Si la queue est vide
        if (sentences.Count == 0)
        {
            //Si c'est le dernier dialogue
            if (dialogues.Count == 0)
            {
                EndDialogue();  //Fin du dialogue
                return;
            }
            else
            {
                LoadNextDialogue(); //Charge le prochain dialogue
                return;
            }
        }

        Debug.Log("Next sentence " + sentences.Count);

        string sentence = sentences.Dequeue();  //On met la phrase suivante dans un string
        StopAllCoroutines();    //Stop toutes les coroutines en cours
        StartCoroutine(TypeSentence(sentence)); //Coroutine d'affichage du texte
    }

    //charge le dialogue suivant
    public void LoadNextDialogue()
    {
        Debug.Log("Next Dialogue " + dialogues.Count);

        DialogueText currentDialogue = dialogues.Dequeue(); //On sors le dialogue suivant de la queue

        nameText.text = currentDialogue.name;  //Prend le nom de la personne qui parle

        sentences.Clear();  //Vide la queue 

        //Pour chaque phrase dans le dialogue
        foreach (string sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);    //Met la phrase dans la queue
        }

        DisplayNextSentence();
    }

    //Coroutine d'affichage du texte
    IEnumerator TypeSentence(string sentence)
    {
        sentenceText.text = "";
        foreach (char letter in sentence.ToCharArray()) //Pour chaque lettre dans le phrase
        {
            sentenceText.text += letter;    //On l'ajoute au textRenderer
            yield return null;
        }
    }

    //Fin du dialogue
    void EndDialogue()
    {
        Debug.Log("End Dialogue");
        FindObjectOfType<PlayerMovement>().canMove = true;  //On remet les déplacements
        isDialogueOnGoing = false;                          //Le dialogue n'est plus en cours
        animator.SetBool("isOpen", false);                  //On ferme la boite de dialogue
    }  
}
