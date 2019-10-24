using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotationial : MonoBehaviour
{
    void Update() => transform.Rotate(Vector3.up, 45f * Time.deltaTime, Space.World);
}
