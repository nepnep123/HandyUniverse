using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate Texture2D RETexture2D();

[System.Serializable]
public struct RawImages
{
    public RawImage[] photo;
}