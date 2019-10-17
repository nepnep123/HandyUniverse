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

    public World venus;

    static public TestManager_v2 instance;
    private void Awake()
    {
        instance = GetComponent<TestManager_v2>();
    }
    bool isInit = false;
    private void LateUpdate()
    {
        TestManager_v2.instance.testia.text = GameManager.instance.hand.curObj.name;
        if (isInit == true) return;
        venus.gameObject.SetActive(true);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            GameManager.instance.masterBook.OpenPortal();
        }
    }
}
