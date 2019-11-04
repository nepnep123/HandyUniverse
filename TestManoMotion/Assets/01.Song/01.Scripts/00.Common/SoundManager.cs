using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	public AudioSource soundPlayer;

	public AudioClip bookZone;
	public AudioClip openbookSound;
	public AudioClip closebookSound;
	public AudioClip nextpageSound;
	public AudioClip keySound;

    [Header("Venus Sound Pack")]
    public VenusSound_Scriptable venusSoundPack;
    [Header("Gesture Sound Pack")]
    public GestrueSound_Scriptable gestureSoundPAck;


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
