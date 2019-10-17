using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MyAssets/PageInfo")]
public class PageInfo_Scriptable : ScriptableObject
{
	//사용자 카메라 위치에 생성될 맵	
    public World world;
	//마스터 책위에 생성될 미니 행성
	public Transform pagePlanet;

    public Texture2D leftTexture;
    [TextArea(15, 20)]
    public string rightDescribe;
}
