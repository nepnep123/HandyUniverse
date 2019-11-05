using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KWorld : World
{
    public static KWorld instance;

    private AudioSource audioSource;
    public AudioClip bgm;

    [HideInInspector]
    public float speed;
    private float speedMax;
    public KBullet kBullet;
    
    private int _dieRealCount;
    public int dieRealCount
    {
        get { return _dieRealCount; }
        set
        {
            _dieRealCount = value;
            if (_dieRealCount == 4)
            {
                isThirdMissionStarted = false; // 주먹 비활성화
                isThirdMissionStarted2 = false; // 보자기 비활성화

                StartCoroutine(MissionParticle3());
                UIManager.instance.StartCoroutine(UIManager.instance.InstructSequenceK4());
            }
        }
    }

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
    // 시작하면 UI 나와
    public override void InitWorld()
    {
        GameManager.instance.hand.mode = GameManager.instance.hand.kTutorialMode;
        GameManager.instance.hand.kTutorialMode.kworld = this;

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

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgm;
        audioSource.Play();
        StartCoroutine(VolUp());

        UIManager.instance.StartCoroutine(UIManager.instance.InstructSequenceK());
    }

    IEnumerator VolUp()
    {
        for (float a = 0; a <= 0.07; a += 0.0001f)
        {
            audioSource.volume = a;

            yield return 0;  // 1 프레임 대기(화면 처리가 끝날 때까지)
        }
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

                    SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.hidepickPlanets);
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
                SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.hidepickPlanets);
                releaseCount++;
                Debug.Log(releaseCount);
                isEnabled = true;
            }
        }

        if ((grabCount >= 2) && (releaseCount >= 3))
        {
            isFirstMissionStarted = false;
            StartCoroutine(MissionParticle1());
            UIManager.instance.StartCoroutine(UIManager.instance.InstructSequenceK2());
        }
        

        IEnumerator MissionParticle1()
        {
            SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.success);
            MissionSuccessParticle.SetActive(true);
            yield return waitForSeconds;
            MissionSuccessParticle.SetActive(false);
            
            for (int i = 0; i < planetsMeshRenderer.Length; i++)
            {
                planetsMeshRenderer[i].enabled = true;
            }
        }
    }

    // 두번째 미션 내용
    public void CountSecondMission(ManoGestureTrigger mgt)
    {
        if (mgt == ManoGestureTrigger.CLICK)
        {
            HideArrow();

            if (count > 3)
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
                SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.success);
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
            speed += Time.deltaTime;

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
    }

    IEnumerator MissionParticle3()
    {
        SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.success);
        MissionSuccessParticle.SetActive(true);
        yield return waitForSeconds;
        MissionSuccessParticle.SetActive(false);
        StartCoroutine(VolDown());
    }

    IEnumerator VolDown()
    {
        for (float a = 0.07f; a >= 0; a -= 0.0001f)
        {
            audioSource.volume = a;

            yield return 0;  // 1 프레임 대기(화면 처리가 끝날 때까지)
        }
    }

    private void FireBullet()
    {
        var bulletBullet = Instantiate(kBullet, GameManager.instance.hand.transform.position, GameManager.instance.hand.transform.rotation);
        SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.fire);
        //var bulletBullet = Instantiate(kBullet, GameManager.instance.mano.palmcenter_parent.transform.position, GameManager.instance.hand.transform.rotation);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // 포탈 아웃 내용
    public void CountGoodBye(ManoGestureTrigger mgt)
    {
        if (mgt == ManoGestureTrigger.GRAB)
        {
            GameManager.instance.masterBook.ClosePortal();
            isGoodBye = false;
        }
    }
}