using UnityEngine;

public class PauseAndQuit : MonoBehaviour
{
    private GameObject panelPause;

    public static bool gameIsPaused;

    private void Start()
    {
        panelPause = GameObject.Find("PanelPause");
        panelPause.SetActive(false);
    }

    public void PauseGame()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}