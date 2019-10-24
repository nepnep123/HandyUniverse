//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleARCore;
//using System;

//public class ARManager : MonoBehaviour
//{
//    public GameObject planet;

//    private bool lockIs = false;

//    private void Start()
//    {
//        EnemySpawner.instance.enabled = false;
//    }

//    private void Update()
//    {
//        UpdateMakePlanet();
//    }

//    private void UpdateMakePlanet()
//    {
//        if (lockIs)
//        {

//            return;
//        }

//        Touch touch = Input.GetTouch(0);

//        if (Input.touchCount > 0 && touch.phase == TouchPhase.Began)
//        {
//            TrackableHit hit;
//            TrackableHitFlags flags = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.FeaturePointWithSurfaceNormal;

//            if (Frame.Raycast(touch.position.x, touch.position.y, flags, out hit))
//            {
//                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

//                var plnetObj = Instantiate(planet, hit.Pose.position, hit.Pose.rotation, anchor.transform);

//                EnemySpawner.instance.enabled = true;

//                lockIs = true;
//            }
//        }
//    }
//}