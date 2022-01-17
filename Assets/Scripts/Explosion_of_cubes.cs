using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_of_cubes : MonoBehaviour
{
    public GameObject restartButton, explosion; //<4> Создаем переменную для показа кнопки

    private bool _collisionSet = true;
    private void OnCollisionEnter(Collision collision) //Для проверки соприкосновения
    {        

        if(collision.gameObject.tag == "Cube" && _collisionSet) 
        {
            for (int i = collision.gameObject.transform.childCount - 1; i >= 0; i--) //Подсчет доч. элементов (childCount) минус один из-за индекса
            {
                Transform child = collision.transform.GetChild(i); //Перебираем наши объекты по индексу
                child.gameObject.AddComponent<Rigidbody>(); //Даем компонет
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(70f, Vector3.up, 5f);
                //получаем компонет, устанавливаем силу взрыва 70(?)f, по координате Y, с радиусом 5(?)f (значения могут быть изменены)
                child.SetParent(null);//Чтобы одчерние объекты не зависели друг от друга
            }
            restartButton.SetActive(true); //<5> Проявляем кнопку
            Camera.main.transform.position -= new Vector3(0, 0, 3f); //<6> При проигрыше отдаляем камеру на 3 еденицы

            Camera.main.gameObject.AddComponent<Camera_Shake>();// Шаг 6. Добавляем тряску камеры

            GameObject newVfx = Instantiate(explosion, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, collision.contacts[0].point.z), Quaternion.identity) as GameObject;
            Destroy(newVfx, 2.5f);
            if (PlayerPrefs.GetString("music") != "OFF")
                GetComponent<AudioSource>().Play();

            Destroy(collision.gameObject); //Уничтожаем связь между кубами
            _collisionSet = false; //хуй знает, подстраховка
        }
    }
}
