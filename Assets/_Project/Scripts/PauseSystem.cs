using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{

    public GameObject PausePanel;
    public KeyCode PauseKey = KeyCode.Escape;
    public static bool isPaused;
    public AudioListener AudioListener;

    public FirstPersonLook FirstPersonLook;

    // Update is called once per frame
    void Update()
    {
        if(!isPaused && Input.GetKey(PauseKey))
        {
            Pause();
        }

        
    }


    public void Pause() // Fungsi Pause
    {
        PausePanel.SetActive(true); // Aktifkan PausePanel
        Time.timeScale = 0f; // Ubah waktu menjadi 0
        isPaused = true; // isPause bernilai 1
        AudioListener.pause = true; // Pause audio
        FirstPersonLook.sensitivity = 0f; // Ubah sensivity menjadi 0

        Debug.Log("Game Paused");

    }

    public void Resmue() // Fungsi resume
    {
        PausePanel.SetActive(false); // Matikan PausePanel
        Time.timeScale = 1f; // Ubah waktu menjadi 1
        isPaused = false; // isPause bernilai 0
        AudioListener.pause = false; // Mulai audio lagi
        FirstPersonLook.sensitivity = 7f; // Ubah sensivity menjadi 7
        Debug.Log("Game Resumed");

    }
}
