using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame() // Fungsi untuk tombol NewGame
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit() // Fungsi untuk tombol Exit
    {
        Debug.Log("App Quit");
        Application.Quit();
    }
}
