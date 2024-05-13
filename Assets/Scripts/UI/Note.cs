using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Note : Interactive
{
    [TextArea(3, 10)]
    public string Text;

    [SerializeField] private GameObject _noteUI;
    [SerializeField] private TextMeshProUGUI _noteText;

    private PlayerMovement _playerMovement;
    private PlayerShooting _playerShooting;

    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerShooting = FindObjectOfType<PlayerShooting>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            CloseNoteInterface();
        }
    }

    public override void Interact()
    {
        base.Interact();
        ShowNoteInterface(Text);
        PausePlayer();
    }

    void ShowNoteInterface(string text)
    {
        _noteUI.SetActive(true);
        _noteText.text = text;
    }

    void CloseNoteInterface()
    {
        _noteUI.SetActive(false);
        ResumePlayer();
    }

    void PausePlayer()
    {
        _playerMovement.enabled = false;
        _playerShooting.enabled = false;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    void ResumePlayer()
    {
        _playerMovement.enabled = true;
        _playerShooting.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
