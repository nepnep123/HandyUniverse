using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WorldMaterial : MonoBehaviour
{
	ManoVisualization manov;
	public Material[] materials;

	void Awake()
	{
		manov = FindObjectOfType<ManoVisualization>();
	}

	void Start()
    {
		SetMaterials(false);
    }

	void SetMaterials(bool fullRender)
	{
		var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;

		foreach(var mat in materials)
		{
			mat.SetInt("_StencilFilterTest", (int)stencilTest);
		}
	}

	public void EnterPlanet(bool enter)
	{
		SetMaterials(enter);

		manov.Show_background_layer = !enter;
		manov._layer_background.gameObject.SetActive(!enter);
		manov._layer_background.enabled = !enter;
	}
}
