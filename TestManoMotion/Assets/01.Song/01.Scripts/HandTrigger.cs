using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class HandTrigger : MonoBehaviour
{

	public GameObject potal;
	public static GameObject potalPrefab; //동적으로 생성된 포탈 
	public Transform camPos;

	private bool isCreate = false;

	public GameObject pointVisual; //스캔 오브젝트


	void Start()
	{
	}

	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("BOOK01"))
		{
			other.transform.gameObject.GetComponent<Outline>().OutlineWidth = 15;

			var a = other.GetComponent<ICollidable>();
			if(a != null)
				a.ProcessCollision();

			if (!isCreate)
			{
				//포탈은 한번만 생성
				isCreate = true;
				//충돌 시 책앞에 포탈 생성 
				Vector3 pos = new Vector3(other.transform.position.x, other.transform.position.y, 
								other.transform.position.z - 0.1f);
				potalPrefab = Instantiate(potal, pos, Quaternion.identity);
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("BOOK01"))
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
			if (canRelease)
			{
				//포탈 안으로 이동 
				StartCoroutine(StartIntoPotal());
			}
			
		}
	}

	public IEnumerator StartIntoPotal()
	{
		yield return new WaitForSeconds(2.0f);

		float timer = 0f;
		while (timer >= 2.0f)
		{
			potalPrefab.transform.position = Vector3.Lerp(potalPrefab.transform.position,
											camPos.transform.position, Time.deltaTime * 2.0f);
			timer += Time.deltaTime;
			yield return null;
		}
	}
}
