using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    private PlanetGravity planetGravity;
    private Rigidbody rb;

    public bool placeOnSurface = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        planetGravity = PlanetGravity.instance;
    }

    void FixedUpdate()
    {
        if (placeOnSurface)
        {
            Debug.Log("1");
            planetGravity.PlaceOnSurface(rb);
        }
        else
        {
            Debug.Log("2");
            planetGravity.Attract(rb);
        }
    }
}
