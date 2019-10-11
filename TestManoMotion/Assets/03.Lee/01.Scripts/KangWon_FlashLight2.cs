using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KangWon_FlashLight2 : MonoBehaviour
{
    public Light flashLight;

    [SerializeField]
    private float speed = 3f;

    private GameObject goFollow;
    private Vector3 vectOffset;
    private bool isActive;

    //public AudioSource audioSource;
    //public AudioClip soundFlashLightOn;
    //public AudioClip soundFlashLightOff;

    void Start()
    {
        goFollow = Camera.main.gameObject;
        vectOffset = transform.position - goFollow.transform.position;

        isActive = false;
        flashLight.enabled = false;
    }

    void Update()
    {
        transform.position = goFollow.transform.position + vectOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);

        LightOnOff();
    }

    private void LightOnOff()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isActive == false)
            {
                flashLight.enabled = true;
                isActive = true;

                //audioSource.PlayOneShot(soundFlashLightOn);
            }
            else if (isActive == true)
            {
                flashLight.enabled = false;
                isActive = false;

                //audioSource.PlayOneShot(soundFlashLightOff);
            }
        }
    }
}