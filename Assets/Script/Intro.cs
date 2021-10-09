using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public GameObject textoX;

    void Start()
    {
        if(PlayerPrefs.GetInt("Som") == 0) textoX.SetActive(false);
        else                               textoX.SetActive(true);
    }

    public void Jogar()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MudarSom()
    {
        int som = PlayerPrefs.GetInt("Som");
        if (som == 1)
        {
            textoX.SetActive(true);
            PlayerPrefs.SetInt("Som", 0);
        }
        else
        {
            textoX.SetActive(false);
            PlayerPrefs.SetInt("Som", 1);
        }
    }
}