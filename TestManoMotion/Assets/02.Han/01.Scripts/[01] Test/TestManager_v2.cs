using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestManager_v2 : MonoBehaviour
{
    public Text testia;
    public Text testiby;
    public Text testica;
    public Text testiden;

    static public TestManager_v2 instance;
    private void Awake()
    {
        instance = GetComponent<TestManager_v2>();
    }
}
