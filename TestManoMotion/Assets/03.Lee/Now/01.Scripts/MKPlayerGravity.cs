using System;
using System.Collections;
using UnityEngine;

public class MKPlayerGravity : MonoBehaviour
{
    private Rigidbody rb;

    private bool playerPos = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        OnSurface();
    }

    private void OnSurface()
    {
        if (MKManager.instance.planetSphereCollider == null)
        {
            return;
        }

        if (playerPos == false)
        {
            //transform.SetParent(MKManager.instance.planet.transform.GetChild(0));

            //행성 위치
            Vector3 planetPos = MKManager.instance.planetSphereCollider.transform.position;
            //행성에서 플레이어를 바라보는 벡터
            Vector3 dir = transform.position - planetPos;
            //행성 로컬스케일
            float localx = MKManager.instance.planetSphereCollider.transform.localScale.x;
            //행성 스피어콜라이더 반지름
            float planetRad = MKManager.instance.planetSphereCollider.radius;

            //플레이어의 위치 = 행성위치벡터 + 행성으로부터 플레이어를 바라보는 방향 *(행성의 반지름 * 로컬스케일 x)
            transform.position = planetPos + dir.normalized *(localx * planetRad);

            Vector3 gravityUp = (rb.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.FromToRotation(rb.transform.up, gravityUp) * rb.rotation;
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 50f * Time.deltaTime));

            playerPos = true;
        }
    }
}
