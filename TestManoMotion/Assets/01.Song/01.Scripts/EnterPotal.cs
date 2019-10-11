using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnterPotal : MonoBehaviour
{
	ManoVisualization manov;
	public Material[] materials;
	public Transform device;
	public GameObject POTAL01_Eagle;

	//World 안에 있는 UI
	public GameObject UITEST;

	private bool hasCollided = false;

	void Awake()
	{
		device = Camera.main.transform;
		manov = FindObjectOfType<ManoVisualization>();
	}

	// Start is called before the first frame update
	void Start()
    {
		SetMaterials(false);
		UITEST.SetActive(false);
    }

	void SetMaterials(bool fullRender)
	{
		var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;

		foreach(var mat in materials)
		{
			mat.SetInt("_StencilFilterTest", (int)stencilTest);
		}
	}

	public static GameObject eagleprefab;

	public void EnterPlanet()
	{
		hasCollided = !hasCollided;

		SetMaterials(hasCollided);

		manov.Show_background_layer = !hasCollided;
		manov._layer_background.gameObject.SetActive(!hasCollided);
		manov._layer_background.enabled = !hasCollided;

		UITEST.SetActive(hasCollided);
		eagleprefab = Instantiate(POTAL01_Eagle);

		iTween.MoveTo(eagleprefab, ITweenManager.ht1);
	}
}
