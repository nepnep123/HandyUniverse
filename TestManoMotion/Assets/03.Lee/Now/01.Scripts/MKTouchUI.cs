using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKTouchUI : MonoBehaviour
{
    private GameObject panel;

    private RaycastHit hit;
    private int maskWater;

    private bool isOk = false;

    private void Start()
    {
        maskWater = LayerMask.GetMask("Water");

        panel = GameObject.Find("asdasdasd");
        panel.SetActive(false);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hit, 1000, maskWater))
            {
                if (Input.touchCount == 1)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (isOk == false)
                        {
                            Debug.Log(123123);
                            panel.SetActive(true);
                            isOk = true;
                        }
                        else
                        {
                            Debug.Log(161611616);
                            panel.SetActive(false);
                            isOk = false;
                        }
                    }
                }
            }
        }
    }
}
                    