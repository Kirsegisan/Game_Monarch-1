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
        // ��������� ���������� ��� ������
        rb.useGravity = true;
        // ��������� ������ ��� �������� ����
        Invoke("DestroyBullet", lifeTime);
    }

    void Update()
    {
        // ���� ���������� ��������, ��������� ��
        if (rb.useGravity)
        {
            rb.AddForce(Vector3.down * 9.8f, ForceMode.Acceleration);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ��������� ������������ � ������� ���������

        // ���������� ����
        DestroyBullet();
    }

    void DestroyBullet()
    {
        // ���������� ����
        Destroy(gameObject);
    }
}
