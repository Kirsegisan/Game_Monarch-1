using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickableUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectNameText;
    [SerializeField] private GameObject pickableUI;
    [SerializeField] private Camera cam;

    // Расстояние, на котором срабатывает подбор
    public float pickupDistance = 3f;

    private void Start()
    {
        // По умолчанию скрываем UI
        pickableUI.SetActive(false);
    }

    private void Update()
    {
        // Создаем луч из центра экрана курсора
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Проверяем, попал ли луч на объект с тегом "Pickable"
        if (Physics.Raycast(ray, out hit, pickupDistance) && hit.collider.CompareTag("Pickable"))
        {
            Debug.Log(hit.transform.name);
            Pickable pickableObject = hit.collider.GetComponent<Pickable>();           

            if (pickableObject)
            {
                // Показываем UI и отображаем имя подбираемого объекта
                pickableUI.SetActive(true);
                objectNameText.text = pickableObject.objectName;

                // Проверяем нажатие клавиши "E"
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
