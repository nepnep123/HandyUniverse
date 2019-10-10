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

    int curPlaneIndex = 0;
    World curOpenedWorld;
    // Start is called before the first frame update

    public void InitBookSetter(PageInfo_Scriptable[] infos)
    {
        book = GetComponent<Book_v2>();
        book.OnPageFlipStart += PageStartSub;
        book.OnPageFlipedEnd += PageEndSub;
        book.OnRequestPortal += OpenPortal;
        if (infos.Length == 0) Debug.LogError("아무런 스크립터블을 받지 못하였다.");
        pageInfos = infos;
        maxBookPlaneIndex = pageInfos.Length;
        book.maxBookPlaneIndex = maxBookPlaneIndex -1;
        //초기 설정
        raw.texture = pageInfos[0].leftTexture;
        text.text = pageInfos[0].rightDescribe;
    }

    private void OnDisable()
    {
        book.OnPageFlipStart -= PageStartSub;
        book.OnPageFlipedEnd -= PageEndSub;
        book.OnRequestPortal -= OpenPortal;
    }
    #region 외부 접근 가능 메서드
    public void OpenPortal()
    {
        //영훈씨가 포탈을 instantiate하세요
        Debug.Log(pageInfos[curPlaneIndex].world.name);
        //curOpenedWorld = pageInfos[curPlaneIndex].world;
        TestManager_v2.instance.testia.text = FindObjectOfType<PrimeHand>().curObj.name;
        TestManager_v2.instance.testiby.text = pageInfos[curPlaneIndex].world.name;
        //여기가 인스턴트화하는 곳
        Instantiate(pageInfos[curPlaneIndex].world, transform.position, Quaternion.identity);
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
    }

    protected virtual void PageEndSub(bool booleana)
    {
        if (booleana)
            raw.texture = pageInfos[book.curPlaneIndex].leftTexture;
        else
            text.text = pageInfos[book.curPlaneIndex].rightDescribe;
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
}
