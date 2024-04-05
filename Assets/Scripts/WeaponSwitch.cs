using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject parentObject;
    private int currentIndex = 0;

    void Start()
    {
        SetActiveChild(currentIndex);
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Если колесо мыши крутится вверх
        if (scroll > 0f)
        {
            SwitchToNextChild();
        }
        // Если колесо мыши крутится вниз
        else if (scroll < 0f)
        {
            SwitchToPreviousChild();
        }
    }

    void SwitchToNextChild()
    {
        parentObject.transform.GetChild(currentIndex).gameObject.SetActive(false);

        currentIndex++;

        // Проверяем, не вышли ли за пределы дочерних объектов
        if (currentIndex >= parentObject.transform.childCount)
        {
            currentIndex = 0; // Если вышли, начинаем с начала
        }

        // Включаем следующий дочерний объект
        SetActiveChild(currentIndex);
    }

    void SwitchToPreviousChild()
    {
        // Выключаем текущий активный дочерний объект
        parentObject.transform.GetChild(currentIndex).gameObject.SetActive(false);

        currentIndex--;

        // Проверяем, не вышли ли за пределы дочерних объектов
        if (currentIndex < 0)
        {
            currentIndex = parentObject.transform.childCount - 1; // Если вышли, начинаем с конца
        }

        // Включаем предыдущий дочерний объект
        SetActiveChild(currentIndex);
    }

    void SetActiveChild(int index)
    {
        // Включаем дочерний объект с указанным индексом
        parentObject.transform.GetChild(index).gameObject.SetActive(true);
    }
}
