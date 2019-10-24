using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public ParticleSystem enemyExplosionParticle;
    private GameObject player;

    private float speed = 1f;

    private bool growingScaleIsDone;
    public bool playerPowerUp;

    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        StartCoroutine(GrowingScale1());

        if (growingScaleIsDone)
        {
            BetweenDistance();
        }
    }

    private void BetweenDistance()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (playerPowerUp)
        {
            Vector3 reverseDir = player.transform.position - transform.position;
            transform.position += -reverseDir.normalized * Time.deltaTime * speed;
        }
        else
        {
            if (distance < 0.5f)
            {
                transform.position = player.transform.position;
            }
            else
            {
                Vector3 dir = player.transform.position - transform.position;
                transform.position += dir.normalized * Time.deltaTime * speed;
            }
        }
    }

    IEnumerator GrowingScale1()
    {
        for (float a = 0f; a <= 1.5f; a += 0.01f)
        {
            yield return null;
            transform.localScale = new Vector3(a, a, a);
        }
        StartCoroutine(GrowingScale2());
    }

    IEnumerator GrowingScale2()
    {
        for (float b = 1.5f; b >= 0.8f; b -= 0.01f)
        {
            yield return null;
            transform.localScale = new Vector3(b, b, b);
        }
        StartCoroutine(GrowingScale3());
    }

    IEnumerator GrowingScale3()
    {
        for (float c = 0.8f; c <= 1f; c += 0.01f)
        {
            yield return null;

            transform.localScale = new Vector3(c, c, c);
        }
        yield return waitForSeconds;

        growingScaleIsDone = true;
    }

    // 플레이어의 파티클이 닿았을 때 : 제거되고 자기의 색깔과 동일한 파티클을 뿜는다
    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
        var effect = Instantiate(enemyExplosionParticle, transform.position, Quaternion.identity);

        ParticleSystem.MainModule particleSetting = effect.GetComponent<ParticleSystem>().main;
        particleSetting.startColor = transform.GetComponent<MeshRenderer>().material.color;

        Destroy(effect, 1.2f);
    }
}
