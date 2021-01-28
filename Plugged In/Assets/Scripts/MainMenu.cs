using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        Debug.Log("You have quit the game");
        Application.Quit();
    }
    public void BackButton()
    {
       SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
