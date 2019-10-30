using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	private void Awake()
	{
		if (instance == null) instance = GetComponent<SoundManager>();
		else DestroyImmediate(instance);

		soundPlayer = gameObject.GetComponent<AudioSource>();
	}

	public AudioClip openbookSound;
	public AudioClip closebookSound;
	public AudioClip nextpageSound;

	public AudioSource soundPlayer;

	private void Start()
	{

	}

}
