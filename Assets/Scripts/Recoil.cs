using UnityEngine;

public class Recoil : MonoBehaviour
{
    // ��������� ������
    public float recoilForce = 0.5f; // ���� ������
    public float recoilSpeed = 2f;   // �������� �����������

    // ���������� ����������
    private Vector3 originalPosition;
    private bool isRecoiling = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // ���� ���� ������� ������, ���������� ������ � ��������� ���������
        if (isRecoiling)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * recoilSpeed);

            // ���� ������ ��������� � ��������� ���������, ��������� ������
            if (Vector3.Distance(transform.localPosition, originalPosition) < 0.01f)
            {
                isRecoiling = false;
            }
        }
    }

    // ����� ��� ������ ������ �� ������ �������� ��� �������
    public void ApplyRecoil()
    {
        // ��������� ������ ������ ���� ��� �� �������
        if (!isRecoiling)
        {
            transform.localPosition -= Vector3.forward * recoilForce;
            isRecoiling = true;
        }
    }
}
