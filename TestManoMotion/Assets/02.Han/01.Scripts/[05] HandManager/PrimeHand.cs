using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeHand : MonoBehaviour
{
    public Mode mode;
    public EntryMode entryMode;
	public MoonMode moonMode;


    public InteractableObject curObj;

    ManoClass preClass;
    ManoGestureContinuous preCon;
    ManoGestureTrigger preTri;

    private void Awake()
    {
        entryMode = new EntryMode(this);
		moonMode = new MoonMode(this);
        mode = entryMode;
    }

    //TODO : 안전장치 필요
    void FixedUpdate()
    {
        if(curObj != null)
        {
            //TestManager_v2.instance.testica.text = curObj.name;
            float dist = Vector3.Distance(this.transform.position, curObj.transform.position);
            if (dist >= 4f) curObj = null;
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
				if (tri == ManoGestureTrigger.CLICK)
				{ 
					if (preTri != ManoGestureTrigger.CLICK)
					{
						mode.OnTriggeredClick();
						preTri = ManoGestureTrigger.CLICK;
					}
				}
				break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
		var inter = other.GetComponent<InteractableObject>();
        if(inter != null)
        {
			if (curObj != null) curObj.ProcessCollisionExit();
			curObj = inter;
            curObj.ProcessCollisionEnter();
        }
	}


	private void OnTriggerExit(Collider other)
    {
        if(curObj!=null)
        {
            curObj.ProcessCollisionExit();
			/////////////
        }
    }
}
