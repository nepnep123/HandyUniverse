using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCoinRotate : MonoBehaviour
{
    private float speedRot = 200;

    private void Update()
    {
        float rotate = speedRot * Time.deltaTime;
        transform.Rotate(Vector3.up * rotate);
    }
}