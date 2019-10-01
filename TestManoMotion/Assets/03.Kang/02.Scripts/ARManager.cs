using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using System;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class ARManager : MonoBehaviour
{
    public GameObject model;
    public GameObject skyboxCamera;
    public GameObject quad;
    public GameObject planeVisualizer;

    void Awake()
    {
        skyboxCamera.SetActive(false);
        quad.SetActive(false);
    }

    void Update()
    {
        MakeModel();
    }

    private void MakeModel()
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began && Input.touchCount > 0)
        {
            TrackableHit hit;
            TrackableHitFlags flags = TrackableHitFlags.FeaturePointWithSurfaceNormal | TrackableHitFlags.PlaneWithinPolygon;

            if (Frame.Raycast(touch.position.x, touch.position.y, flags, out hit))
            {
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                GameObject modelModel = Instantiate(model, hit.Pose.position, hit.Pose.rotation, anchor.transform);

                skyboxCamera.SetActive(true);
                quad.SetActive(true);

                //Destroy(planeVisualizer); // 더 이상 바닥인식은 NO
            }
        }
    }
}
//private void MakeSomthing()
//{
//    Touch touch = Input.GetTouch(0);
//    touchPosition = touch.position;

//    if (Input.touchCount > 0 && touch.phase == TouchPhase.Began)
//    {
//        if (raycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
//        {
//            Pose hitPose = hits[0].pose;
//            GameObject modelModel = Instantiate(model, hitPose.position, hitPose.rotation);
//        }
//    }
//}