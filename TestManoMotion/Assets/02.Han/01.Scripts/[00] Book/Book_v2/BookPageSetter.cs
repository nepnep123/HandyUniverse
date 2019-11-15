using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookPageSetter : MonoBehaviour
{
    //한재현
    //책 페이지 설정에 필요한 필드들
    private Book_v2 book;
    public RawImage raw;                    //책의 왼쪽 텍스쳐
    public Text text;                       //책플레인의 오른쪽 텍스트
    public Text centText;                   //페이지의 오른쪽 텍스트
    public RawImage centRaw;                //넘어가는 페이지
    public PageInfo_Scriptable[] pageInfos; //스크립터블 오브젝트로 만들어진 페이지 정보
    public int maxBookPlaneIndex = 0;

    //송영훈
    //책과 게임 매니져, 책속의 행성
	private GameManager gameMgr;
	public Transform[] pagePlanets_pre;
	public World[] world_pre;

	//책안에 있는 행성 및 맵 생성 후 True 반환
	bool isSetting = false;
	public Transform planetPos;

    //한재현
    //책 초기화
	public void InitBookSetter(PageInfo_Scriptable[] infos)
    {
        book = GetComponent<Book_v2>();
        book.OnPageFlipStart += PageStartSubscriber;    //페이지가 넘어가기 시작할 때
        book.OnPageFlipedEnd += PageEndSubscriber;      //페이지가 다 넘어 왔을 때
        book.OnRequestPortal += OpenPortal;             //행성 입장 요청을 할 때
		book.OnBookOpened += BookOpenedSub;             //책이 열렸을 때
		book.OnRequestClosePortal += ExitWorldCtrl;     //행성에서 퇴장할 때

        //페이지를 초기화함
		if (infos.Length == 0) Debug.LogError("아무런 스크립터블을 받지 못함");
        pageInfos = infos;
        maxBookPlaneIndex = pageInfos.Length;           
        book.maxBookPlaneIndex = maxBookPlaneIndex -1;  //책 면의 최대 수
        raw.texture = pageInfos[0].leftTexture;
        text.text = pageInfos[0].rightDescribe;


		//송영훈
        //책을 폈을 때 행성이 떠있게 하는 작업
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
				
				//마스터북 초기화 
				pagePlanets_pre[i].GetComponent<InteractablePlanet>().ProcessInit(book);

				world_pre[i] = Instantiate(pageInfos[i].world, GameManager.instance.camPos.position, Quaternion.identity);

				Debug.Log("pageplanet = " + pagePlanets_pre[i]);
				Debug.Log("world_pre = " + world_pre[i]);
			}
			DisableAllPage();
			isSetting = true;
		}
	}
	private void OnDestroy()
    {
        book.OnPageFlipStart -= PageStartSubscriber;
        book.OnPageFlipedEnd -= PageEndSubscriber;
        book.OnRequestPortal -= OpenPortal;
		book.OnBookOpened -= BookOpenedSub;
		book.OnRequestClosePortal -= ExitWorldCtrl;
		book.bookOpenEffect.SetActive(false);
	}


	#region 외부 접근 가능 메서드
    //송영훈
    //행성 입장
	public void OpenPortal()
	{
		//백그라운드 끄고 / 마스터북 끄고 / 페이드 아웃 - 페이드 인 
		GameManager.instance.StartCoroutine(GameManager.instance.EnterWorld());
		world_pre[book.curPlaneIndex].gameObject.SetActive(true);
		world_pre[book.curPlaneIndex].InitWorld();
		pagePlanets_pre[book.curPlaneIndex].transform.position = planetPos.position;
		pagePlanets_pre[book.curPlaneIndex].gameObject.SetActive(false);
		book.gameObject.SetActive(false);
		GameManager.instance.zone.SetActive(false);
	}

	//Book_v2::ClosePortal()
    public void ExitWorldCtrl()
    {
		book.gameObject.SetActive(true);
		book.isOpenable = true;
		book.IsBookOpened = false;

		GameManager.instance.StartCoroutine(GameManager.instance.ExitWorld());
		GameManager.instance.zone.SetActive(true);
		
		//fog 삭제
		RenderSettings.fog = false;
		DisableAllPage();
	}

    #endregion

    #region 내부사용 메서드
    //한재현
    //페이지가 넘어가기 시작할 때 호출되는 메서드
    private void PageStartSubscriber(bool booleana)
    {
        if(booleana)
        {   
            //현재 페이지의 인덱스로 다음 페이지 설정
            SetNextingPage(book.curPlaneIndex);
		}
        else
        {
            //현재 페이지의 인덱스로 이전페이지 설정
            SetPreingPage(book.curPlaneIndex);
		}
		DisableAllPage();
	}

    private void PageEndSubscriber(bool booleana)
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
		book.bookOpenEffect.SetActive(true);
	}

    //다음 페이지 세팅
    void SetNextingPage(int curIndex)
    {
		//현재플레인의 텍스트, 다음플레인의 텍스쳐//다음플레인의 텍스트
		centText.text = pageInfos[curIndex-1].rightDescribe;    //이전플레인의 텍스트
        centRaw.texture = pageInfos[curIndex].leftTexture;      //다음플레인의 텍스쳐
        text.text = pageInfos[curIndex].rightDescribe;          //다음플레인의 텍스트
    }
    //이전페이지 세팅
    void SetPreingPage(int curIndex)
    {
		centText.text = pageInfos[curIndex].rightDescribe;      //이전플레인의 텍스트
        centRaw.texture = pageInfos[curIndex+1].leftTexture;    //다음플레인의 텍스쳐
        raw.texture = pageInfos[curIndex].leftTexture;          //이전플레인의 텍스쳐
    }
	#endregion
	
	//송영훈
	void BookOpenedSub()
	{
		//해당 페이지 행성 생성
		pagePlanets_pre[0].gameObject.SetActive(true);
		//UIManager에서 책을 열었을때 주변 효과를 생성 시킨다. 
		book.bookOpenEffect.SetActive(true);
	}
    //모든 행성 비활성화
	public void DisableAllPage()
	{
		for (int i = 0; i < pageInfos.Length; i++)
		{
			pagePlanets_pre[i].gameObject.SetActive(false);
			world_pre[i].gameObject.SetActive(false);
		}
		book.bookOpenEffect.SetActive(false);
	}
}
