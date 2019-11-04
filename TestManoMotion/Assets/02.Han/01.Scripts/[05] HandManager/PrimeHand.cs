using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeHand : MonoBehaviour
{
    public Mode mode;
    public KTutorialMode kTutorialMode;
    public EntryMode entryMode;
	public MoonMode moonMode;
    public VenusMode venusMode;

    public InteractableObject curObj;

    ManoClass preClass;
    ManoGestureContinuous preCon;
    ManoGestureTrigger preTri;


    private void Awake()
    {
        kTutorialMode = new KTutorialMode(this);
        entryMode = new EntryMode(this);
		moonMode = new MoonMode(this);
        venusMode = new VenusMode(this);
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
                        if (SoundManager.instance == null) Debug.Log("사운드매니저 인스턴스 널");
                        if (SoundManager.instance.soundPlayer == null) Debug.Log("사운드플레이어 널");
                        if (SoundManager.instance.gestureSoundPAck == null) Debug.Log("제스쳐팩 널");
                        if (SoundManager.instance.gestureSoundPAck.grab == null) Debug.Log("오디오파일 널");
                        SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.gestureSoundPAck.grab);
                        mode.OnTriggeredGrab();
                        preTri = ManoGestureTrigger.GRAB;
                    }
                }
                if(tri == ManoGestureTrigger.RELEASE)
                {
                    if(preTri != ManoGestureTrigger.RELEASE)
                    {
                        SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.gestureSoundPAck.release);
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
                        SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.gestureSoundPAck.pick);
                        mode.OnTriggeredPick();
                        preTri = ManoGestureTrigger.PICK;
                    }
                }
                if(tri == ManoGestureTrigger.DROP)
                {
                    if(preTri != ManoGestureTrigger.DROP)
                    {
                        SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.gestureSoundPAck.drop);
                        mode.OnTriggeredDrop();
                        preTri = ManoGestureTrigger.DROP;
                    }
                }
				if (tri == ManoGestureTrigger.CLICK)
				{ 
					if (preTri != ManoGestureTrigger.CLICK)
					{
                        SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.gestureSoundPAck.click);
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
