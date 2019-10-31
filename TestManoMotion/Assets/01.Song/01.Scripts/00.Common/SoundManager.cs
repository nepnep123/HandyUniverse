using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;


	public AudioClip bookZone;
	public AudioClip openbookSound;
	public AudioClip closebookSound;
	public AudioClip nextpageSound;


	public AudioSource soundPlayer;

	private void Awake()
	{
		if (instance == null) instance = GetComponent<SoundManager>();
		else DestroyImmediate(instance);

		soundPlayer = gameObject.GetComponent<AudioSource>();
	}

	private void Start()
	{
		StartBackGroundSound();
	}

	public void StartBackGroundSound()
	{
		soundPlayer.Play();
	}

	public void StopBackGroundSound()
	{
		soundPlayer.Stop();
	}


}
