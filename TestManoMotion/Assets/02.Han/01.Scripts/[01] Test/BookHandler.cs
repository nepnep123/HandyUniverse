using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHandler : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var a = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        if(a.mano_gesture_trigger == ManoGestureTrigger.GRAB)
        {
            TestManager.instance.book.CloseBook();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var a = other.GetComponent<ICollidable>();
        if (a != null)
        {
            a.ProcessCollision();
        }
    }
}
