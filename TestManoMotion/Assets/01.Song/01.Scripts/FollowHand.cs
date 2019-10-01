using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		Movement(ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info);
	}

	//HandTrigger
	void Movement(TrackingInfo trackingInfo)
	{
		Vector3 normalizedPalmCenterPosition = trackingInfo.palm_center;
		float depth = trackingInfo.relative_depth;

		Vector3 relativePalmCenterPosition = ManoUtils.Instance.CalculateNewPosition(normalizedPalmCenterPosition, depth);

		float smoothingVariable = 0.85f;
		transform.position = Vector3.Lerp(transform.position, relativePalmCenterPosition, smoothingVariable);
	}
}
