using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fmew : MonoBehaviour
{
    Vector3 originForward;
    Quaternion quaternion = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    private bool lockIs = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, 10f * Time.deltaTime);

        PlayerMove(Input.GetAxis("Horizontal"));
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
            transform.Rotate(Vector3.back * playerMoveInput * 200 * Time.deltaTime);
        }
    }
}
