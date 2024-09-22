using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float maxSpeed = 10.0f; // ����������� ��������
    public float acceleration = 2.0f; // �����������
    public float rotationSpeed = 200.0f; // �������� ���������
    public float driftAmplitude = 0.1f; // �������� ������
    public float driftFrequency = 0.5f; // ������� ������
    public float rotationAmplitude = 0.5f; // �������� ���������
    public float rotationFrequency = 0.5f; // ������� ���������

    private Rigidbody2D rb;
    private float currentSpeed = 0f; // ������� ��������
    private float driftTime;
    private float rotationTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning("Rigidbody2D �� ��������!");
        }

        driftTime = Random.Range(0f, 2f * Mathf.PI); // ���������� ������� ��������
        rotationTime = Random.Range(0f, 2f * Mathf.PI); // ���������� ������� ���������
    }

    void FixedUpdate()
    {
        // ��������� ���������� ��������� �� ����
        HandleRotation();

        // ��� ������ � ������� ��������
        if (Input.GetKey(KeyCode.W))
        {
            Accelerate();
        }
        else
        {
            Decelerate();
            // ������ ���������, ���� ���� ����
            SmoothDrift();
        }

        // ���������� ������� �������� �� ������� ��������
        Vector2 moveDirection = transform.up * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection);
    }

    void HandleRotation()
    {
        float rotationInput = Input.GetAxis("Horizontal"); // ������������� ��� "Horizontal" ��� ��������� ����������

        // ����������� ��������� ����� ���� � �������� �� �����������
        if (Mathf.Abs(rotationInput) > 0.01f)
        {
            transform.Rotate(Vector3.forward, -rotationInput * rotationSpeed * Time.deltaTime);
        }
    }

    void Accelerate()
    {
        // ������� ���� �������� �� �����������
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed; // �������� �������� �� ��������
            }
        }
    }

    void Decelerate()
    {
        // ������ ��������� �������� ��� ��������� ������ W
        if (currentSpeed > 0)
        {
            currentSpeed -= 2 * acceleration * Time.deltaTime;
            if (currentSpeed < 0)
            {
                currentSpeed = 0; // ����� ������� �������
            }
        }
    }

    void SmoothDrift()
    {
        // ³�������� ������� ����� �� ���������, ���� �������� �������� ������
        if (currentSpeed > 0) return;

        // ��������� ���� ��� �������������� ����
        driftTime += driftFrequency * Time.deltaTime;
        rotationTime += rotationFrequency * Time.deltaTime;

        // ������� ����� � ����������� �������� � ������������� ��������
        Vector2 driftDirection = new Vector2(
            Mathf.Sin(driftTime) * driftAmplitude,
            Mathf.Cos(driftTime) * driftAmplitude
        );

        rb.MovePosition(rb.position + driftDirection * Time.deltaTime);

        // ������ ��������� ������� �� � ������������� ��������
        float smoothRotation = Mathf.Sin(rotationTime) * rotationAmplitude;
        transform.Rotate(Vector3.forward, smoothRotation * Time.deltaTime);
    }
}
