using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonsManager : MonoBehaviour
{
    // Start the game (Load Main Scene)
    public void StartGame()
    {
        SceneManager.LoadScene("1_MainScene");
    }

    // Reload the current scene
    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Return to the Main Menu
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("0_MainMenu");
    }

}

