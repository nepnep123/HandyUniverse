using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class HandTrigger : MonoBehaviour
{

	//public GameObject potal;
	//public static GameObject potalPrefab; //동적으로 생성된 포탈 
	public Transform camPos;

	private bool isCreate = false;

	public GameObject pointVisual; //스캔 오브젝트

	Book curGrabbingBook;

	void Start()
	{
	}

	void Update()
	{
		HandGesture();
	}

	void OnTriggerEnter(Collider other)
	{
		var a = other.GetComponent<Book>();
		if(a != null)
		{
			curGrabbingBook = a;

			//책에 충돌하면 포탈이 열리고 책 아웃라인 생성
			a.WorldInfo.InToThePortalWorld(camPos.position + camPos.forward * 0.8f);
			other.transform.gameObject.GetComponent<Outline>().OutlineWidth = 15;
		}
	}
	void OnTriggerExit(Collider other)
	{
		var a = other.GetComponent<Book>();
		if (a != null)
		{
			other.transform.gameObject.GetComponent<Outline>().OutlineWidth = 0;
		}

	}

	private bool canRelease;

	private void HandGesture()
	{
		ManoGestureTrigger gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger;

		if (gesture == ManoGestureTrigger.GRAB)
		{
			canRelease = true;
		}
		else if (gesture == ManoGestureTrigger.RELEASE)
		{
			
		}
	}
}
