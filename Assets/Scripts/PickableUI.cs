using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickableUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectNameText;
    [SerializeField] private GameObject pickableUI;
    [SerializeField] private Camera cam;

    public float pickupDistance = 3f;

    private void Start()
    {
        pickableUI.SetActive(false);
    }

    private void Update()
    {
        // Создаем луч из центра экрана курсора
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance) && hit.collider.CompareTag("Pickable"))
        {
            //Debug.Log(hit.transform.name);
            Pickable pickableObject = hit.collider.GetComponent<Pickable>();           

            if (pickableObject)
            {
                pickableUI.SetActive(true);
                objectNameText.text = pickableObject.objectName;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Вызываем метод взаимодействия с объектом
                    pickableObject.Interact();
                }
            }

        }
        else
        {
            // Скрываем UI, если луч не попал на объект с тегом "Pickable"
            pickableUI.SetActive(false);
        }
    }
}
