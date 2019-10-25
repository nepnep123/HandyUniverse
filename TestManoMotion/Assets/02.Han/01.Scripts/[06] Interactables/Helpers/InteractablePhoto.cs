using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePhoto : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        //바라보기
        transform.LookAt(target);
        Quaternion rot = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.rotation = rot;
    }
}
