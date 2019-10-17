using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public GameObject[] skills;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Start()
    {
        InvokeRepeating("SpawnSkill", 10, 10f);
    }

    private void SpawnSkill()
    {
        GameObject skillsObj = (GameObject)Instantiate(skills[Random.Range(0, 4)], RandomSphereInPoint(30f), Quaternion.identity);
    }

    public Vector3 RandomSphereInPoint(float radius)
    {
        Vector3 getPoint = Random.onUnitSphere; // 반경 1을 갖는 구의 

        float r = Random.Range(0f, radius);
        return (getPoint * r) + player.transform.position;
    }

}
