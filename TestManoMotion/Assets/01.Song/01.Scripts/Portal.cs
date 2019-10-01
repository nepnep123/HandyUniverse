using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class Portal : MonoBehaviour
{
	ManoVisualization manov;

	public Material[] materials;

	public Transform device;

    bool wasInFront;
    bool inOtherWorld;

    bool hasCollided;

    void Awake()
    {
        device = Camera.main.transform;
		manov = FindObjectOfType<ManoVisualization>();
	}

    void Start()
    {
        SetMaterials(false);
	}

	void Update()
	{
		WhileCameraColliding();
	}

	void SetMaterials(bool fullRender)
    {
        var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;

		foreach (var mat in materials)
		{
			mat.SetInt("_StencilFilterTest", (int)stencilTest);
			Debug.Log(mat);
		}
		
	}

    //문을 통과 했는지 여부를 알려주는 기능
    bool GetIsInFront()
    {
        Vector3 worldPos = device.position + device.forward * Camera.main.nearClipPlane;

        Vector3 pos = transform.InverseTransformPoint(worldPos);
        return pos.z >= 0 ? true : false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform != device)
			return;
		wasInFront = GetIsInFront();

		Debug.Log("들어갔을때 상태 :  " + wasInFront);
		hasCollided = true;


		manov.Show_background_layer = false;
		manov._layer_background.gameObject.SetActive(false);
		manov._layer_background.enabled = false;

	}

    void OnTriggerExit(Collider other)
    {

        if (other.transform != device)
			return;	
		hasCollided = false;

		manov.Show_background_layer = true;
		manov._layer_background.gameObject.SetActive(true);
		manov._layer_background.enabled = true;

	}

    void WhileCameraColliding()
    {
        if (!hasCollided)
        {
            return;
        }

        bool isInFront = GetIsInFront();

        if (isInFront && !wasInFront || (wasInFront && !isInFront))
        {
            inOtherWorld = !inOtherWorld;
            SetMaterials(inOtherWorld);
        }
        wasInFront = isInFront;
		Debug.Log("나왓을때 상태 :  " + wasInFront);
	}

    void OnDestroy()
    {
        SetMaterials(true);
    }

}
