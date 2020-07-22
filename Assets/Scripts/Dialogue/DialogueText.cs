using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueText : ScriptableObject
{
    [Tooltip("Nom de la personne qui parle")]
    public new string name;

    [Tooltip("Ce que dit la personne qui parle")]
    [TextArea(3, 10)]
    public string[] sentences;
}
