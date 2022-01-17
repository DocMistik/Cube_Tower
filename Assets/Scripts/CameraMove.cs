using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���� �� ������ ���� ������ �� �� ������ ����� ���� ��� ���, ������� ������ ����� ������� � ������� ����
//��� ���������, ������� ����� ������� ����(����� ����� ���, � �� � ������� � ����� �� ����������� ���� ������ ���)
//��� � Unity � ������ ������ �� ���� ��������� �����, ����� ��� ������� ���� 
//����� � Unity-> Windows-> Packege maneger-> Post Processing (���� �� ��������� ��� ������ ���� �������� � Packeges: In project �� Packeges: Unity Registry)  
public class CameraMove : MonoBehaviour
{
    public float speed = 5f; //������� ���������� ��������
    private Transform _rotator; //C������ ���������� ��� �������� ������, ��������� � Transform � Unity

    private void Start()
    {
        _rotator = GetComponent<Transform>(); // ����� ��������� Transform � ����������� ��������
    }

    private void Update()
    {
        _rotator.Rotate(0, speed * Time.deltaTime, 0); //������ ��������
    }
}
