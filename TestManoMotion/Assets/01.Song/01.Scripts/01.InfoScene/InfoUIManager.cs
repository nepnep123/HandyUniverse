using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfoUIManager : MonoBehaviour
{
	public AudioClip clip;

	private AudioSource player;

	public GameObject startInfo;
	public GameObject tutorialInfo;

	private TutorialInstuctor tutorial_Info;

	private int count = -1;

	private void Awake()
	{
		player = gameObject.GetComponent<AudioSource>();
		tutorial_Info = FindObjectOfType<TutorialInstuctor>();



		startInfo.SetActive(true);
		tutorialInfo.SetActive(false);
	}

	public void EnterScene()
	{
		player.PlayOneShot(clip);

		startInfo.SetActive(false);
		tutorialInfo.SetActive(true);
		for (int i = 0; i < tutorial_Info.tutorial_Canvas.Length; i++)
		{
			tutorial_Info.tutorial_Canvas[i].alpha = 0;
		}

		MainUICtrl(0);
	}

	public void ExitApplication()
	{
		player.PlayOneShot(clip);
		Application.Quit();
	}


	private void Update()
	{
		if(count == tutorial_Info.tutorial_Canvas.Length - 1)
		{
			tutorial_Info.pageCtrl.SetActive(false);
		}
	}
	public void MainUICtrl(int temp)
	{
		switch (temp)
		{
			//다음
			case 0:
				if (count >= tutorial_Info.tutorial_Canvas.Length)
				{
					Debug.Log("다음 페이지가 없습니다!!!");
					break;
				}
				else
				{
					++count;
					NextLevel(count);
					break;
				}
			//이전
			case 1:
				if (count == -1 || count == 0 )
				{
					Debug.Log("이전 페이지가 없습니다!!!");
					break;
				}
				else
				{
					--count;
					PreLevel(count);
					break;
				}
		}
	}
	public void NextLevel(int _count)
	{
		tutorial_Info.tutorial_Canvas[_count].alpha = 1;

		if (_count != 0)
		{
			tutorial_Info.tutorial_Canvas[_count - 1].alpha = 0;
		}
	}
	public void PreLevel(int _count)
	{
		tutorial_Info.tutorial_Canvas[_count].alpha = 1;

		if (_count != 0)
		{
			tutorial_Info.tutorial_Canvas[_count + 1].alpha = 0;
		}
	}

	public void StartMain()
	{
		SceneManager.LoadScene("SongMain");
	}
}
