using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float interactionDistance = 2f;
    public LayerMask interactableLayer;
    public GameObject UI;

    private bool isInRange = false;
    private GameObject currentInteractable;

    void Update()
    {
        CheckForInteractable();
        HandleInteraction();
    }

    void CheckForInteractable()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            currentInteractable = hit.collider.gameObject;
            isInRange = true;
            ShowName(currentInteractable);
        }
        else
        {
            HideName();
            currentInteractable = null;
            isInRange = false;
        }
    }

    void HandleInteraction()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(2);
        }
    }

    void ShowName(GameObject item)
    {
        Text itemNameText = UI.GetComponent<Text>();
        itemNameText.text = item.name;
    }
    void HideName()
    {
        Text itemNameText = UI.GetComponent<Text>();
        itemNameText.text = "";
    }
}
