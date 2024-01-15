using System.Threading;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float crouchSpeed = 2f;
    public float jumpForce = 8f;
    public float crouchHeight = 0.5f;
    public float playerHeight = 1f;
    public float sensitivity = 2f;
    public float minimumY = -20f;
    public float maximumY = 20f;
    public float cameraRotationK = 5f;
    public float fireRate = 1f;
    public int jumps = 2;
    public GameObject bulletPrefab; // ������ ����
    public Transform spawnPoint; // �����, ������ ����� ����������� ����
    public Transform cameraRotationPoint;
    public Transform gunTransform;

    private bool isGrounded;
    private bool isCrouching;
    private bool isShooting;
    private Rigidbody playerRigidbody;
    private float rotationY = 0;
    private float turn = 0;
    private float jumpCount = 0;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 4f);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (!isCrouching)
        {
            MovePlayer(moveDirection, walkSpeed);
        }
        else
        {
            MovePlayer(moveDirection, crouchSpeed);
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartShooting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopShooting();
        }

        if (isGrounded && Input.GetButtonDown("Jump") && jumpCount < jumps)
        {
            Jump();
            jumpCount++;
        }
        else if (isGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StandUp();
        }

        lookMove();
    }

    private void StartShooting()
    {
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootCoroutine());
        }
    }

    private void StopShooting()
    {
        if (isShooting)
        {
            isShooting = false;
            StopCoroutine(ShootCoroutine());
        }
    }

    private IEnumerator ShootCoroutine()
    {
        while (isShooting)
        {
            Shoot();

            // ���� ��������������� �������� ������� ����� ��������� ���������
            yield return new WaitForSeconds(1f / fireRate);
        }
    }


    void MovePlayer(Vector3 moveDirection, float speed)
    {
        // ��������� ������ �� ��������� ��������� � ����������
        Vector3 worldMoveDirection = transform.TransformDirection(moveDirection);
        Vector3 velocity = worldMoveDirection * speed;
        playerRigidbody.velocity = new Vector3(velocity.x, playerRigidbody.velocity.y, velocity.z);
    }

    void Jump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Crouch()
    {
        isCrouching = true;
    }

    void StandUp()
    {
        isCrouching = false;
    }

    void Shoot()
    {
        // ������� ����� ��������� ���� �� �������
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

        // �������� ����������� ������� ������
        Vector3 lookDirection = Camera.main.transform.forward;

        // ������������ ���� � ����������� ������� ������
        bullet.transform.forward = lookDirection;
    }

    void lookMove()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        float cameraRotationY = Camera.main.transform.eulerAngles.x;

        // ������������ ������ �� ����������� (������ ��� Y)
        transform.Rotate(Vector3.up * mouseX * sensitivity * 10f);

        // ������������ ������ �� ��������� (������ ��� X)
        rotationY = mouseY * sensitivity;
        if (minimumY < turn - rotationY & maximumY > turn - rotationY)
        {
            Vector3 pivotPosition = cameraRotationPoint != null ? cameraRotationPoint.position : transform.position;

            Camera.main.transform.RotateAround(pivotPosition, transform.right, rotationY * cameraRotationK);
            gunTransform.eulerAngles = new Vector3(cameraRotationY, gunTransform.eulerAngles.y, gunTransform.eulerAngles.z);

            turn = turn - rotationY;
        }

    }
}
