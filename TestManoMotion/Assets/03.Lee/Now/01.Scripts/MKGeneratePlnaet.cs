using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 행성 생성
public class MKGeneratePlnaet : MonoBehaviour
{
    public static MKGeneratePlnaet instance;

    public GameObject planet;

    private GameObject buttonGeneratePlanet;
    private GameObject panelGameOver;
    private GameObject panelPause;
    private GameObject buttonPause;
    private Text textScoreResult;       // 출력 점수
    private Text textHighScoreResult;   // 출력 최고 점수

    Vector3 transformPos = new Vector3(0, 0, 0.4f);

    private WaitForSeconds waitForSeconds = new WaitForSeconds(3.5f);

    private void Awake()
    {
        instance = this;
        MKEnemySpawnManager.instance.enabled = false;
    }

    private void Start()
    {
        buttonGeneratePlanet = GameObject.Find("ButtonGeneratePlanet");
        panelGameOver = GameObject.Find("PanelGameOver");
        panelPause = GameObject.Find("PanelPause");
        buttonPause = GameObject.Find("ButtonPause");
        textScoreResult = GameObject.Find("TextScoreResult").GetComponent<Text>();
        textHighScoreResult = GameObject.Find("TextHighScoreResult").GetComponent<Text>();

        panelGameOver.SetActive(false);
        panelPause.SetActive(false);
    }

    public void MakePlanet()
    {
        var planetObj = Instantiate(planet);
        planetObj.transform.SetParent(Camera.main.transform.parent.transform);
        planetObj.transform.localPosition = transformPos;
        MKEnemySpawnManager.instance.planet = planetObj.transform;
        buttonGeneratePlanet.SetActive(false);

        MKScoreManager.instance.hasStarted = true;
        StartCoroutine(StartMKEnemySpawner());
    }

    IEnumerator StartMKEnemySpawner()
    {
        yield return waitForSeconds;
        MKEnemySpawnManager.instance.enabled = true;
        MKEnemySpawnManager.instance.StartCoroutine(MKEnemySpawnManager.instance.SpawnMeteor());
    }

    //public void EndGame() // 게임끝 -> 패널 짠
    //{
    //    TextDistanceScoreResult.text = "NOW SCORE : " + Mathf.Round(PlayerPrefs.GetFloat("NowScore")) + "M";
    //    TextHighDistanceScoreResult.text = "HIGH SCORE : " + Mathf.Round(PlayerPrefs.GetFloat("HighScore")) + "M";
    //    panelGameOver.SetActive(true);
    //}

    public void PauseGame() // 일시정지
    {
        panelPause.SetActive(true);
        buttonPause.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame() // 일시정지 해제
    {
        panelPause.SetActive(false);
        buttonPause.SetActive(true);
        Time.timeScale = 1f;
    }

    //public void Restart() // 씬 리스타트.. 이거 수정해야
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    public void QuitGame() // 게임종료
    {
        Application.Quit();
    }
}