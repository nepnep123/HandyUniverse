using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKPlanetLeftRight : MonoBehaviour
{
    private void Update()
    {
        PlanetHandLeftRight(ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info);
    }

    private void PlanetHandLeftRight(TrackingInfo tracking_info)
    {
        float angle = Mathf.LerpAngle(transform.rotation.z, tracking_info.rotation, 0.8f);
        transform.eulerAngles = new Vector3(0, 0, -angle);
    }
}