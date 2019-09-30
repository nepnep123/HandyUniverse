using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PotalTest : MonoBehaviour
{
	ManoVisualization manov;
	public Material[] materials;
	public Transform device;

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
    }

	void SetMaterials(bool fullRender)
	{
		var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;

		foreach(var mat in materials)
		{
			mat.SetInt("_StencilFilterTest", (int)stencilTest);
			Debug.Log(mat);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform != device) return;

		//hasCollided = true;
		//isEnterPotal = true;
		hasCollided = !hasCollided;

		SetMaterials(hasCollided);

		manov.Show_background_layer = hasCollided;
		manov._layer_background.gameObject.SetActive(hasCollided);
		manov._layer_background.enabled = hasCollided;
	}

	void Update()
    {
        
    }
}
