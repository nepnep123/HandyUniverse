using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_GC : MonoBehaviour
{
    public bool isGC = false;
    //Texture2D tx;
    public int gett;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            isGC = !isGC;
        for (int i = 0; i < 500; i++)
        {
            if (isGC)
            {
                Texture2D tx = new Texture2D(2, 2);
            }
            else
            {
                Vector3 pos = new Vector3(25, 24, 33);
            }
        }
    }
}
