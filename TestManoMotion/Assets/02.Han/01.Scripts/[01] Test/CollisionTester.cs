using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTester : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var a = other.GetComponent<ICollidable>();
        if(a != null)
        {
            a.ProcessCollision();
        }
    }
}
