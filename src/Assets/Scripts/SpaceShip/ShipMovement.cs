using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float maxSpeed = 10.0f; // Максимальна швидкість
    public float acceleration = 2.0f; // Прискорення
    public float rotationSpeed = 200.0f; // Швидкість обертання
    public float driftAmplitude = 0.1f; // Амплітуда дрейфу
    public float driftFrequency = 0.5f; // Частота дрейфу
    public float rotationAmplitude = 0.5f; // Амплітуда обертання
    public float rotationFrequency = 0.5f; // Частота обертання

    private Rigidbody2D rb;
    private float currentSpeed = 0f; // Поточна швидкість
    private float driftTime;
    private float rotationTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning("Rigidbody2D не знайдено!");
        }

        driftTime = Random.Range(0f, 2f * Mathf.PI); // Випадковий початок коливань
        rotationTime = Random.Range(0f, 2f * Mathf.PI); // Випадковий початок обертання
    }

    void FixedUpdate()
    {
        // Управління обертанням незалежно від руху
        HandleRotation();

        // Рух вперед з набором швидкості
        if (Input.GetKey(KeyCode.W))
        {
            Accelerate();
        }
        else
        {
            Decelerate();
            // Плавне коливання, якщо немає руху
            SmoothDrift();
        }

        // Переміщення корабля відповідно до поточної швидкості
        Vector2 moveDirection = transform.up * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection);
    }

    void HandleRotation()
    {
        float rotationInput = Input.GetAxis("Horizontal"); // Використовуємо вісь "Horizontal" для керування обертанням

        // Застосовуємо обертання тільки якщо є введення від користувача
        if (Mathf.Abs(rotationInput) > 0.01f)
        {
            transform.Rotate(Vector3.forward, -rotationInput * rotationSpeed * Time.deltaTime);
        }
    }

    void Accelerate()
    {
        // Плавний набір швидкості до максимальної
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed; // Обмежуємо швидкість на максимумі
            }
        }
    }

    void Decelerate()
    {
        // Плавне зменшення швидкості при відпусканні клавіші W
        if (currentSpeed > 0)
        {
            currentSpeed -= 2 * acceleration * Time.deltaTime;
            if (currentSpeed < 0)
            {
                currentSpeed = 0; // Повна зупинка корабля
            }
        }
    }

    void SmoothDrift()
    {
        // Відключаємо плавний дрейф та обертання, якщо корабель рухається вперед
        if (currentSpeed > 0) return;

        // Оновлення часу для синусоїдального руху
        driftTime += driftFrequency * Time.deltaTime;
        rotationTime += rotationFrequency * Time.deltaTime;

        // Плавний дрейф у випадковому напрямку з використанням синусоїди
        Vector2 driftDirection = new Vector2(
            Mathf.Sin(driftTime) * driftAmplitude,
            Mathf.Cos(driftTime) * driftAmplitude
        );

        rb.MovePosition(rb.position + driftDirection * Time.deltaTime);

        // Плавне обертання навколо осі з використанням синусоїди
        float smoothRotation = Mathf.Sin(rotationTime) * rotationAmplitude;
        transform.Rotate(Vector3.forward, smoothRotation * Time.deltaTime);
    }
}
