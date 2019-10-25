using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonUICtrl : MonoBehaviour
{
	public static MoonUICtrl instance;
	public GameObject infoImg;


	public GameObject neilInfo;
	public GameObject edwinInfo;
	public GameObject myInfo;

	public GameObject loading_particle;
	public GameObject printing_img;

	private string mission;

	private void Awake()
	{
		if (instance == null) instance = GetComponent<MoonUICtrl>();
		else Destroy(instance);
	}
	private void Start()
	{
		infoImg.SetActive(false);
		neilInfo.SetActive(false);
		edwinInfo.SetActive(false);
		myInfo.SetActive(false);
		loading_particle.SetActive(false);
		printing_img.SetActive(false);
	}

	private void OnEnable()
	{
		GameManager.instance.OnStartInfo += StartMoonInfo;
	}

	private void OnDisable()
	{
		GameManager.instance.OnStartInfo -= StartMoonInfo;
	}

	//5초동안 안내후 미션 안내 
	public IEnumerator AfterInfo()
	{
		yield return new WaitForSeconds(5.0f);
		infoImg.SetActive(false);

		mission = "첫번째 미션" + "\n" + "\n" 
			+ "달에는 대기가 존재하지 않아서 풍화작용이 일어나지" + "\n" 
			+ "않습니다. 그러므로, 우주인이 달에 남긴 발자국은" + "\n" 
			+ "사라지지 않습니다. " + "\n" 
			+ "이 행성에 우주인의 발자취가 존재합니다." + "\n"
			+ "우주인의 발자취를 찾으세요.";

		StartCoroutine(UIManager.instance.ShowMissionUI(mission));
		//3초뒤에 힌트 공개 
		yield return new WaitForSeconds(3.0f);
		MoonWorld.instance.ShowHint();
	}

	public void StartMoonInfo()
	{
		infoImg.SetActive(true);
		StartCoroutine(AfterInfo());
	}

	//hint를 클릭했을때 보여지는 UI
	public void ShowPicture(int i)
	{
		switch (i)
		{
			case 1:
				neilInfo.SetActive(true);
				neilInfo.GetComponent<Animator>().SetBool("IsShow", true);
				break;
			case 2:
				edwinInfo.SetActive(true);
				edwinInfo.GetComponent<Animator>().SetBool("IsShow", true);
				break;
			case 3:
				myInfo.SetActive(true);

				break;
		}
	}
	private string msg;
	public IEnumerator LoadingAndPrint()
	{
		//미션 클리어시 5초동안 로딩... 후 바닥에 손바닥 생성
		loading_particle.SetActive(true);
		yield return new WaitForSeconds(5.0f);
		loading_particle.SetActive(false);
		printing_img.SetActive(true);

		myInfo.SetActive(false);
		msg = "미션 클리어 !!!" + "\n" + "\n"
			+ "당신은 달행성에 미션을 완수 하였습니다. " + "\n"
			+ "3초 뒤에 GRAB 제스처를 사용해 행성을 탈출합니다. ";
		StartCoroutine(UIManager.instance.ShowMissionUI(msg));

		//문 모드이면 모든 퀘스트가 끝나고 Grab제스처로 포탈을 생성한다. 
		GameManager.instance.hand.moonMode.isFinished = true;

	}

}
