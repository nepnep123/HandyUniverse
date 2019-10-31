using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abcdefghijkTest : MonoBehaviour
{

    void Update()
    {
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
            GameManager.instance.hand.mode.OnTriggeredGrab();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.hand.mode.OnTriggeredRelease();
        }
    }
}
