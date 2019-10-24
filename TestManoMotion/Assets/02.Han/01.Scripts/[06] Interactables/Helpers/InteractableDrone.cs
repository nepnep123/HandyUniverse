using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDrone : InteractableObject
{
    Helper helper;
    bool isOpened = false;

    public Stack<VoidNotier> releaseStack;
    public override void ProcessInit<T>(T obj)
    {
        if(obj is Helper)
        {
            helper = obj as Helper;
        }
        releaseStack = new Stack<VoidNotier>();
    }

    public void ReturnBack()
    {
        if (releaseStack.Count > 0)
        {
            releaseStack.Pop()();
        }
    }

    public void RequestClose()
    {
        isOpened = false;
        helper.OpenObjects(false);
        //사진들, 인포들 모두 닫는 로직 필요
    }

    public override void ProcessCollisionEnter()
    {
        if (isOpened == false)
        {
            helper.OpenObjects(true);
            isOpened = true;
            releaseStack.Push(RequestClose);
        }
    }
}
