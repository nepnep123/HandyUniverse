using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    private Text textDistanceScore;            // 점수
    private Text textNewDistanceScore;         // 최고 점수

    private float distanceScore;
    private float newDistanceScore;

    private float distanceScorePerSeconds = 20;

    private bool distanceScoreIncreasing = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        textDistanceScore = GameObject.Find("TextDistanceScore").GetComponent<Text>();
        textNewDistanceScore = GameObject.Find("TextHighDistanceScore").GetComponent<Text>();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            newDistanceScore = PlayerPrefs.GetFloat("HighScore"); 
        }
    }

    private void Update()
    {
        if(distanceScoreIncreasing)
        {
            distanceScore += distanceScorePerSeconds * Time.deltaTime;
            PlayerPrefs.SetFloat("NowScore", distanceScore);
        }

        if (distanceScore > newDistanceScore)
        {
            newDistanceScore = distanceScore;
            PlayerPrefs.SetFloat("HighScore", newDistanceScore);
        }

        textDistanceScore.text = Mathf.Round(distanceScore) + "M";
        textNewDistanceScore.text = Mathf.Round(newDistanceScore) + "M";
    }
}
