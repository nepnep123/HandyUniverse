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

	public TutorialInstuctor tutorial_Info;

	private int count = -1;

	private void Awake()
	{
		player = gameObject.GetComponent<AudioSource>();

		for (int i = 0; i < tutorial_Info.tutorial_Canvas.Length; i++)
		{
			tutorial_Info.tutorial_Canvas[i].alpha = 0;
		}
	}

	public void EnterScene()
	{
		player.PlayOneShot(clip);
		SceneManager.LoadScene("SongMain");
		//startInfo.SetActive(false);
		//tutorialInfo.SetActive(true);
		//MainUICtrl(0);
	}

	public void ExitApplication()
	{
		player.PlayOneShot(clip);
		Application.Quit();
	}
	


	public void MainUICtrl(int temp)
	{
		switch (temp)
		{
			//다음
			case 0:
				++count;
				StartCoroutine(NextLevel(count));
				break;
			//이전
			case 1:
				if (count == 0 || count == -1)
				{
					Debug.Log("이전 페이지가 없습니다!!!");
					break;
				}
				else
				{
					--count;
					StartCoroutine(NextLevel(count));
					break;
				}
		}
	}
	public IEnumerator NextLevel(int _count)
	{
		float timer = 0;

		while (timer < 1)
		{
			timer += Time.deltaTime;
			tutorial_Info.tutorial_Canvas[_count].alpha = timer;
			yield return null;
		}

		yield return new WaitForSeconds(3f);
		timer = 1;

		if (_count != 0)
		{
			while (timer > 0)
			{
				timer -= Time.deltaTime;
				tutorial_Info.tutorial_Canvas[_count - 1].alpha = timer;
				yield return null;
			}
		}
	}

	//SceneManager.LoadScene("SongMain");

	//public IEnumerator PreLevel()
	//{
	//	float timer = 0;
	//	for (int i = 0; i < tutorial_Info.tutorial_Canvas.Length; i++)
	//	{
	//		while (timer < 1)
	//		{
	//			timer += Time.deltaTime;
	//			tutorial_Info.tutorial_Canvas[i].alpha = timer;
	//			yield return null;
	//		}
	//		yield return new WaitForSeconds(3f);
	//	}
	//}
}
