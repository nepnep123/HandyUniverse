using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Book이 가지는 기능 :
//초기화
//페이지 넘기기
public class Book_v2 : MonoBehaviour//InteractableBook
{
    private Animator bookAnim;
    private Page page;
    private bool isOpenable = true;     //페이지든 책이든 열기 가능한지
    private float bookAnimTime = 0;     //책 애니메이션 시간
    private float pageAnimTime = 0;     //페이지 애니메이션 시간
    public int curPlaneIndex = 0;
    public int maxBookPlaneIndex = 0;


	//델리게이트 이벤트, 책을 넘길때 발생
	public event VoidNotier OnBookOpened;
    public event VoidBoolNotier OnPageFlipStart;
    public event VoidBoolNotier OnPageFlipedEnd;
    public event VoidNotier OnRequestPortal;
	public event VoidNotier OnRequestClosePortal;

    public Text title;
    //걍 하이러키뷰에서 미리 넣어놨음. 0은 왼쪽, 1은 오른쪽 즉, 총 길이는 2
    //bookPages의 0,1,2,3에 넣어주기, 0,1 그리고 2,3 에게...
    [SerializeField] public RawImages[] bookPages;
    [SerializeField] public RawImages[] centerPages;

    //원래 isBookOpened 였는데 auto 속성하니까 이렇게 됨
    public bool IsBookOpened { get; private set; } = false;
    public bool isPlanetGrowing = false;
    // Start is called before the first frame update

    #region 외부접근 가능 메서드
    public void InitBook(string name)
    {
        //이 책 초기화
        bookAnim = GetComponent<Animator>();
        page = GetComponentInChildren<Page>(true);
        title.text = name;
        pageAnimTime = page.InitPage();
        page.OnOffPage(false);
        //자식들 초기화
        var inters = GetComponentsInChildren<InteractableObject>();
        foreach (InteractableObject inter in inters)
        {
            inter.ProcessInit(this);
        }
        //책 애니메이션 시간 찾기
        RuntimeAnimatorController acb = bookAnim.runtimeAnimatorController;
        for (int i = 0; i < acb.animationClips.Length; i++)
        {
            if (acb.animationClips[i].name == "BookAnim")
            {
                bookAnimTime = acb.animationClips[i].length;
            }
        }
    }

    public void OpenBook() => OCBook(true);
    public void CloseBook() => OCBook(false);
    public void NextPage() => PNPage(true);
    public void PrePage() => PNPage(false);
    public void OpenPortal() => OnRequestPortal?.Invoke();

	public void ClosePortal() => OnRequestClosePortal?.Invoke();

	#endregion

	#region 내부 메서드

	void OCBook(bool booleana)
    {
        if (isOpenable == false) return;
        if (booleana == IsBookOpened) return;
        StopAllCoroutines();
        bookAnim.SetBool("OpenBook", booleana);
        if (booleana == false) { page.OnOffPage(false); }
        isOpenable = false;
        StartCoroutine(CheckBookTime(booleana));
    }
    void PNPage(bool booleana)
    {
        if (isOpenable == false) return;
        if (IsBookOpened == false) return;
        if (isPlanetGrowing != false) return;
        if (booleana)
        {
            if (curPlaneIndex == maxBookPlaneIndex) return;
            curPlaneIndex++;
        }
        else
        {
            if (curPlaneIndex == 0) return;
            curPlaneIndex--;
        }
        StopAllCoroutines();
        isOpenable = false;
		OnPageFlipStart?.Invoke(booleana);
		StartCoroutine(CheckPageTime(booleana));
    }

    //책을 열고 닫을 때 다른 행동을 막는 이뉴머레이터
    IEnumerator CheckBookTime(bool booleana)
    {
        float curTime = 0;
        while (curTime < bookAnimTime * 0.8f)
        {
            curTime += Time.deltaTime;
            yield return null;
        }
		//BookPageSetter가 구독하고 있다. 
		OnBookOpened?.Invoke();
		isOpenable = true;
        IsBookOpened = booleana;
    }
    IEnumerator CheckPageTime(bool booleana)
    {
        float curTime = 0;
        page.OnOffPage(true);
        page.PNPage(booleana);//애니메이션 시작
        while (curTime < pageAnimTime)
        {
            curTime += Time.deltaTime;
            yield return null;
        }
        OnPageFlipedEnd?.Invoke(booleana);
        page.OnOffPage(false);
        isOpenable = true;
        //여기서 페이지를 설정해야한다.
    }
    #endregion

    #region 오버라이드 메서드
    #endregion
}
