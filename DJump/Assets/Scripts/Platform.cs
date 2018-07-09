using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform CameraTransform;
    public bool Stationary = true;

    private float _jumpForce = 11.5f;
    private float _movement = 4f;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
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

    void FixedUpdate()
    {
        if (Stationary)
            return;

        var position = _rigidBody.transform.position;
        if (position.x < GameManager.PlatformHorizontalMinLimit || position.x > GameManager.PlatformHorizontalMaxLimit)
            _movement *= -1;

        var velocity = _rigidBody.velocity;
        velocity.x = _movement;

        _rigidBody.velocity = velocity;
        _rigidBody.transform.position = new Vector2(position.x, position.y);
    }

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
}
