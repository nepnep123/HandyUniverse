using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KangWon_MissionCheck : MonoBehaviour
{
    //#region 드론 정찰 관측자
    //DroneInternalCam droneInternalCam;
    //bool[] isDetecteds;
    //public VoidIntNotifier OnObserveComplete;
    //#endregion

    //#region 수송 관측자
    //CheckPoint checkPoint;
    //#endregion

    //#region 전체 미션 관측자
    //public VoidNotifier OnAllMissionComplete;
    //#endregion

    //public bool[] areCompleted = new bool[2];

    //// Start is called before the first frame update
    //void Awake()
    //{
    //    InitializeMissionCatcher();
    //}

    //private void OnEnable()
    //{
    //    droneInternalCam.OnDetectionUpdate += DroneCamSubscriber;
    //    //OnDropBox가 직접 미션 성공여부를 전달해줄 것이다.
    //    checkPoint.OnDropBox += MissionSubscriber;
    //    //테스트 코드
    //    //DroneCamSubscriber는 전부 정찰되었는지를 판단해줄 것이고
    //    //OnObserve가 미션 성공여부를 전달해줄 것이다.
    //    OnObserveComplete += MissionSubscriber;
    //}

    //private void OnDisable()
    //{
    //    droneInternalCam.OnDetectionUpdate -= DroneCamSubscriber;
    //    checkPoint.OnDropBox -= MissionSubscriber;
    //    OnObserveComplete -= MissionSubscriber;
    //}

    //private void Start()
    //{
    //    isDetecteds = new bool[droneInternalCam.enemies.Length];
    //}


    //public void InitializeMissionCatcher()
    //{
    //    droneInternalCam = FindObjectOfType<DroneInternalCam>();
    //    checkPoint = FindObjectOfType<CheckPoint>();
    //}

    //private void MissionSubscriber(int i)
    //{
    //    areCompleted[i] = true;
    //}

    //private void CheckPointSubscriber()
    //{

    //}

    //private void DroneCamSubscriber(int i)
    //{
    //    if (isDetecteds[i] == false) DroneSoundManager.instance.audi.PlayOneShot(DroneSoundManager.instance.soundpack.rockOnSfx2);
    //    isDetecteds[i] = true;
    //    Debug.Log(i + "번째 적 발견 OBSERBED");
    //    if (AreAllTrue() == true)
    //    {
    //        //정찰 미션 완료
    //        Debug.Log("정찰 미션 완료");
    //        DroneSoundManager.instance.audi.PlayOneShot(DroneSoundManager.instance.soundpack.rockOnSfx);
    //        OnObserveComplete?.Invoke((int)MissionType.Observe);
    //    }
    //}

    //public void TempFung()
    //{
    //    Debug.Log("Detection complete");
    //}

    //private bool AreAllTrue()
    //{
    //    for (int i = 0; i < isDetecteds.Length; i++)
    //    {
    //        if (isDetecteds[i] == false)
    //            return false;
    //    }
    //    return true;
    //}
}
