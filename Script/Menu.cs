using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject home;
    public GameObject informasi;
    public GameObject Pengembang;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        home.SetActive(true);
        informasi.SetActive(false);
        Pengembang.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SoundButton()
    {
        if(GameObject.Find("SistemSound") == true)
        {
            SistemSound.instance.SoundButton();
        }
    }
    public void StartButton(string scenename)
    {
        SoundButton();
        SceneManager.LoadScene(scenename);
    }

    public void InfoButton()
    {
        SoundButton();
        home.SetActive(false);
        informasi.SetActive(true);
    }
    public void DevButton()
    {
        SoundButton();
        home.SetActive(false);
        Pengembang.SetActive(true);
    }
    public void ExitButton()
    {
        SoundButton();
        home.SetActive(true);
        informasi.SetActive(false);
        Pengembang.SetActive(false);

    }

    public void QuitButton()
    {
        SoundButton();
        Application.Quit();
        Debug.Log("Aplikasi Sudah Dikeluarkan...");
    }
    
}
