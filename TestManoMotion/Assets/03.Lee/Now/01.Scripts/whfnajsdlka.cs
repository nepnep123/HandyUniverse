using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whfnajsdlka : MonoBehaviour
{
    public GameObject[] planets;

    private int randomInt;

    public void Generate()
    {
        randomInt = Random.Range(0, planets.Length);
        Instantiate(planets[randomInt], transform.position, transform.rotation);
    }
}