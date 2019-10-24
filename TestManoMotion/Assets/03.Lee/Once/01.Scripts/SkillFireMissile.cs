using System;
using UnityEngine;

public class SkillFireMissile : PlayerToSkill
{
    public override void ProcessCollision()
    {
        Debug.Log("SkillFireMissile");
        //Missile함수
        Destroy(gameObject);
    }
}


// 1. ★불(사라짐)  2. 따발총(사라짐)  3. 에너지파(사라짐)  4. 지우개(사라짐)  5. 톱니바퀴(사라짐)
// 1. 코인        2. 거미줄    3. 폭탄       4. 힐        5. 블랙홀