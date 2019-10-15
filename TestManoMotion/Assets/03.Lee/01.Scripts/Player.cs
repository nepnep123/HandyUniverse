using UnityEngine;

public class Player : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        var col = collision.gameObject.GetComponent<PlayerToSkill>();

        if(col != null)
        {
            col.OnCollision();
        }
    }
}