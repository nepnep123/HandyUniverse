using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableInfoma : InteractableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 45f * Time.deltaTime, Space.World);
    }
}
