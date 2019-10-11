using System;
using UnityEngine;

public class KangWon_Test_MouseFollow : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    private float minimumX = -360f;
    private float maximumX = 360f;

    private float minimumY = -360f;
    private float maximumY = 360f;

    private float rotationY = 0F;

    private Camera mainCamera;

    private float zoomSize = 60;
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        DrawRay();
        MouseMove();
        Zoom();
    }

    private void DrawRay()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);
    }

    private void MouseMove()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityY, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }

    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (zoomSize > 20)
            {
                zoomSize -= 2;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (zoomSize < 60)
            {
                zoomSize += 2;
            }
        }

        mainCamera.fieldOfView = zoomSize;
    }
}
