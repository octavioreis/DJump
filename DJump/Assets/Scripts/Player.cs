using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public GameObject Bullet;
    public Transform BulletSocket;
    public float HorizontalMaxLimit;
    public float HorizontalMinLimit;
    public float MovementSpeed = 7f;

    private Rigidbody2D rigidBody;
    private float movement = 0f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = Input.GetAxis("Horizontal") * MovementSpeed;
    }

    void FixedUpdate()
    {
        var velocity = rigidBody.velocity;
        velocity.x = movement;
        rigidBody.velocity = velocity;

        var position = rigidBody.transform.position;
        if (position.x < HorizontalMinLimit || position.x > HorizontalMaxLimit)
            position.x = -Mathf.Clamp(position.x, HorizontalMinLimit, HorizontalMaxLimit);

        rigidBody.transform.position = new Vector2(position.x, position.y);

        if (Input.GetMouseButtonDown(0))
        {
            //var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //var direction = (mouseWorldPosition - transform.position).normalized;
            //BulletSocket.transform.up = direction;

            Instantiate(Bullet, BulletSocket.transform.position, BulletSocket.transform.rotation);
        }
    }
}
