using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : Interactive
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private SpecialConditions conditions;
    [SerializeField] private GameObject toDelete;
    [SerializeField] private GameObject toGive;

    public override void Interact()
    {
        base.Interact();
        dialogueManager.StartDialogue(dialogue);
        dialogueManager.GiveParams(toDelete, toGive);
    }
}
