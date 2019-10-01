using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct RawImages
{
    public RawImage[] photo;
}

public interface ICollidable
{
    void InitCollData(Book book);
    void ProcessCollision();
}

public interface IPortalFolder
{

}