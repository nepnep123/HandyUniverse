using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeHand : MonoBehaviour
{
    public Mode mode;
    EntryMode entryMode;

    public GameObject curObj;

    ManoClass preClass;
    ManoGestureContinuous preCon;
    ManoGestureTrigger preTri;

    private void Awake()
    {
        entryMode = new EntryMode(this);
        mode = entryMode;
    }

    //TODO : 안전장치 필요
    void FixedUpdate()
    {
        if(curObj != null)
        {
            float dist = Vector3.Distance(this.transform.position, curObj.transform.position);
            if (dist >= 2f) curObj = null;
        }
        ManoClass cla = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_class;
        //ManoGestureContinuous con = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous;
        ManoGestureTrigger tri = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger;

        switch(cla)
        {
            case ManoClass.GRAB_GESTURE_FAMILY:
                if(tri == ManoGestureTrigger.GRAB)
                {
                    if(preTri != ManoGestureTrigger.GRAB)
                    {
                        mode.OnTriggeredGrab();
                        preTri = ManoGestureTrigger.GRAB;
                    }
                }
                if(tri == ManoGestureTrigger.RELEASE)
                {
                    if(preTri != ManoGestureTrigger.RELEASE)
                    {
                        mode.OnTriggeredRelease();
                        preTri = ManoGestureTrigger.RELEASE;
                    }
                }
                break;
            case ManoClass.PINCH_GESTURE_FAMILY:
                if(tri == ManoGestureTrigger.PICK)
                {
                    if(preTri != ManoGestureTrigger.PICK)
                    {
                        mode.OnTriggeredPick();
                        preTri = ManoGestureTrigger.PICK;
                    }
                }
                if(tri == ManoGestureTrigger.DROP)
                {
                    if(preTri != ManoGestureTrigger.DROP)
                    {
                        mode.OnTriggeredDrop();
                        preTri = ManoGestureTrigger.DROP;
                    }
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var col = other.GetComponent<ICollidable>();
        if(col != null)
        {
            curObj = other.gameObject;
            mode.OnHandCollideEnter(col);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var col = other.GetComponent<ICollidable>();
        mode.OnHandCollideEnter(col); //null 검사 필요
    }
}
