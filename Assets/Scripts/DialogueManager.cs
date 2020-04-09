using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI sentenceText;

    private Queue<string> sentences;

    [SerializeField]
    private Animator animator;

    public bool isDialogueOnGoing = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(DialogueText dialogue)
    {
        FindObjectOfType<PlayerMovement>().enabled = false;

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        animator.SetBool("isOpen", true);

        isDialogueOnGoing = true;
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        sentenceText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            sentenceText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        FindObjectOfType<PlayerMovement>().enabled = true;
        isDialogueOnGoing = false;
        animator.SetBool("isOpen", false);
    }
}
