using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		//Debug.Log("main 카메라 position : " + maincam.position);


        if(Input.GetKeyDown(KeyCode.Q))
		{
			GameManager.instance.hand.mode.OnTriggeredClick();
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			GameManager.instance.hand.mode.OnTriggeredPick();
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			GameManager.instance.hand.mode.OnTriggeredDrop();
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			GameManager.instance.hand.mode.OnTriggeredGrab();
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
			GameManager.instance.hand.mode.OnTriggeredRelease();
		}
    }
}
