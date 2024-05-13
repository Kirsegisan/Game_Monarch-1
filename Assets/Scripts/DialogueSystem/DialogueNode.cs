using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string dialogueText;
    public DialogueOption[] options;
    public int nextNodeIndex;
}


