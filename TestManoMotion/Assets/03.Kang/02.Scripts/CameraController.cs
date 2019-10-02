using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        float rot = 0;
        if (Input.GetKey(KeyCode.Q))
            rot -= 1;
        if (Input.GetKey(KeyCode.E))
            rot += 1;
        transform.Rotate(0, rot, 0);
    }
}
