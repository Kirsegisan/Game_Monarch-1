using UnityEngine;

public class Recoil : MonoBehaviour
{
    // Параметры отдачи
    public float recoilForce = 0.5f; // Сила отдачи
    public float recoilSpeed = 2f;   // Скорость возвращения

    // Внутренние переменные
    private Vector3 originalPosition;
    private bool isRecoiling = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Если идет процесс отдачи, возвращаем оружие к исходному положению
        if (isRecoiling)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * recoilSpeed);

            // Если оружие вернулось к исходному положению, завершаем отдачу
            if (Vector3.Distance(transform.localPosition, originalPosition) < 0.01f)
            {
                isRecoiling = false;
            }
        }
    }

    // Метод для вызова отдачи из других скриптов или событий
    public void ApplyRecoil()
    {
        // Применяем отдачу только если она не активна
        if (!isRecoiling)
        {
            transform.localPosition -= Vector3.forward * recoilForce;
            isRecoiling = true;
        }
    }
}
