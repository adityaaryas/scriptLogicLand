using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SistemSound : MonoBehaviour
{
    public static SistemSound instance { get; private set; }

    public AudioSource audioDefault;
    public bool[] isMusicStop;
    public bool isStop;

    // Properti untuk AudioSource dan AudioClip
    public AudioSource audioSourceSoundEffect;
    public AudioClip audioClipButton;
    public AudioSource music;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Pastikan GameObject tidak dihancurkan
        }
        else
        {
            Destroy(gameObject); // Menghancurkan duplikat
        }

        // Cek dan ambil AudioSource jika belum diatur
        if (audioSourceSoundEffect == null)
        {
            audioSourceSoundEffect = GetComponent<AudioSource>();
        }
    }

    // Fungsi untuk memutar suara tombol
    public void SoundButton()
    {
        if (audioSourceSoundEffect != null && audioClipButton != null)
        {
            audioSourceSoundEffect.PlayOneShot(audioClipButton);
        }
        else
        {
            Debug.LogWarning("AudioSource atau AudioClip belum diatur!");
        }
    }
    public void MuteSound()
    {
        if (music.mute == false)
        {
            music.mute = true;
        }
        else
        {
            music.mute = false;
        }
    }
    void update()
    {
        if (isStop == false)
        {

        }
        else//true
        {

        }
    }
    void PauseMusic()
    {
        for(int i = 0; i < isMusicStop.Length; i++)
        {
            if (isMusicStop[i] == true && SceneManager.GetActiveScene().buildIndex == i)
            {
                audioDefault.Pause();

                isStop = true;

                Debug.Log("pause musik");

                break;
            }
        }
    }

    void ContinueMusik()
    {
        for(int i = 0; i < isMusicStop.Length; i++)
        {
            if(isMusicStop[i] == false && SceneManager.GetActiveScene().buildIndex == i)
            {
                audioDefault.Play();
                isStop = false;

                Debug.Log("continue musik");

                break;
            }
        }
    }
}
