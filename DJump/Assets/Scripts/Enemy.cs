using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (string.Equals(other.gameObject.tag, "Player"))
        {
            GameManager.EndingGameReason = GameOverReason.Death;
        }
        else if (string.Equals(other.gameObject.tag, "Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
