using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _parentObject;
    [SerializeField] private PlayerShooting _playerShooting;

    private int _currentIndex = 0;


    void Start()
    {
        SetActiveChild(_currentIndex);
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
        _parentObject.transform.GetChild(_currentIndex).gameObject.SetActive(false);

        _currentIndex++;

        // Проверяем, не вышли ли за пределы дочерних объектов
        if (_currentIndex >= _parentObject.transform.childCount)
        {
            _currentIndex = 0; // Если вышли, начинаем с начала
        }

        // Включаем следующий дочерний объект
        SetActiveChild(_currentIndex);
    }

    void SwitchToPreviousChild()
    {
        // Выключаем текущий активный дочерний объект
        _parentObject.transform.GetChild(_currentIndex).gameObject.SetActive(false);

        _currentIndex--;

        // Проверяем, не вышли ли за пределы дочерних объектов
        if (_currentIndex < 0)
        {
            _currentIndex = _parentObject.transform.childCount - 1; // Если вышли, начинаем с конца
        }

        // Включаем предыдущий дочерний объект
        SetActiveChild(_currentIndex);
    }

    void SetActiveChild(int index)
    {
        // Включаем дочерний объект с указанным индексом
        _parentObject.transform.GetChild(index).gameObject.SetActive(true);
        _playerShooting.shooter = _parentObject.transform.GetChild(index).gameObject.GetComponent<Shooter>();
    }
}
