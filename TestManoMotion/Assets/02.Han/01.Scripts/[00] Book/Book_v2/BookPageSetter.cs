using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookPageSetter : MonoBehaviour
{
    protected Book_v2 book;

    public RawImage raw;
    public Text text;

    public Text centText;
    public RawImage centRaw;

    public PageInfo_Scriptable[] pageInfos;
    public int maxBookPlaneIndex = 0;

    World curOpenedWorld;
	

	public Transform[] pagePlanets_pre;
	public World[] world_pre;

	//송영훈
	//책안에 있는 행성 및 맵 생성 후 True 반환
	bool isSetting = false;
	Transform planetPos;

	public void InitBookSetter(PageInfo_Scriptable[] infos)
    {
        book = GetComponent<Book_v2>();
        book.OnPageFlipStart += PageStartSub;
        book.OnPageFlipedEnd += PageEndSub;
        book.OnRequestPortal += OpenPortal;
		book.OnBookOpened += BookOpenedSub;

		if (infos.Length == 0) Debug.LogError("아무런 스크립터블을 받지 못하였다.");
        pageInfos = infos;
        maxBookPlaneIndex = pageInfos.Length;
        book.maxBookPlaneIndex = maxBookPlaneIndex -1;
        //초기 설정
        raw.texture = pageInfos[0].leftTexture;
        text.text = pageInfos[0].rightDescribe;


		//송영훈
		planetPos = transform.Find("PlanetPos");

		pagePlanets_pre = new Transform[pageInfos.Length];
		world_pre = new World[pageInfos.Length];
		//카메라 위치에 해당 페이지에 맵 생성
		//해당 페이지에 미니 행성 생성
		if (!isSetting)
		{
			for (int i = 0; i < pageInfos.Length; i++)
			{

				pagePlanets_pre[i] = Instantiate(pageInfos[i].pagePlanet, planetPos.position, Quaternion.identity);
				Debug.Log(planetPos.name);
				//마스터북 초기화 
				pagePlanets_pre[i].GetComponent<InteractablePlanet>().ProcessInit(book);

				world_pre[i] = Instantiate(pageInfos[i].world, GameManager.instance.camPos.position, Quaternion.identity);

				Debug.Log("pageplanet = " + pagePlanets_pre[i]);
				Debug.Log("world_pre = " + world_pre[i]);
			}
			EnableAllPage();
			isSetting = true;
		}
	}

    private void OnDisable()
    {
        book.OnPageFlipStart -= PageStartSub;
        book.OnPageFlipedEnd -= PageEndSub;
        book.OnRequestPortal -= OpenPortal;
		book.OnBookOpened -= BookOpenedSub;
	}


	#region 외부 접근 가능 메서드
	public void OpenPortal()
	{
		//백그라운드 끄고 / 마스터북 끄고 / 페이드 아웃 - 페이드 인 
		GameManager.instance.StartCoroutine("EnterWorld");
		world_pre[book.curPlaneIndex].gameObject.SetActive(true);
		pagePlanets_pre[book.curPlaneIndex].gameObject.SetActive(false);

	}

    public void ClosePortal()
    {

    }

    #endregion

    #region 내부사용 메서드
    protected virtual void PageStartSub(bool booleana)
    {

        if(booleana)
        {
            SetNextingPage(book.curPlaneIndex);
		}
        else
        {
            SetPreingPage(book.curPlaneIndex);
		}
		EnableAllPage();
	}

    protected virtual void PageEndSub(bool booleana)
    {
        if (booleana)
		{
            raw.texture = pageInfos[book.curPlaneIndex].leftTexture;
			
		}
		else
		{
            text.text = pageInfos[book.curPlaneIndex].rightDescribe;
		}
		pagePlanets_pre[book.curPlaneIndex].gameObject.SetActive(true);
	}

    void SetNextingPage(int curIndex)
    {
		//현재플레인의 텍스트, 다음플레인의 텍스쳐//다음플레인의 텍스트
		centText.text = pageInfos[curIndex-1].rightDescribe;//현재플레인의 텍스트
        centRaw.texture = pageInfos[curIndex].leftTexture;//다음플레인의 텍스쳐
        text.text = pageInfos[curIndex].rightDescribe;
    }
    void SetPreingPage(int curIndex)
    {
		centText.text = pageInfos[curIndex].rightDescribe;//이전플레인의 텍스트
        centRaw.texture = pageInfos[curIndex+1].leftTexture;//다음플레인의 텍스쳐
        raw.texture = pageInfos[curIndex].leftTexture;
    }
	#endregion

	//송영훈
	void BookOpenedSub()
	{
		//해당 페이지 행성 생성
		pagePlanets_pre[0].gameObject.SetActive(true);
	}

	void EnableAllPage()
	{
		for (int i = 0; i < pageInfos.Length; i++)
		{
			pagePlanets_pre[i].gameObject.SetActive(false);
			world_pre[i].gameObject.SetActive(false);
		}
	}
}
