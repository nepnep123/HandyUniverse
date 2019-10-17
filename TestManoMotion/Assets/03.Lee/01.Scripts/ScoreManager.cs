using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.005f);
    private Text textScore;

    private int preCoin = 0;
    private int coin = 0;

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
        textScore.text = "3000";
    }

    public void AddScore()
    {
        coin += 100;
        StartCoroutine(CoinUp(coin));
       
    }
    IEnumerator CoinUp(int coin)
    {
        for (int i = preCoin; i <= coin; i++)
        {
            yield return waitForSeconds;
            textScore.text = i.ToString();
        }
        preCoin = coin;
    }
}