using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrinity : InteractableObject
{
    public ParticleSystem childicle;
    public InteractableDrone drone;
    protected Helper helper;
    public bool isPicked = false;

    public override void ProcessInit<T>(T obj)
    {
        childicle.gameObject.SetActive(false);
        if (obj is Helper)
            helper = obj as Helper;
    }

    public override void ProcessGrab()
    {
        isPicked = true;
    }

    public override void ProcessCollisionEnter()
    {
        helper.AllDeselect();
        SelectObj(true);
    }

    public void SelectObj(bool booleana)
    {
        childicle.gameObject.SetActive(booleana);
    }

    public virtual void CloseObjs() {
        if (helper.drone.releaseStack.Count > 1)
            helper.drone.ReturnBack();
    }
}
