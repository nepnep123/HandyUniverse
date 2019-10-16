using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusSfx : MonoBehaviour
{
    AudioSource audi;

    private void OnEnable()
    {
        InitVenusSounds();
    }

    public void InitVenusSounds()
    {
        audi = GetComponent<AudioSource>();
    }

}
