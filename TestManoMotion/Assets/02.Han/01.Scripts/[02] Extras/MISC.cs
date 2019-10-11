using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void VoidBoolNotier(bool booleana);
public delegate void VoidNotier();
public enum LR { Left, Right}

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

