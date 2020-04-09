using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueText : ScriptableObject
{
    public new string name;

    [TextArea(3, 10)]
    public string[] sentences;

    public bool isRepeatable;
}
