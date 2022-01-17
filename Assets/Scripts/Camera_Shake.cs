using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    private Transform camTransform; //Шаг 1. Создаем переменную для трансформации камеры
    private float shakeDur = 1f, shakeAmount = 0.07f, decreaseFactor = 1.5f; //Шаг 2. Контролируем как сильно будет трястись наша камера
        //shakeDur=Время за которое оно будет трястись
        //shakeAmount = Как сильно она будет трястись
        //decreaseFactor = Насколько быстро остановим тряску камеры
    private Vector3 orginPos; //Шаг 3. Первоначальная позиция камеры

    private void Start()// Шаг 4. Создаем старт
    {
        camTransform = GetComponent<Transform>();
        orginPos = camTransform.localPosition;
    }
    private void Update() // Шаг 5. Создаем  Update
    {
        if (shakeDur > 0)
        {
            camTransform.localPosition = orginPos + Random.insideUnitSphere * shakeAmount;
            shakeDur -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDur = 0;
            camTransform.localPosition = orginPos;
        }
    }   
}
