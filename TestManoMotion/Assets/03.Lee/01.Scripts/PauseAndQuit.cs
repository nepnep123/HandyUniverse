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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void ResumeGame()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void PauseGame()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}