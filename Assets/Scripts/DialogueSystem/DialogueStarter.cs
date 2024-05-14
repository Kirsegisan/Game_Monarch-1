using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : Interactive
{
    //q
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private DialogueManager dialogueManager;

    public override void Interact()
    {
        base.Interact();
        dialogueManager.StartDialogue(dialogue);
    }
}
