using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Book : MonoBehaviour
{
    public enum BlackWhite { left = 0, right = 2 }
    public Text title;
    //걍 하이러키뷰에서 미리 넣어놨음. 0은 왼쪽, 1은 오른쪽 즉, 총 길이는 2
    //bookPages의 0,1,2,3에 넣어주기, 0,1 그리고 2,3 에게...
    [SerializeField] RawImages[] bookPages;
    [SerializeField] RawImages[] centerPages;

    private Animator bookAnim;
    private Page page;
    [SerializeField] private FolderInfo folderInfo;
    //[SerializeField] private WorldInfo worldInfo;
    private bool isOpenable = true;     //페이지든 책이든 열기 가능한지
    private float bookAnimTime = 0;     //책 애니메이션 시간
    private float pageAnimTime = 0;     //페이지 애니메이션 시간

    //원래 isBookOpened 였는데 auto 속성하니까 이렇게 됨
    public bool IsBookOpened { get; private set; } = false;
    public FolderInfo FolderInfo { get => folderInfo; private set => folderInfo = value; }
    //public WorldInfo WorldInfo { get => worldInfo; private set => worldInfo = value; }


    #region Public Method : 외부에선 이 메서드에만 접근해야함
    //초기화
    public void InitBook(FolderInfo info)
    {
        //필드 초기화
        bookAnim = GetComponent<Animator>();
        page = GetComponentInChildren<Page>(true);
        pageAnimTime = page.InitPage();
        page.OnOffPage(false);
        folderInfo = info;
        title.text = info.folderName;
        var objs = GetComponentsInChildren<InteractableObject>();
        foreach(InteractableObject obj in objs)
        {
            obj.ProcessInit(this);
        }

        //책 애니메이션 클립 길이 찾기
        RuntimeAnimatorController acb = bookAnim.runtimeAnimatorController;
        for (int i = 0; i < acb.animationClips.Length; i++)
        {
            if (acb.animationClips[i].name == "BookAnim")
            {
                bookAnimTime = acb.animationClips[i].length;
            }
        }
    }

    //책 열기
    public void OpenBook()
    {
        //사진 처음에 넣어주기
        for (int i = 0; i < 4; i++)
        {
            if (i > folderInfo.lastPhotoIndex)
                SetIndividualPhoto(i, null);
            else
                SetIndividualPhoto(i, folderInfo.photos[i]);
        }
        OCBook(true);

    }
    //책 닫기
    public void CloseBook()
    {
        OCBook(false);
        folderInfo.curBookPlaneIndex = 0;
    }

    public void NextPage() => PNPage(true);     //다음 페이지
    public void PrePage() => PNPage(false);     //이전 페이지

    #endregion

    #region Private Method : 외부에서 접근 불가능한 내부 사용 메서드들
    // 0이면 왼쪽의 두개 사진, 오른쪽이면 오른쪽 두개의 사진, 지금 인덱스로부터
    void SetPagePhoto(BlackWhite pageLR, BlackWhite photoLR)
    {
        SetIndividualPagePhoto((int)pageLR, folderInfo.photos[(folderInfo.curBookPlaneIndex * 4) + (int)photoLR]);
        SetIndividualPagePhoto((int)pageLR + 1, folderInfo.photos[(folderInfo.curBookPlaneIndex * 4) + (int)photoLR + 1]);
    }

    void SetBookPhoto(BlackWhite pageLR, BlackWhite photoLR)
    {
        SetIndividualPhoto((int)pageLR, folderInfo.photos[(folderInfo.curBookPlaneIndex * 4) + (int)photoLR]);
        SetIndividualPhoto((int)pageLR + 1, folderInfo.photos[(folderInfo.curBookPlaneIndex * 4) + (int)photoLR + 1]);
    }


    //todo : 나중에 private으로 바꿀 것
    void SetIndividualPhoto(int index, Texture2D texture)
    {
        switch (index)
        {
            case 0:
                if (texture == null)
                    bookPages[0].photo[0].color = new Color(0, 0, 0, 0);
                else
                {
                    bookPages[0].photo[0].color = new Color(1, 1, 1, 1);
                    bookPages[0].photo[0].texture = texture;
                }
                break;
            case 1:
                if (texture == null)
                    bookPages[0].photo[1].color = new Color(0, 0, 0, 0);
                else
                {
                    bookPages[0].photo[1].color = new Color(1, 1, 1, 1);
                    bookPages[0].photo[1].texture = texture;
                }
                break;
            case 2:
                if (texture == null)
                    bookPages[1].photo[0].color = new Color(0, 0, 0, 0);
                else
                {
                    bookPages[1].photo[0].color = new Color(1, 1, 1, 1);
                    bookPages[1].photo[0].texture = texture;
                }
                break;
            case 3:
                if (texture == null)
                    bookPages[1].photo[1].color = new Color(0, 0, 0, 0);
                else
                {
                    bookPages[1].photo[1].color = new Color(1, 1, 1, 1);
                    bookPages[1].photo[1].texture = texture;
                }
                break;
        }
    }

    void SetIndividualPagePhoto(int index, Texture2D texture)
    {
        switch (index)
        {
            case 0:
                if (texture == null)
                    centerPages[0].photo[0].color = new Color(0, 0, 0, 0);
                else
                {
                    centerPages[0].photo[0].color = new Color(1, 1, 1, 1);
                    centerPages[0].photo[0].texture = texture;
                }
                break;
            case 1:
                if (texture == null)
                    centerPages[0].photo[1].color = new Color(0, 0, 0, 0);
                else
                {
                    centerPages[0].photo[1].color = new Color(1, 1, 1, 1);
                    centerPages[0].photo[1].texture = texture;
                }
                break;
            case 2:
                if (texture == null)
                    centerPages[1].photo[0].color = new Color(0, 0, 0, 0);
                else
                {
                    centerPages[1].photo[0].color = new Color(1, 1, 1, 1);
                    centerPages[1].photo[0].texture = texture;
                }
                break;
            case 3:
                if (texture == null)
                    centerPages[1].photo[1].color = new Color(0, 0, 0, 0);
                else
                {
                    centerPages[1].photo[1].color = new Color(1, 1, 1, 1);
                    centerPages[1].photo[1].texture = texture;
                }
                break;
        }
    }


    //책을 열고 닫는 함수
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
    //페이지를 넘기는 함수
    void PNPage(bool booleana)
    {
        if (isOpenable == false) return;
        if (IsBookOpened == false) return;
        //페이지 설정
        if (booleana == true)
        {
            if (folderInfo.curBookPlaneIndex == folderInfo.maxBookPlaneIndex) return;
            SetPagePhoto(BlackWhite.left, BlackWhite.right);//넘길때 페이지포토 왼쪽은 현재의 오른쪽
            folderInfo.curBookPlaneIndex++;
            SetBookPhoto(BlackWhite.right, BlackWhite.right);//넘길때 책포토는 다음의 오른쪽
            SetPagePhoto(BlackWhite.right, BlackWhite.left);//넘길때 페이지포토는 다음의 왼쪽
        }
        else
        {
            if (folderInfo.curBookPlaneIndex == 0) return;
            SetPagePhoto(BlackWhite.right, BlackWhite.left);//넘길때 페이지포토 왼쪽은 현재의 오른쪽
            folderInfo.curBookPlaneIndex--;
            SetBookPhoto(BlackWhite.left, BlackWhite.left);//넘길때 책포토는 다음의 오른쪽
            SetPagePhoto(BlackWhite.left, BlackWhite.right);//넘길때 페이지포토는 다음의 왼쪽
        }
        StopAllCoroutines();
        isOpenable = false;
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
        //다 넘어와서는?
        if (booleana == true)
        {
            SetBookPhoto(BlackWhite.left, BlackWhite.left);//넘길때 책포토는 다음의 오른쪽
        }
        else
        {
            SetBookPhoto(BlackWhite.right, BlackWhite.right);//넘길때 책포토는 다음의 오른쪽
        }
        page.OnOffPage(false);
        isOpenable = true;
        //여기서 페이지를 설정해야한다.
    }
    #endregion
}

