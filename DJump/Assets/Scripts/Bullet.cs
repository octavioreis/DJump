using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public float MaxBulletTravelDistance;

    private void Start()
    {
        Invoke("DestroyBullet", 5);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (string.Equals(other.gameObject.tag, "Enemy"))
            Destroy(gameObject);
    }
}
