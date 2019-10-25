using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonWorld : World
{
	public static MoonWorld instance;

	public GameObject[] hint_objects;

	public Transform campos;
	public Transform start_Pos;
	public Transform first_Pos;
	public Transform second_Pos;

	public GameObject exitPotal;

	private void Awake()
	{
		if (instance == null) instance = GetComponent<MoonWorld>();
		else Destroy(this);
	}

	public override void InitWorld()
	{
		GameManager.instance.hand.mode = GameManager.instance.hand.moonMode;
		campos.position = start_Pos.position;
	}

	private void OnEnable()
	{
		//메인카메라의 부모 가져오기 
		campos = Camera.main.transform.parent.gameObject.transform;

		var a = GetComponentsInChildren<InteractableMoonObject>(true);
		Debug.Log(a.Length);
		hint_objects = new GameObject[a.Length];

		for (int i = 0; i < hint_objects.Length; i++)
		{
			hint_objects[i] = a[i].gameObject;
			hint_objects[i].SetActive(false);
		}
	}

	private void OnDisable()
	{

	}


	public void ShowHint()
	{
		for (int i = 0; i < hint_objects.Length; i++)
		{
			hint_objects[i].SetActive(true);
		}
	}


	public void GoFirstPos() => StartCoroutine(MovePlayerCP01());
	public IEnumerator MovePlayerCP01()
	{
		
		//이동하기전에 이전에 있는 neilInfo는 OFF
		MoonUICtrl.instance.neilInfo.SetActive(false);

		yield return new WaitForSeconds(3.0f);

		while (true)
		{
			campos.position = Vector3.MoveTowards(campos.position, first_Pos.position, Time.deltaTime * 1f);
			campos.LookAt(first_Pos);

			if (campos.position == first_Pos.position)
			{
				campos.rotation = first_Pos.rotation;
				yield break;
			}
			yield return null;
		}
	}
	

	public void GoSecondPos() => StartCoroutine(MovePlayerCP02());
	public IEnumerator MovePlayerCP02()
	{
		//이동하기전에 이전에 있는 edwinInfo는 OFF
		MoonUICtrl.instance.edwinInfo.SetActive(false);

		yield return new WaitForSeconds(3.0f);

		while (true)
		{
			campos.position = Vector3.MoveTowards(campos.position, second_Pos.position, Time.deltaTime * 1f);
			campos.LookAt(second_Pos);

			if (campos.position == second_Pos.position)
			{
				campos.rotation = second_Pos.rotation;
				//마지막 UI 표시 
				MoonUICtrl.instance.ShowPicture(3);
				yield break;
			}

			yield return null;
		}
	}
	public GameObject potalPrefab;

	//나가는 포탈 생성
	public void AbleExitPotal()
	{
		Vector3 ablePos = campos.position + new Vector3(0, 0, 0.5f);

		potalPrefab = Instantiate(exitPotal, ablePos, Quaternion.identity);
	}
	
}
