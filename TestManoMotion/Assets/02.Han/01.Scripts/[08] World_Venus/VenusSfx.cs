using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusSfx : MonoBehaviour
{
    AudioSource audi;
    public VenusSound_Scriptable sfxPack;
    public void InitVenusSounds()
    {
        audi = GetComponent<AudioSource>();
    }

}
