using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    private Transform camTransform; //��� 1. ������� ���������� ��� ������������� ������
    private float shakeDur = 1f, shakeAmount = 0.07f, decreaseFactor = 1.5f; //��� 2. ������������ ��� ������ ����� �������� ���� ������
        //shakeDur=����� �� ������� ��� ����� ��������
        //shakeAmount = ��� ������ ��� ����� ��������
        //decreaseFactor = ��������� ������ ��������� ������ ������
    private Vector3 orginPos; //��� 3. �������������� ������� ������

    private void Start()// ��� 4. ������� �����
    {
        camTransform = GetComponent<Transform>();
        orginPos = camTransform.localPosition;
    }
    private void Update() // ��� 5. �������  Update
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
