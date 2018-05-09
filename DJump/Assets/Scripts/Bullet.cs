using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public float MaxBulletTravelDistance;

    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);

        if (transform.position.y - startingPosition.y > MaxBulletTravelDistance)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (string.Equals(other.gameObject.tag, "Enemy"))
            Destroy(gameObject);
    }
}
