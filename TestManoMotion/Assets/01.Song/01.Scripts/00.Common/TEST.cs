﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
	//private Transform maincam;
    // Start is called before the first frame update
    void Start()
    {
		//maincam = Camera.main.transform.parent.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
		//Debug.Log("main 카메라 position : " + maincam.position);


        if(Input.GetKeyDown(KeyCode.Q))
		{
			GameManager.instance.hand.curObj.ProcessClick();
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			GameManager.instance.hand.curObj.ProcessPick();
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			GameManager.instance.hand.curObj.ProcessDrop();
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			GameManager.instance.hand.curObj.ProcessGrab();
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
			GameManager.instance.hand.curObj.ProcessRelease();
		}
		if (Input.GetKeyDown(KeyCode.Y))
		{
			GameManager.instance.hand.mode.OnTriggeredGrab();
		}
	}
}