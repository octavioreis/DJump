using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public GameObject Bullet;
    public Transform SocketTransform;
    public Transform CameraTransform;
    public float MovementSpeed = 7f;

    private Rigidbody2D _rigidBody;
    private float _movement = 0f;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _movement = Input.GetAxis("Horizontal") * MovementSpeed;

        if (Input.GetMouseButtonDown(0))
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var direction = (mouseWorldPosition - transform.position).normalized;
            direction.z = 0;

            SocketTransform.transform.up = direction;

            Instantiate(Bullet, SocketTransform.transform.position, SocketTransform.transform.rotation);
        }

        var distanceCameraToPlayer = CameraTransform.position.y - _rigidBody.transform.position.y;
        if (distanceCameraToPlayer >= GameManager.HalfScreenHeight)
            GameManager.GameLives = 0;
    }

    void FixedUpdate()
    {
        var position = _rigidBody.transform.position;
        if (position.x < GameManager.HorizontalMinLimit || position.x > GameManager.HorizontalMaxLimit)
            position.x = -Mathf.Clamp(position.x, GameManager.HorizontalMinLimit, GameManager.HorizontalMaxLimit);

        var velocity = _rigidBody.velocity;
        velocity.x = _movement;

        _rigidBody.velocity = velocity;
        _rigidBody.transform.position = new Vector2(position.x, position.y);
    }
}
