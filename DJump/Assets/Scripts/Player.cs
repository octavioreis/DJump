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

        if (Input.GetMouseButtonDown(0) && !PauseMenu.GameIsPaused)
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var direction = (mouseWorldPosition - SocketTransform.position).normalized;
            direction.z = 0;

            SocketTransform.transform.up = direction;

            Instantiate(Bullet, SocketTransform.transform.position, SocketTransform.transform.rotation);
        }

        var yAxisDistanceTraveled = _rigidBody.transform.position.y;
        if (yAxisDistanceTraveled > GameManager.GameScore)
            GameManager.GameScore = Mathf.FloorToInt(yAxisDistanceTraveled);

        var distanceCameraToPlayer = CameraTransform.position.y - yAxisDistanceTraveled;
        if (distanceCameraToPlayer >= GameManager.HalfScreenHeight)
            GameManager.EndingGameReason = GameOverReason.Fall;
    }

    void FixedUpdate()
    {
        var position = _rigidBody.transform.position;

        if (position.x < GameManager.PlayerHorizontalMinLimit || position.x > GameManager.PlayerHorizontalMaxLimit)
            position.x = -Mathf.Clamp(position.x, GameManager.PlayerHorizontalMinLimit, GameManager.PlayerHorizontalMaxLimit);

        var velocity = _rigidBody.velocity;
        velocity.x = _movement;

        _rigidBody.velocity = velocity;
        _rigidBody.transform.position = new Vector2(position.x, position.y);
    }
}
