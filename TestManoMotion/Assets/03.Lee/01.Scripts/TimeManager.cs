using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private Text textCountDown;

    //private Color colorStart = Color.black;
    //private Color colorEnd = Color.red;

    private float currentTime = 0f;
    private float startTime = 21f;
    //private float duration = 1f;

    private bool isFinished;

    private void Start()
    {
        textCountDown = GameObject.Find("TextCountDown").GetComponent<Text>();
        currentTime = startTime;
    }

    private void Update()
    {
        CountDown();
    }

    private void CountDown()
    {
        if (isFinished)
            return;

        currentTime -= 1 * Time.deltaTime;
        textCountDown.text = currentTime.ToString("00");

        if (currentTime <= 5.5f)
        {
            textCountDown.color = Color.red;
            //float lerp = Mathf.PingPong(Time.time, duration) / duration;
            //textCountDown.material.color = Color.Lerp(co, colorEnd, lerp);
        }

        if (currentTime <= 0)
        {
            currentTime = 0;
            isFinished = true;
        }
    }
}