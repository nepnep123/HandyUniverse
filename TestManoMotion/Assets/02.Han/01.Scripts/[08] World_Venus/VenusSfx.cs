using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusSfx : MonoBehaviour
{
    public static VenusSfx instance;

    AudioSource audi;
    public VenusSound_Scriptable sfxPack;
    public AudioClip venusDroneTouch;
    public void InitVenusSounds()
    {
        instance = GetComponent<VenusSfx>();
        audi = GetComponent<AudioSource>();
    }

}
