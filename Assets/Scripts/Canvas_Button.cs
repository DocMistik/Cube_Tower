using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement; //���������� ����� ��� ���� <2>
//���� ����� ��������� � <>
public class Canvas_Button : MonoBehaviour
{
    public Sprite musicON, musicOFF;

    private void Start()
    {
        if (PlayerPrefs.GetString("music") == "OFF" && gameObject.name == "Music")
            GetComponent<Image>().sprite = musicOFF;
    }

    public void RestartGame() //������� �������, ����� ���������� ���� <1>
    {
        if (PlayerPrefs.GetString("music") != "OFF")
            GetComponent<AudioSource>().Play();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //<3> � ����� � Unity->file-> build Settings-> Add open scenes
        //����� � ����� ������� �������� ����� ����
        //��������� � Explosion_of_cubes

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
        if (PlayerPrefs.GetString("music") == "OFF") //������ ���������.���� ��������
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
