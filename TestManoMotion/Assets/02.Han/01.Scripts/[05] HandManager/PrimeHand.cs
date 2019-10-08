using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeHand : MonoBehaviour
{
    public Mode mode;
    EntryMode entryMode;

    private void Awake()
    {
        entryMode = new EntryMode(this);
        mode = entryMode;
    }

    //TODO : 안전장치 필요
    void FixedUpdate()
    {
        ManoGestureTrigger ges = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger;
        switch(ges)
        {
            case ManoGestureTrigger.CLICK:
                mode.OnTriggeredClick();
                break;
            case ManoGestureTrigger.RELEASE:
                mode.OnTriggeredRelease();
                break;
            case ManoGestureTrigger.GRAB:
                mode.OnTriggeredGrab();
                break;
            case ManoGestureTrigger.PICK:
                mode.OnTriggeredPick();
                break;
            case ManoGestureTrigger.DROP:
                mode.OnTriggeredDrop();
                break;
        }
    }
}
