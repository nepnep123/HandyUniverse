using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlanetGameManager : MonoBehaviour
{
    public static PlanetGameManager instance;

    private GameObject panelGameOver;
    private GameObject panelPause;

    private Text TextDistanceScoreResult;       // 출력 점수
    private Text TextHighDistanceScoreResult;   // 출력 최고 점수

    public static bool gameIsPaused;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TextDistanceScoreResult = GameObject.Find("TextDistanceScoreResult").GetComponent<Text>();
        TextHighDistanceScoreResult = GameObject.Find("TextHighDistanceScoreResult").GetComponent<Text>();

        panelGameOver = GameObject.Find("PanelGameOver");
        panelPause = GameObject.Find("PanelPause");
        panelPause.SetActive(false);
        panelGameOver.SetActive(false);

    }

    public void Restart() // 씬 리스타트
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame() // 게임끝 -> 패널 짠
    {
        TextDistanceScoreResult.text = "NOW SCORE : " + Mathf.Round(PlayerPrefs.GetFloat("NowScore")) + "M";
        TextHighDistanceScoreResult.text = "HIGH SCORE : " + Mathf.Round(PlayerPrefs.GetFloat("HighScore")) + "M";
        panelGameOver.SetActive(true);
    }

    public void PauseGame() // 일시정지
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ResumeGame() // 일시정지 해제
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void QuitGame() // 게임종료
    {
        Application.Quit();
    }
}
