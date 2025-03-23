using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject controlsPanel;
    public Button closeControlsPanelButton;
    public Button openControlsPanelButton;
    public Button playButton;
    public Button quitButton;

    private void Start()
    {
        quitButton.onClick.AddListener(Quit);
        playButton.onClick.AddListener(PlayMainGame);
        closeControlsPanelButton.onClick.AddListener(HideControls);
        openControlsPanelButton.onClick.AddListener(SeeControls);

        controlsPanel.SetActive(false);
    }
    public void PlayMainGame()
    {
        SceneManager.LoadScene(1);
    }
    public void SeeControls()
    {
        controlsPanel.SetActive(true);
    }
    public void HideControls()
    {
        controlsPanel.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

