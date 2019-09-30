using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class HandTrigger : MonoBehaviour
{
	private TrackableHit hit;        //레이케스트 결과값을 반환할 구조체
	private TrackableHitFlags flags; //레이케스트 레이어 마스크

	public GameObject potal;
	public static GameObject potalPrefab; //동적으로 생성된 포탈 
	public Transform camPos;

	private bool isCreate;

	public GameObject pointVisual; //스캔 오브젝트


	void Start()
	{
		flags = TrackableHitFlags.FeaturePointWithSurfaceNormal | TrackableHitFlags.PlaneWithinPolygon;
	}

	void Update()
	{
		if (Input.touchCount == 0) return;

		Touch touch = Input.GetTouch(0);
		if(touch.phase == TouchPhase.Began && !isCreate)
		{
			if (Frame.Raycast(touch.position.x, touch.position.y, flags, out hit))
			{
				isCreate = true; //한번 생성되면 더 생성 안됨.
				Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
				potalPrefab = Instantiate(potal, hit.Pose.position, hit.Pose.rotation, anchor.transform);
				Destroy(pointVisual); //더이상 바닥인식은 하지 않는다. 

				Vector3 cameraPosition = camPos.transform.position;
				cameraPosition.y = hit.Pose.position.y;
				potalPrefab.transform.LookAt(cameraPosition, potalPrefab.transform.up);
			}
		}

		//ManoGestureTrigger gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger;

		//if (gesture == ManoGestureTrigger.GRAB)
		//{
		//}
		//else if (gesture == ManoGestureTrigger.RELEASE)
		//{

		//	//Vector3 dir = manov._layer_background.transform.position - Camera.main.transform.position;
		//	//if (Frame.Raycast(transform.position, dir, out hit, 300f, flags))
		//	//{
		//	//	if (!isCreate)
		//	//	{
		//	//		Instantiate(obj, hit.Pose.position, Quaternion.identity);

		//	//		//한번 생성하고 안됨.
		//	//		isCreate = true;
		//	//	}
		//	//}
		//}
	}



}
