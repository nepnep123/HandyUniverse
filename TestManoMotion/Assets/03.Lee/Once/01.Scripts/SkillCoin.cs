using System;
using UnityEngine;

public class SkillCoin : PlayerToSkill
{
    public override void ProcessCollision()
    {
        Debug.Log("SkillCoin");
        //ScoreManager1.instance.AddScore();
        Destroy(gameObject);
    }
}


// 1. ★불        2. 따발총    3. 에너지파   4. 지우개코인   5. 톱니바퀴 
// 1. 코인        2. 거미줄    3. 폭탄       4. 힐          5. 에너지파*3  6. 블랙홀