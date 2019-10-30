using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MoonUICtrl : MonoBehaviour
{
	public static MoonUICtrl instance;
	public GameObject infoImg;
	private Text infotxt;
	public GameObject introVideo;
	private VideoPlayer video;


	public GameObject neilInfo;
	public GameObject edwinInfo;
	public GameObject myInfo;
	public GameObject exitInfo;

	public GameObject loading_particle;
	public GameObject printing_img;

	private string mission;

	private void Awake()
	{
		if (instance == null) instance = GetComponent<MoonUICtrl>();
		else Destroy(instance);

		infotxt = infoImg.GetComponentInChildren<Text>();
		video = introVideo.GetComponent<VideoPlayer>();
	}
	private void Start()
	{
		infoImg.SetActive(false);
		introVideo.SetActive(false);

		neilInfo.SetActive(false);
		edwinInfo.SetActive(false);
		myInfo.SetActive(false);
		exitInfo.SetActive(false);

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

	public IEnumerator AfterInfo()
	{
		yield return new WaitForSeconds(5.0f);
		infotxt.text = "아폴로 11호는" + "<color=red>" + "1969년 7월 16일 13시 32분 " + "</color>" + "\n"
			+ "UTC에 플로리다 주 케네디 우주 센터에서 새턴 5호 로켓으로 발사 되었다. " + "\n\n"
			+ "<color=red>" + "7월 20일 20시 17분 " + "</color>" + "UTC에 달착륙선이 달의 표면에 착륙했다. " + "\n"
			+ "선장은 " + "<color=red>" + "닐 암스트롱 중위, " + "</color>" +
			"<color=red>" + "조종사 버즈 올드린" + "</color>" + "이었다.";
		yield return new WaitForSeconds(10.0f);
		infoImg.SetActive(false);
		
		//20초 짜리 동영상 진행.
		introVideo.SetActive(true);
		video.Play();
		yield return new WaitForSeconds(20.0f);
		video.Stop();
		introVideo.SetActive(false);

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
		MoonWorld.instance.finalHint.SetActive(false);
	}

	public void StartMoonInfo()
	{
		infoImg.SetActive(true);
		MoonSoundManager.instance.PlayBGM();
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
		MoonSoundManager.instance.sfxPlayer.PlayOneShot(MoonSoundManager.instance.handprintSound);
		yield return new WaitForSeconds(2.0f);
		//미션 클리어시 5초동안 로딩... 후 바닥에 손바닥 생성
		loading_particle.SetActive(true);
		MoonSoundManager.instance.sfxPlayer.PlayOneShot(MoonSoundManager.instance.loadingSound);
		yield return new WaitForSeconds(5.0f);
		loading_particle.SetActive(false);

		printing_img.SetActive(true);

		myInfo.SetActive(false);
		msg = "미션 클리어 !!!" + "\n" + "\n"
			+ "당신은 달행성에 미션을 완수 하였습니다. " + "\n"
			+ "3초 뒤에 Click 제스처를 사용해 행성을 탈출합니다. ";
		StartCoroutine(UIManager.instance.ShowMissionUI(msg));

		MoonWorld.instance.finalHint.SetActive(true);
	}

}
