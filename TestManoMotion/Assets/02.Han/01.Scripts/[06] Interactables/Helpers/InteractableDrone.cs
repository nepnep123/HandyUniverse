using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDrone : InteractableObject
{
    Helper helper;
    public override void ProcessInit<T>(T obj)
    {
        if(obj is Helper)
        {
            helper = obj as Helper;
        }
    }
    public override void ProcessCollisionEnter()
    {
        helper.OpenObjects(true);
    }
    public override void ProcessCollisionExit()
    {
        helper.OpenObjects(false);
    }
}
