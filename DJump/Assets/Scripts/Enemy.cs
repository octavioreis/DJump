using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (string.Equals(other.gameObject.tag, "Player"))
        {
            GameManager.GameLives--;
        }
        else if (string.Equals(other.gameObject.tag, "Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
