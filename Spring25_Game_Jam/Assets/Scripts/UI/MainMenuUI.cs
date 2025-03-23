using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMainGame()
    {
        SceneManager.LoadScene("1_MainScene");
    }

    public void SeeControls()
    {
        SceneManager.LoadScene("00_Controls");
    }

    public void Quit()
    {
        // Quit the game (this will work in a built application)
        Application.Quit();
    }
}

