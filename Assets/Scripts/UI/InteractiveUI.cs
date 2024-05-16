using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractiveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectNameText;
    [SerializeField] private GameObject interactiveUI;
    [SerializeField] private Camera cam;

    public float pickupDistance = 3f;

    private void Start()
    {
        interactiveUI.SetActive(false);
    }

    private void Update()
    {
        // Создаем луч из центра экрана курсора
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance) && hit.collider.CompareTag("Interactive"))
        {
            //Debug.Log(hit.transform.name);
            Interactive interactiveObject = hit.collider.GetComponent<Interactive>();

            if (interactiveObject)
            {
                interactiveUI.SetActive(true);
                if (interactiveObject.interactionButton != "")
                {
                    objectNameText.text = interactiveObject.interactionButton + "" + interactiveObject.objectName;
                }
                else
                {
                    objectNameText.text = interactiveObject.objectName;
                }


                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Вызываем метод взаимодействия с объектом
                    interactiveObject.Interact();
                }
            }

        }
        else
        {
            // Скрываем UI, если луч не попал на объект с тегом "Interactive"
            interactiveUI.SetActive(false);
        }
    }
}
