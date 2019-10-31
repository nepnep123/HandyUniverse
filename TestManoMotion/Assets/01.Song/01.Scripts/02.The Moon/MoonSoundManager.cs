using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSoundManager : MonoBehaviour
{
	#region 싱글톤
	public static MoonSoundManager instance;

	public static MoonSoundManager GetInstance()
	{
		if(instance == null)
		{
			instance = (MoonSoundManager)FindObjectOfType(typeof(MoonSoundManager));
			if(instance == null)
			{
				Debug.Log("Sound MGR 애러");
			}
		}
		return instance;
	}
	private void Awake()
	{
		if (instance != null && instance != this)
		{
			DestroyImmediate(gameObject);
		}
		else
		{
			instance = this;
		}

		AwakeAfter();
	}
	#endregion

	//백그라운드사운드는 함수로 실행 / 효과음은 해당 클립 가지고 실행
	public AudioClip clickSound;
	public AudioClip arrowTouchSound;
	public AudioClip loadingSound;
	public AudioClip handprintSound;
	public AudioClip teleportSound;

	public AudioSource sfxPlayer;
	AudioSource bgmPlayer;

	void AwakeAfter()
	{
		bgmPlayer = gameObject.GetComponent<AudioSource>();
		sfxPlayer = transform.GetChild(0).GetComponent<AudioSource>();
	}

	public void PlayBGM()
	{
		bgmPlayer.Play();
	}
	public void StopBGM()
	{
		bgmPlayer.Stop();
	}


}
