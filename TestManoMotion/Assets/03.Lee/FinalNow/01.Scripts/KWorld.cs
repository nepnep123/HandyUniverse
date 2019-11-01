using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KWorld : World
{
    public static KWorld instance;

    public GameObject planet;
    public GameObject MissionSuccessParticle;
    public ParticleSystem hidePlanetParticle;

    public bool isFirstMissionStarted = false;
    public bool isSecondMissionStarted = false;
    public bool isGoodBye = false;

    public GameObject[] arrows = new GameObject[8];
    public GameObject[] planets = new GameObject[8];

    private int count = 0;

    private WaitForSeconds waitForSeconds = new WaitForSeconds(2.9f);

    private void Awake()
    {
        if (instance == null) instance = GetComponent<KWorld>();
        else Destroy(this);
    }

    private void Start()
    {
        MissionSuccessParticle.SetActive(false);

        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].GetComponent<KArrow>().targetPlanet = planets[i].transform;

            arrows[i].SetActive(false);
        }

        planet.SetActive(false);
    }

    public override void InitWorld()
    {
        GameManager.instance.hand.mode = GameManager.instance.hand.kTutorialMode;
        GameManager.instance.hand.kTutorialMode.kworld = this;

        UIManager.instance.StartCoroutine(UIManager.instance.InstructSequenceK());
        StartCoroutine(MissionStart());
    }

    IEnumerator MissionStart()
    {
        yield return null;
        
        isFirstMissionStarted = true;
    }

    public int grabCount = 0;
    public int releaseCount = 0;

    public void CountFirstMission(ManoGestureTrigger mgt)
    {
        if (mgt == ManoGestureTrigger.GRAB)
        {
            planet.SetActive(false);

            for (int i = 0; i < arrows.Length; i++)
            {
                var hiding = Instantiate(hidePlanetParticle, planets[i].transform.position, planets[i].transform.rotation);
                hiding.Play();
            }

            grabCount++;
        }
        else
        {
            planet.SetActive(true);
            releaseCount++;
        }

        //  Start SecondMission!!
        if ((grabCount >= 2) && (releaseCount >= 2))
        {
            StartCoroutine(MissionParticle1());
        }

        IEnumerator MissionParticle1()
        {
            MissionSuccessParticle.SetActive(true);
            yield return waitForSeconds;
            MissionSuccessParticle.SetActive(false);

            // 파티클 생성 = 성공의 의미
            StartCoroutine(UIManager.instance.ShowMissionUI("두번째는 '클릭'입니다\n\n다음과 같은 모션을 취하여\n'클릭'으로 차례를 넘겨봅시다"));
            
            isFirstMissionStarted = false;
            planet.SetActive(true); // 만에하나
            isSecondMissionStarted = true;
        }
    }

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
            }

            IEnumerator MissionParticle2()
            {
                MissionSuccessParticle.SetActive(true);
                yield return waitForSeconds;
                MissionSuccessParticle.SetActive(false);
                StartCoroutine(UIManager.instance.ShowMissionUI("수고하셨습니다!\n\n이제 '그랩 앤 릴리즈'로 포탈을 나가고\n튜토리얼에서 배운 핸드모션으로 행성 여행을 떠나봅시다"));
                isGoodBye = true;
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
        arrows[count].transform.position = planets[count].transform.position + new Vector3(0f, 0.05f, 0f);
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

    public void CountGoodBye(ManoGestureTrigger mgt)
    {
        if (mgt == ManoGestureTrigger.GRAB)
        {
            GameManager.instance.masterBook.ClosePortal();
        }
    }
}