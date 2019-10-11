using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHandler : MonoBehaviour
{
    Book curBook;
    // Update is called once per frame
    void Update()
    {
        /*
        if(Vector3.Distance(curBook.transform.position, this.transform.position) > 3f)
        {
            curBook.CloseBook();
            curBook.GetComponent<BookController>().isPicked = false;
            curBook.transform.parent = null;
            curBook = null;
        }
        var a = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        if(a.mano_gesture_trigger == ManoGestureTrigger.RELEASE)
        {
            curBook.transform.parent = null;
            curBook.GetComponent<BookController>().isPicked = false;
            if(curBook.WorldInfo.isPortalOpened == false)
                curBook.OpenPortal();
            curBook = null;
        }
        else if (a.mano_gesture_trigger == ManoGestureTrigger.PICK)
        {
            curBook.transform.parent = transform;
            curBook.GetComponent<BookController>().isPicked = true;
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        var a = other.GetComponent<ICollidable>();
        if (a != null)
        {
            //curBook = a.book;
            a.ProcessCollision();
        }
    }
}
