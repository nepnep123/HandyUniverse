using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MKScoreManager : MonoBehaviour
{
    public static MKScoreManager instance = null;

    private Text textScore;             // 점수
    private Text textHighScore;         // 최고 점수

    private float textScorePoint;
    private float highTextScorePoint;

    private float scoreUnit = 20;

    private bool scoreIncreasing = true;
    private bool hasStarted = false;

    private void Awake()
    {
        if (instance == null) instance = GetComponent<MKScoreManager>();
        else Destroy(instance);
    }

    private void Start()
    {
        MKManager.instance.OnPlanetCreated += StartCounting;

        textScore = GameObject.Find("TextScore").GetComponent<Text>();
        textHighScore = GameObject.Find("TextHighScore").GetComponent<Text>();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highTextScorePoint = PlayerPrefs.GetFloat("HighScore");
        }
    }

    private void OnDisable()
    {
        MKManager.instance.OnPlanetCreated -= StartCounting;
    }

    private void StartCounting()
    {
        hasStarted = true;
    }
    
    private void Update()
    {
        if (hasStarted == false)
        {
            return;
        }
        
        if (scoreIncreasing)
        {
            textScorePoint += scoreUnit * Time.deltaTime;
            PlayerPrefs.SetFloat("NowScore", textScorePoint);
        }

        if (textScorePoint > highTextScorePoint)
        {
            highTextScorePoint = textScorePoint;
            PlayerPrefs.SetFloat("HighScore", highTextScorePoint);
        }

        textScore.text = Mathf.Round(textScorePoint) + "M";
        textHighScore.text = Mathf.Round(highTextScorePoint) + "M";
    }
}
