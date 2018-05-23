using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _points = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (string.Equals(other.gameObject.tag, "Player"))
        {
            GameManager.GameLives--;
        }
        else if (string.Equals(other.gameObject.tag, "Bullet"))
        {
            GameManager.GameScore += _points;
            Destroy(gameObject);
        }
    }
}
