using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WERF : MonoBehaviour
{
    Quaternion quaternion = Quaternion.Euler(new Vector3(-90f, 0f, 0f));


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left * 200 * Time.deltaTime, Space.World);

        PlayerMove(Input.GetAxis("Horizontal"));
    }

    private void PlayerMove(float playerMoveInput)
    {
        transform.Rotate(Vector3.up * playerMoveInput * 200 * Time.deltaTime, Space.World);
    }
}
