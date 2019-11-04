using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrinity : InteractableObject
{
    public ParticleSystem childicle;
    public InteractableDrone drone;
    public Helper helper;

    public override void ProcessInit<T>(T obj)
    {
        childicle.gameObject.SetActive(false);
        if (obj is Helper)
            helper = obj as Helper;
    }

    public override void ProcessCollisionEnter()
    {
        SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.venusSoundPack.venusDronePick);
        helper.AllDeselect();
        SelectObj(true);
    }

    public void SelectObj(bool booleana)
    {
        childicle.gameObject.SetActive(booleana);
    }
}
