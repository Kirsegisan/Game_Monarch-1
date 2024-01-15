using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        // Выключаем гравитацию при старте
        rb.useGravity = true;
        // Запускаем таймер для удаления пули
        Invoke("DestroyBullet", lifeTime);
    }

    void Update()
    {
        // Если гравитация включена, применяем ее
        if (rb.useGravity)
        {
            rb.AddForce(Vector3.down * 9.8f, ForceMode.Acceleration);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Обработка столкновения с другими объектами

        // Уничтожаем пулю
        DestroyBullet();
    }

    void DestroyBullet()
    {
        // Уничтожаем пулю
        Destroy(gameObject);
    }
}
