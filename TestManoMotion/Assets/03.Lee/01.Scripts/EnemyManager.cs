using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;

    private Vector3 scaleZero = new Vector3(0,0,0);
    private MeshRenderer enemyColor;

    public bool lockIs;

    void Start()
    {
        enemy.transform.localScale = scaleZero;
        InvokeRepeating("SpawnEnemy", 3, 2f);
    }

    private void SpawnEnemy()
    {
        if (lockIs)
        {
            //float randomX = Random.Range(-50f, 50f);
            //float randomY = Random.Range(-50f, 50f);
            //float randomZ = Random.Range(-50f, 50f);

            GameObject enemyObj = (GameObject)Instantiate(enemy, RandomSphereInPoint(50f), Quaternion.identity);
            //GameObject enemyObj = (GameObject)Instantiate(enemy, new Vector3(randomX, randomY, randomZ), Quaternion.identity);

            enemyColor = enemyObj.GetComponent<MeshRenderer>();
            enemyColor.material.color = new Color(Random.value, Random.value, Random.value, 1f);
        }
    }

    public Vector3 RandomSphereInPoint(float radius)
    {
        Vector3 getPoint = Random.onUnitSphere; // 반경 1을 갖는 구의 

        float r = Random.Range(0f, radius);
        return (getPoint * r) + transform.position;
    }
}
