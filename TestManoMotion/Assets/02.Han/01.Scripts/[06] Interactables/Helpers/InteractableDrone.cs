using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDrone : InteractableObject
{
    //드론 로봇
    Helper helper;
    bool isOpened = false;

    //델리게이트 스택 : 뒤로가기 구현용
    public Stack<VoidNotier> releaseStack;

    public override void ProcessInit<T>(T obj)
    {
        //만약 T가 Helper라면 초기화 실시
        if(obj is Helper)
        {
            helper = obj as Helper;
            releaseStack = new Stack<VoidNotier>();
        }
    }

    public void ReturnBack()
    {
        //마지막으로 저장한 메서드 실행
        if (releaseStack.Count > 0)
        {
            releaseStack.Pop()();
        }
    }

    public override void ProcessCollisionEnter()
    {
        //충돌 이벤트를 처리할 때 드론이 열고 있는 모든 아이콘 열기
        if (isOpened == false)
        {
            SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.venusSoundPack.venusDronePick);
            helper.OpenObjects(true);
            isOpened = true;
            //드론 아이콘 닫기 메서드를 델리게이트스택에 푸쉬
            //ReturnBack()으로 팝
            releaseStack.Push(RequestClose);
        }
    }

    public void RequestClose()
    {
        //사진들, 인포들 모두 닫기
        isOpened = false;
        helper.OpenObjects(false);
    }
}
