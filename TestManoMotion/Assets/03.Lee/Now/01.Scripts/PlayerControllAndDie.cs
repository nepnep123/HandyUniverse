using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllAndDie : MonoBehaviour
{
    public GameObject deathEffect;

    private float screenWidth;

    private bool lockIs = true;

    Vector3 originForward;

    Quaternion quaternion = Quaternion.Euler(new Vector3(-90f, 0f, 0f));

    void Start()
    {
        screenWidth = Screen.width;
        originForward = transform.forward;
    }

    private void Update()
    {
        if (lockIs == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, 10f * Time.deltaTime);
        }

        PlayerMove(Input.GetAxis("Horizontal"));

        //Touch touch = Input.GetTouch(0);

        //if (Input.touchCount > 0)
        //{
        //    if (Input.GetTouch(0).position.x < screenWidth / 2)
        //    {
        //        PlayerMove(-1.0f);
        //    }

        //    if (Input.GetTouch(0).position.x > screenWidth / 2)
        //    {
        //        PlayerMove(1.0f);
        //    }
        //}

        //if (touch.phase == TouchPhase.Ended)
        //{
        //    lockIs = true;
        //}
    }

    private void PlayerMove(float playerMoveInput)
    {
        if (playerMoveInput != 0)
        {
            lockIs = false;
        }
        else
        {
            lockIs = true;
        }

        float a = Vector3.Angle(originForward, transform.forward);
        
        if (a < 30)
        {
            transform.Rotate(Vector3.up * playerMoveInput * 200 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            ScoreManager.instance.enabled = false;
            PlanetGameManager.instance.EndGame();

            //Instantiate(deathEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }

    private void OnParticleCollision()
    {
        ScoreManager.instance.enabled = false;
        PlanetGameManager.instance.EndGame();

        //Instantiate(deathEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
