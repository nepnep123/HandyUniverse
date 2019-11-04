﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KWorld : World
{
    public static KWorld instance;

    [HideInInspector]
    public float speed;
    private float speedMax;
    public KBullet kBullet;
    public int dieRealCount;

    public GameObject planet;
    public GameObject MissionSuccessParticle;
    public ParticleSystem hidePlanetParticle;

    public bool isFirstMissionStarted = false;
    public bool isSecondMissionStarted = false;
    public bool isGoodBye = false;
    public bool isThirdMissionStarted = false;
    public bool isThirdMissionStarted2 = false;
    private bool isGrabbed = false;
    private bool isEnabled = false;
    public bool test = false;

    public GameObject[] arrows = new GameObject[9];
    public MeshRenderer[] planetsMeshRenderer = new MeshRenderer[9];

    private int count = 0;

    private WaitForSeconds waitForSeconds = new WaitForSeconds(3f);

    public int grabCount = 0;
    public int releaseCount = 0;

    private void Awake()
    {
        if (instance == null) instance = GetComponent<KWorld>();
        else Destroy(this);
    }

    // 미션 후 나오는 파티클 끄고
    // 화살표 전부 끄고
    // 행성들 메쉬 전부 끄고
    private void Start()
    {
        speed = 0f;
        speedMax = 100f;

        MissionSuccessParticle.SetActive(false);

        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].GetComponent<KArrow>().targetPlanet = planetsMeshRenderer[i].transform;

            arrows[i].SetActive(false);
        }

        for (int i = 0; i < planetsMeshRenderer.Length; i++)
        {
            planetsMeshRenderer[i].enabled = false;
        }
    }

    // 시작하면 UI 나와
    public override void InitWorld()
    {
        GameManager.instance.hand.mode = GameManager.instance.hand.kTutorialMode;
        GameManager.instance.hand.kTutorialMode.kworld = this;

        UIManager.instance.StartCoroutine(UIManager.instance.InstructSequenceK());
        StartCoroutine(MissionStart());
    }

    // 첫번째 미션 스타트
    IEnumerator MissionStart()
    {
        yield return null;
        
        isFirstMissionStarted = true;
    }

    // 첫번째 미션 내용
    public void CountFirstMission(ManoGestureTrigger mgt)
    {
        if (mgt == ManoGestureTrigger.GRAB)
        {
            if(isEnabled == true)
            {
                for (int i = 0; i < planetsMeshRenderer.Length; i++)
                {                
                    planetsMeshRenderer[i].enabled = false;

                    var hiding = Instantiate(hidePlanetParticle, planetsMeshRenderer[i].transform.position, planetsMeshRenderer[i].transform.rotation);
                    hiding.Play();
                }
                Debug.Log(grabCount);
                grabCount++;
                isEnabled = false;
            }
        }
        else
        {
            if(isEnabled == false)
            {
                for (int i = 0; i < planetsMeshRenderer.Length; i++)
                {    
                    planetsMeshRenderer[i].enabled = true;
                }
                releaseCount++;
                Debug.Log(releaseCount);
                isEnabled = true;
            }
        }

        if ((grabCount >= 2) && (releaseCount >= 3))
        {
            StartCoroutine(MissionParticle1());
            UIManager.instance.StartCoroutine(UIManager.instance.InstructSequenceK2());
        }
        

        IEnumerator MissionParticle1()
        {
            MissionSuccessParticle.SetActive(true);
            yield return waitForSeconds;
            MissionSuccessParticle.SetActive(false);
            
            isFirstMissionStarted = false;
            for (int i = 0; i < planetsMeshRenderer.Length; i++)
            {
                planetsMeshRenderer[i].enabled = true;
            }
            isSecondMissionStarted = true;
        }
    }

    // 두번째 미션 내용
    public void CountSecondMission(ManoGestureTrigger mgt)
    {
        if (mgt == ManoGestureTrigger.CLICK)
        {
            HideArrow();

            if (count == 4)
            {
                isSecondMissionStarted = false;

                for (int i = 0; i < arrows.Length; i++)
                {
                    arrows[i].SetActive(false);
                }

                StartCoroutine(MissionParticle2());
                UIManager.instance.StartCoroutine(UIManager.instance.InstructSequenceK3());
            }

            IEnumerator MissionParticle2()
            {
                MissionSuccessParticle.SetActive(true);
                yield return waitForSeconds;
                MissionSuccessParticle.SetActive(false);
                isThirdMissionStarted = true;   // 주먹 활성화
            }
        }
    }

    public void HideArrow()
    {
        arrows[count].SetActive(true);

        ArrowPosition();

        arrows[GetPreIndex()].SetActive(false);

        count++;

        if (count > arrows.Length - 1)
        {
            count = 0;
        }
    }

    private void ArrowPosition()
    {
        arrows[count].transform.position = planetsMeshRenderer[count].transform.position + new Vector3(0f, 0.05f, 0f);
    }

    private int GetPreIndex()
    {
        int preIndex = count - 1;

        if (preIndex == -1)
        {
            return arrows.Length - 1;
        }
        else
        {
            return preIndex;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 세번째 미션 내용
    public void CountThirdMission(ManoGestureTrigger mgt)
    {
        if (mgt == ManoGestureTrigger.GRAB)
        {
            isGrabbed = true;
            StartCoroutine(ChargeSpeed());
        }
    }

    IEnumerator ChargeSpeed()
    {
        Debug.Log(speed);

        while (isGrabbed == true)
        {
            speed += 50 * Time.deltaTime;

            speed = Mathf.Clamp(speed, 0f, speedMax);

            yield return null;
            isThirdMissionStarted2 = true; // 테스트 : 보자기 활성화
        }
    }

    public void CountThirdMission2(ManoGestureTrigger mgt)
    {
        if (mgt == ManoGestureTrigger.RELEASE && isGrabbed == true)
        {
            isGrabbed = false;

            kBullet.getSpeed = speed;
            FireBullet();
            speed = 0;
            StartCoroutine(ChargeSpeed());
        }

        if (dieRealCount >= 3)
        {
            StartCoroutine(MissionParticle3());
            StartCoroutine(UIManager.instance.ShowMissionUI("수고하셨습니다!\n\n이제 '그랩 앤 릴리즈'로 포탈을 나가고\n튜토리얼에서 배운 핸드모션으로 행성 여행을 떠나봅시다"));
        }
    }

    IEnumerator MissionParticle3()
    {
        MissionSuccessParticle.SetActive(true);
        yield return waitForSeconds;
        MissionSuccessParticle.SetActive(false);

        isThirdMissionStarted = false; // 주먹 비활성화
        isThirdMissionStarted2 = false; // 보자기 비활성화
        isGoodBye = true; // 테스트 끝나고 주석 풀기
    }

    private void FireBullet()
    {
        var bulletBullet = Instantiate(kBullet);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // 포탈 아웃 내용
    public void CountGoodBye(ManoGestureTrigger mgt)
    {
        if (mgt == ManoGestureTrigger.GRAB)
        {
            GameManager.instance.masterBook.ClosePortal();
        }
    }
}