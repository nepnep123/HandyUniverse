using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_TestCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var a = other.GetComponent<InteractableObject>();
        if(a != null)
        {
            a.ProcessCollisionEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var a = other.GetComponent<InteractableObject>();
        if (a != null)
        {
            a.ProcessCollisionExit();
        }
    }
}
