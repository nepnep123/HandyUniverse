using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonUICtrl : MonoBehaviour
{

	private void Start()
	{
		StartCoroutine(UIManager.instance.ShowInfoUI("씨발러마"));
	}


}
