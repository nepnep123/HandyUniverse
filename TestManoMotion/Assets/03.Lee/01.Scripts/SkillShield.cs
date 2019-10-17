using System;
using UnityEngine;

public class SkillShield : PlayerToSkill
{
    public override void ProcessCollision()
    {
        Debug.Log("SkillShield");
        Player.shield.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    //private void AddShield()
    //{
    //    Vector3 playertPosition = player.position;
    //    Quaternion playerAngle = player.rotation;

    //    var effect = Instantiate(shield, playertPosition, playerAngle);
    //    effect.transform.SetParent(player.transform);
    //}
}


// 1. 불(사라짐) 2. 에너지파(사라짐)     2. 따발총(사라짐)  3. 에너지파(사라짐)  4. 지우개(사라짐)  5. 톱니바퀴(사라짐)
// 1. 코인 2. 실드                      2. 거미줄  4. 폭탄  6. 블랙홀