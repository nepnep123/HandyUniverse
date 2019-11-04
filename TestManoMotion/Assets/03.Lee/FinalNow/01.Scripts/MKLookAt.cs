using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKLookAt : MonoBehaviour
{
    private Transform camPos;

    private void OnEnable()
    {
        camPos = Camera.main.transform;
    }

    void Update()
    {
        Vector3 b = camPos.position - this.transform.position;
        Quaternion a = Quaternion.LookRotation(b);
        transform.localRotation = a;
    }
}