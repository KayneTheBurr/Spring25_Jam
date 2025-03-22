using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Return to the Main Menu
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("0_MainMenu");
    }
}
