using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoUIManager : MonoBehaviour
{
	public AudioClip clip;

	private AudioSource player;


	private void Awake()
	{
		player = gameObject.GetComponent<AudioSource>();
	}

	public void EnterScene()
	{
		player.PlayOneShot(clip);
		SceneManager.LoadScene("SongMain");
	}

	public void ExitApplication()
	{
		player.PlayOneShot(clip);
		Application.Quit();
	}


}
