using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChampion : MonoBehaviour
{
    public GameObject car;

    private Transform gameObj;

    public void Champion()
    {
        gameObj = GameObject.Find("GameObject").transform;
        //gameObj = GameObject.Find("GameObject(Clone)").transform;

        var carCar = Instantiate(car, gameObj.position + new Vector3(0f, 0f, -0.1f), transform.rotation);
        carCar.transform.SetParent(gameObj);

    }
}
