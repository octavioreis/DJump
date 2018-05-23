using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform CameraTransform;

    private float _jumpForce = 11f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y > 0f)
            return;

        var rigidBody = collision.collider.GetComponent<Rigidbody2D>();

        if (rigidBody != null)
        {
            var velocity = rigidBody.velocity;
            velocity.y = _jumpForce;
            rigidBody.velocity = velocity;
        }
    }

    private void Update()
    {
        var distanceCameraToPlatform = CameraTransform.position.y - transform.position.y;
        if (distanceCameraToPlatform >= GameManager.HalfScreenHeight + 2)
        {
            Destroy(gameObject);
            LevelGenerator.PlatformsToBuild++;
        }
    }
}
