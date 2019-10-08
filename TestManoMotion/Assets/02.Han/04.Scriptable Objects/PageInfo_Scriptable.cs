using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MyAssets/PageInfo")]
public class PageInfo_Scriptable : ScriptableObject
{
    public World world;
    public Texture2D leftTexture;
    [TextArea(15, 20)]
    public string rightDescribe;
}
