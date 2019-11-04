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
 
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
			GameManager.instance.hand.curObj.ProcessRelease();
		}
		if (Input.GetKeyDown(KeyCode.Y))
		{
			GameManager.instance.hand.mode.OnTriggeredGrab();
		}
        if (Input.GetKeyDown(KeyCode.A))
        {
            SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.gestureSoundPAck.click);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.gestureSoundPAck.grab);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameManager.instance.hand.mode.OnTriggeredClick();
        }
    }
}
