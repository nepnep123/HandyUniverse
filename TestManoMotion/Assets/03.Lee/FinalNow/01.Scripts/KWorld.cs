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
    private bool isEnabled = false;

    public GameObject[] arrows = new GameObject[9];
    public MeshRenderer[] planetsMeshRenderer = new MeshRenderer[9];

    private int count = 0;

    private WaitForSeconds waitForSeconds = new WaitForSeconds(3f);

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
            arrows[i].GetComponent<KArrow>().targetPlanet = planetsMeshRenderer[i].transform;

            arrows[i].SetActive(false);
        }

        for (int i = 0; i < planetsMeshRenderer.Length; i++)
        {
            planetsMeshRenderer[i].enabled = false;
        }
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

        //  Start SecondMission!!
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

    public void CountGoodBye(ManoGestureTrigger mgt)
    {
        if (mgt == ManoGestureTrigger.GRAB)
        {
            GameManager.instance.masterBook.ClosePortal();
        }
    }
}