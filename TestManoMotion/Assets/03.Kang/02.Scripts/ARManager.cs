using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class ARManager : MonoBehaviour
{
    private TrackableHit hit;
    private TrackableHitFlags flags;

    public GameObject model;
    public static GameObject modelModel;
    public GameObject skyboxCamera;
    public GameObject quad;
    public GameObject planeVisualizer;

    private bool isCreate;

    void Awake()
    {
        skyboxCamera.SetActive(false);
        quad.SetActive(false);
    }

    void Start()
    {
        flags = TrackableHitFlags.FeaturePointWithSurfaceNormal | TrackableHitFlags.PlaneWithinPolygon;
    }

    void Update()
    {
        MakeModel();
    }

    private void MakeModel()
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began && Input.touchCount > 0 && !isCreate)
        {           
            if (Frame.Raycast(touch.position.x, touch.position.y, flags, out hit))
            {
                isCreate = true;

                Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
                modelModel = Instantiate(model, hit.Pose.position, hit.Pose.rotation, anchor.transform);

                skyboxCamera.SetActive(true);
                quad.SetActive(true);
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