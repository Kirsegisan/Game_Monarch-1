using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text[] optionTexts;
    [SerializeField] private Button[] optionButtons; // Добавлен массив кнопок

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject dialogueUI;

    private Dialogue currentDialogue;
    private DialogueNode currentNode;

    private PlayerMovement movement;
    private PlayerShooting shooting;

    private void Start()
    {
        movement = player.GetComponent<PlayerMovement>();
        shooting = player.GetComponent<PlayerShooting>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentNode = dialogue.nodes[0];
        DisplayCurrentNode();
        OnDialogueStart();
    }

    public void SelectOption(int optionIndex)
    {
        if (currentNode == null || optionIndex < 0 || optionIndex >= currentNode.options.Length)
            return;

        DialogueOption selectedOption = currentNode.options[optionIndex];
        currentNode = currentDialogue.nodes[selectedOption.nextNodeIndex];
        DisplayCurrentNode();

        // Check for special tags in the selected option text
        if (selectedOption.optionText.Contains("[EXIT]"))
            EndDialogue();
        if (selectedOption.optionText.Contains("[KTULHU]"))
            KtulhuComing();
    }

    private void DisplayCurrentNode()
    {
        if (currentNode == null)
            return;

        dialogueText.text = currentNode.dialogueText;

        for (int i = 0; i < optionTexts.Length; i++)
        {
            if (i < currentNode.options.Length)
            {
                optionTexts[i].gameObject.SetActive(true);
                // Replace special tags with an empty string when displaying option text
                string optionText = currentNode.options[i].optionText.Replace("[EXIT]", "").Replace("[KTULHU]", "");
                optionTexts[i].text = optionText;
                optionButtons[i].gameObject.SetActive(true);
            }
            else
            {
                optionTexts[i].gameObject.SetActive(false);
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }


    private void EndDialogue()
    {
        OnDialogueEnd();
        Debug.Log("Dialogue Ended");
    }

    private void OnDialogueStart()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        dialogueUI.SetActive(true);
        TogglePlayerControl(false);
    }

    private void OnDialogueEnd()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dialogueUI.SetActive(false);
        TogglePlayerControl(true);
    }

    private void TogglePlayerControl(bool enable)
    {
        movement.enabled = enable;
        shooting.enabled = enable;
    }

    private void KtulhuComing()
    {

    }
}
