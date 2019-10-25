using System;
using UnityEngine;

public class PlanetControl : MonoBehaviour
{
    private float screenWidth;

    void Start()
    {
        screenWidth = Screen.width;
    }

    private void Update()
    {
        UpDownRot();
        LeftRightRot();
    }

    private void UpDownRot()
    {
        float upDownRotSpeed = 50 * Time.deltaTime;
        transform.Rotate(Vector3.left * upDownRotSpeed, Space.World);
    }

    private void LeftRightRot()
    {

#if UNITY_EDITOR
        PlanetLeftRightRot(Input.GetAxis("Horizontal"));
#endif

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).position.x < screenWidth / 2)
            {
                PlanetLeftRightRot(-1.0f);
            }

            if (Input.GetTouch(0).position.x > screenWidth / 2)
            {
                PlanetLeftRightRot(1.0f);
            }
        }
    }

    private void PlanetLeftRightRot(float planetLeftRightRotInput)
    {
        float leftRightRotSpeed = 100 * planetLeftRightRotInput * Time.deltaTime;
        transform.Rotate(Vector3.forward * leftRightRotSpeed, Space.World);
    }
}
