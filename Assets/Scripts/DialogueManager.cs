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

    [Tooltip("L'animation de la boite de dialogue")]
    [SerializeField]
    private Animator animator;

    public bool isDialogueOnGoing = false;  //Si un dialogue est en cours

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    //Charge le dialogue dans la queue et bloque les inputs de déplacement
    public void StartDialogue(DialogueText dialogue)
    {
        Debug.Log("Start Dialogue");

        FindObjectOfType<PlayerMovement>().canMove = false; //Bloque les déplacement

        nameText.text = dialogue.name;  //Prend le nom de la personne qui parle

        sentences.Clear();  //Vide la queue 

        //Pour chaque phrase dans le dialogue
        foreach (string sentence in dialogue.sentences)
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
        Debug.Log("Next sentence");

        //Si la queue est vide
        if (sentences.Count == 0)
        {
            EndDialogue();  //Fin du dialogue
            return;
        }

        string sentence = sentences.Dequeue();  //On met la phrase suivante dans un string
        StopAllCoroutines();    //Stop toutes les coroutines en cours
        StartCoroutine(TypeSentence(sentence)); //Coroutine d'affichage du texte
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
        FindObjectOfType<PlayerMovement>().canMove = true;  //On remet les déplacements
        isDialogueOnGoing = false;                          //Le dialogue n'est plus en cours
        animator.SetBool("isOpen", false);                  //On ferme la boite de dialogue
    }  
}
