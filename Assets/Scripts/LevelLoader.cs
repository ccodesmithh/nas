using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextLevel()
    {
        StartCoroutine(Loadlevel(SceneManager.GetActiveScene().buildIndex + 1)); // Mulai coroutine Loadlevel. pindah ke scene dengan cara mendapatkan build index scene saat ini lalu ditambah 1
    }

    public void LoadLevel(int levelIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
    }

    IEnumerator Loadlevel(int levelindex) // gun
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelindex);
        
    }
}
