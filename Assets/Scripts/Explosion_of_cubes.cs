using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_of_cubes : MonoBehaviour
{
    public GameObject restartButton, explosion; //<4> ������� ���������� ��� ������ ������

    private bool _collisionSet = true;
    private void OnCollisionEnter(Collision collision) //��� �������� ���������������
    {        

        if(collision.gameObject.tag == "Cube" && _collisionSet) 
        {
            for (int i = collision.gameObject.transform.childCount - 1; i >= 0; i--) //������� ���. ��������� (childCount) ����� ���� ��-�� �������
            {
                Transform child = collision.transform.GetChild(i); //���������� ���� ������� �� �������
                child.gameObject.AddComponent<Rigidbody>(); //���� ��������
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(70f, Vector3.up, 5f);
                //�������� ��������, ������������� ���� ������ 70(?)f, �� ���������� Y, � �������� 5(?)f (�������� ����� ���� ��������)
                child.SetParent(null);//����� �������� ������� �� �������� ���� �� �����
            }
            restartButton.SetActive(true); //<5> ��������� ������
            Camera.main.transform.position -= new Vector3(0, 0, 3f); //<6> ��� ��������� �������� ������ �� 3 �������

            Camera.main.gameObject.AddComponent<Camera_Shake>();// ��� 6. ��������� ������ ������

            GameObject newVfx = Instantiate(explosion, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, collision.contacts[0].point.z), Quaternion.identity) as GameObject;
            Destroy(newVfx, 2.5f);
            if (PlayerPrefs.GetString("music") != "OFF")
                GetComponent<AudioSource>().Play();

            Destroy(collision.gameObject); //���������� ����� ����� ������
            _collisionSet = false; //��� �����, ������������
        }
    }
}
