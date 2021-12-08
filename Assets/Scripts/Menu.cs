using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //load next level
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeVolume()
    {
        
    }

    //quit the application
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
}
