using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonscript : MonoBehaviour
{
    public Sprite[] spriteMute; // Sprite untuk mute dan unmute
    public List<Button> allMuteButtons; // Daftar semua tombol mute (jika ada lebih dari satu tombol mute)
    public GameObject Play, Petunjuk, Petunjuk2;
    public List<GameObject> MateriSlides; // List untuk semua slide materi

    private int currentSlideIndex = -1; // Indeks slide aktif

    void Start()
    {
        // Validasi awal
        if (spriteMute == null || spriteMute.Length < 2)
        {
            Debug.LogError("spriteMute is not properly assigned or has less than 2 sprites!");
        }

        if (allMuteButtons == null || allMuteButtons.Count == 0)
        {
            Debug.LogWarning("allMuteButtons is empty or not assigned!");
        }

        if (Play == null || Petunjuk == null || Petunjuk2 == null)
        {
            Debug.LogError("One or more GameObject references (Play, Petunjuk, Petunjuk2) are not assigned!");
        }

        // Mengatur objek default
        Play?.SetActive(true);
        Petunjuk?.SetActive(false);
        Petunjuk2?.SetActive(false);

        // Menonaktifkan semua slide materi
        foreach (var slide in MateriSlides)
        {
            slide?.SetActive(false);
        }

        // Pastikan ikon mute diupdate sesuai status awal
        UpdateMuteUI();
    }

    void SoundButton()
    {
        // Memanggil fungsi SoundButton dari SistemSound jika objek ditemukan
        if (GameObject.Find("SistemSound") != null && SistemSound.instance != null)
        {
            SistemSound.instance.SoundButton();
        }
        else
        {
            Debug.LogWarning("SistemSound instance is not found!");
        }
    }

    public void StartButton(string scenename)
    {
        SoundButton();
        SceneManager.LoadScene(scenename); // Memuat scene baru
    }

    public void ButtonMute()
    {
        SoundButton(); // Mainkan efek suara tombol

        // Validasi SistemSound
        if (SistemSound.instance == null)
        {
            Debug.LogError("SistemSound instance is not found!");
            return;
        }

        // Toggle mute sound
        SistemSound.instance.MuteSound();

        // Perbarui ikon mute/unmute di semua tombol
        UpdateMuteUI();
    }

    private void UpdateMuteUI()
    {
        // Periksa apakah spriteMute sudah diatur dengan benar
        if (spriteMute == null || spriteMute.Length < 2)
        {
            Debug.LogError("spriteMute is not properly assigned or has less than 2 sprites!");
            return;
        }

        // Periksa apakah SistemSound tersedia
        if (SistemSound.instance == null)
        {
            Debug.LogError("SistemSound instance is not found in the scene!");
            return;
        }

        // Periksa status mute dan perbarui sprite tombol mute
        Sprite newSprite = SistemSound.instance.music.mute ? spriteMute[1] : spriteMute[0];

        foreach (var muteButton in allMuteButtons)
        {
            if (muteButton != null) // Pastikan tombol tidak null
            {
                if (muteButton.image != null)
                {
                    muteButton.image.sprite = newSprite;
                }
                else
                {
                    Debug.LogWarning("Mute button does not have an Image component!");
                }
            }
            else
            {
                Debug.LogWarning("A mute button in allMuteButtons is null!");
            }
        }
    }

    public void materibtn()
    {
        SoundButton();
        Play?.SetActive(false);

        // Menampilkan slide pertama jika ada
        if (MateriSlides.Count > 0)
        {
            currentSlideIndex = 0;
            UpdateSlideVisibility();
        }
    }

    public void NextSlide()
    {
        SoundButton();

        if (currentSlideIndex < MateriSlides.Count - 1)
        {
            currentSlideIndex++;
            UpdateSlideVisibility();
        }
    }

    public void PreviousSlide()
    {
        SoundButton();

        if (currentSlideIndex > 0)
        {
            currentSlideIndex--;
            UpdateSlideVisibility();
        }
    }

    private void UpdateSlideVisibility()
    {
        // Menonaktifkan semua slide terlebih dahulu
        foreach (var slide in MateriSlides)
        {
            slide?.SetActive(false);
        }

        // Mengaktifkan slide saat ini
        if (currentSlideIndex >= 0 && currentSlideIndex < MateriSlides.Count)
        {
            MateriSlides[currentSlideIndex]?.SetActive(true);
        }
    }

    public void petunjukbtn()
    {
        SoundButton();
        Play?.SetActive(false);
        Petunjuk?.SetActive(true);
    }

    public void nextslidebtn()
    {
        SoundButton();
        Petunjuk?.SetActive(false);
        Petunjuk2?.SetActive(true);
    }

    public void backslidebtn()
    {
        SoundButton();
        Petunjuk?.SetActive(true);
        Petunjuk2?.SetActive(false);
    }

    public void exitbtn()
    {
        SoundButton();
        Play?.SetActive(true);
        Petunjuk?.SetActive(false);
        Petunjuk2?.SetActive(false);

        // Reset slide materi jika dibutuhkan
        foreach (var slide in MateriSlides)
        {
            slide?.SetActive(false);
        }
        currentSlideIndex = -1;
    }
}
