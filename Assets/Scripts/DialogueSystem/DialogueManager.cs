using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public TMP_Text[] optionTexts; // »спользуем массив дл€ кнопок вариантов ответа

    private DialogueNode currentNode;
    private Dialogue currentDialogue;

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("started");
        currentDialogue = dialogue;
        currentNode = dialogue.nodes[0];
        DisplayCurrentNode();
    }

    public void SelectOption(int optionIndex)
    {
        if (optionIndex >= 0 && optionIndex < currentNode.options.Length)
        {
            currentNode = currentDialogue.nodes[currentNode.options[optionIndex].nextNodeIndex];
            DisplayCurrentNode();
        }
    }

    private void DisplayCurrentNode()
    {
        dialogueText.text = currentNode.dialogueText;
        for (int i = 0; i < optionTexts.Length; i++)
        {
            if (i < currentNode.options.Length)
            {
                optionTexts[i].gameObject.SetActive(true);
                optionTexts[i].text = currentNode.options[i].optionText;
            }
            else
            {
                optionTexts[i].gameObject.SetActive(false);
            }
        }
    }
}


