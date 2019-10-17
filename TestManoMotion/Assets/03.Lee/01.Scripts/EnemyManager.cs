using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;

    private Vector3 scaleZero = new Vector3(0,0,0);
    private MeshRenderer enemyColor;

    //Queue
    //List<>

    void Start()
    {
        enemy.transform.localScale = scaleZero;
        InvokeRepeating("SpawnEnemy", 5, 4f);
    }

    private void SpawnEnemy()
    {
        GameObject enemyObj = (GameObject)Instantiate(enemy, RandomSphereInPoint(50f), Quaternion.identity);

        enemyColor = enemyObj.GetComponent<MeshRenderer>();
        enemyColor.material.color = new Color(Random.value, Random.value, Random.value, 1f);
    }

    public Vector3 RandomSphereInPoint(float radius)
    {
        Vector3 getPoint = Random.onUnitSphere;

        float r = Random.Range(0f, radius);
        return (getPoint * r) + transform.position;
    }
}
// 오트젝트 풀링!