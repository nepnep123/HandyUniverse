using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_TestCollision : MonoBehaviour
{
    public InteractableObject curObj;

    private void OnTriggerEnter(Collider other)
    {
        var a = other.GetComponent<InteractableObject>();
        if(a != null)
        {
            curObj = a;
            curObj.ProcessCollisionEnter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (curObj != null)
        {
            curObj.ProcessCollisionExit();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            curObj.ProcessPick();
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            curObj.ProcessRelease();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<InteractableDrone>().ReturnBack();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            curObj.ProcessClick();
        }
    }
}
