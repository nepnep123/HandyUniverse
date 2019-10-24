using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// void : 아무것도 반환하지 않아.
// delegate 정의 순서 1번 째 : 
public delegate void VoidBoolNotier(bool booleana);
public delegate void VoidNotier();
public enum LR { Left, Right}
public enum VenusPos { Lakshmi, Maxwell, Venus}

[System.Serializable]
public struct RawImages
{
    public RawImage[] photo;
}

public interface ICollidable
{
    void InitCollData<T>(T book);
    void ProcessCollision();
    void ProcessMethod();
}

