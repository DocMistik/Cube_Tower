using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Если ты видишь этот скрипт то ты скорее всего Рома или Юля, которая решила резко залезть в скрипты Ромы
//Или грабитель, который украл ноутбук Ромы(лучше отдай его, а то я никогда в жизни не оптимизирую этот ебучий код)
//Еще в Unity я скачал плагин на пост обработку света, чтобы его скачать надо 
//Зайти в Unity-> Windows-> Packege maneger-> Post Processing (Если не находится там сверху надо поменять с Packeges: In project на Packeges: Unity Registry)  
public class CameraMove : MonoBehaviour
{
    public float speed = 5f; //Создаем переменную скорости
    private Transform _rotator; //Cоздаем переменную для вращения камеры, обращаясь к Transform в Unity

    private void Start()
    {
        _rotator = GetComponent<Transform>(); // Берем компонент Transform и присваиваем значение
    }

    private void Update()
    {
        _rotator.Rotate(0, speed * Time.deltaTime, 0); //Ставим скорость
    }
}
