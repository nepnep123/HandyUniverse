using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSelfDestory : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3f);
    }
}
