using UnityEngine;

public class Player : MonoBehaviour
{
    public static GameObject shield;

    private void Awake()
    {
        shield = GameObject.Find("Shield");
    }

    private void Start()
    {
        shield.gameObject.SetActive(false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        var col = collision.gameObject.GetComponent<PlayerToSkill>();
        if (col != null)
        {
            col.ProcessCollision();
        }

        var col2 = collision.gameObject.GetComponentInChildren<SphereCollider>();
        if (col2.gameObject.CompareTag("Enemy"))
        {
            shield.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("GameOver");
        }
    }
}