using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    public Text textScore;
    private int coinCount = 0;

    private void Awake()
    {
        // 여러 사람이 개발할 때 => 유용 => 실수 방지용 => 건들지마
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        textScore = GameObject.Find("TextScore").GetComponent<Text>();
    }

    private void Update()
    {
        SetScore();
    }

    private void SetScore()
    {
        textScore.text = coinCount.ToString("3000");
    }

    public void AddScore()
    {
        coinCount++;
    }
}