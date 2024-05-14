using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    [TextArea(3, 10)]
    public string dialogueText;
    public DialogueOption[] options;
    public int nextNodeIndex;
}


