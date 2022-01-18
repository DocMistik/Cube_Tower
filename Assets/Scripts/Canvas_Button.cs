using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement; //Подключаем херню для сцен <2>
//Шаги будут заключены в <>
public class Canvas_Button : MonoBehaviour
{
    public Sprite musicON, musicOFF;

    private void Start()
    {
        if (PlayerPrefs.GetString("music") == "OFF" && gameObject.name == "Music")
            GetComponent<Image>().sprite = musicOFF;
    }

    public void RestartGame() //Создаем функцию, чтобы рестартить игру <1>
    {
        if (PlayerPrefs.GetString("music") != "OFF")
            GetComponent<AudioSource>().Play();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //<3> Я зашел в Unity->file-> build Settings-> Add open scenes
        //Также я здесь добавил активную сцену сюда
        //Переходим в Explosion_of_cubes

    }
    public void LoadInstagramm()
    {
        if (PlayerPrefs.GetString("music") != "OFF")
            GetComponent<AudioSource>().Play();

        Application.OpenURL("https://rt.pornhub.com/");
    }
    public void LoadShop() 
    {
        if (PlayerPrefs.GetString("music") != "OFF")
            GetComponent<AudioSource>().Play();

        SceneManager.LoadScene("Shop");
        
    }
    public void CloseShop()
    {
        if (PlayerPrefs.GetString("music") != "OFF")
            GetComponent<AudioSource>().Play();

        SceneManager.LoadScene("Main");

    }

    public void MusicWork()
    {
        if (PlayerPrefs.GetString("music") == "OFF") //Музыка выключена.Надо включить
        {
            GetComponent<AudioSource>().Play();
            PlayerPrefs.SetString("music", "ON");
            GetComponent<Image>().sprite = musicON;
        }
        else
        { 
            PlayerPrefs.SetString("music", "OFF");
            GetComponent<Image>().sprite = musicOFF;
        }
    }
}
