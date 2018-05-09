﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public GameObject Bullet;
    public Transform SocketTransform;
    public Transform CameraTransform;
    public float HorizontalMaxLimit;
    public float HorizontalMinLimit;
    public float MovementSpeed = 7f;
    public float HalfScreenHeight = 4.6f;

    private Rigidbody2D rigidBody;
    private float movement = 0f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = Input.GetAxis("Horizontal") * MovementSpeed;

        if (Input.GetMouseButtonDown(0))
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var direction = (mouseWorldPosition - transform.position).normalized;
            direction.z = 0;

            SocketTransform.transform.up = direction;

            Instantiate(Bullet, SocketTransform.transform.position, SocketTransform.transform.rotation);
        }

        var distanceCameraToPlayer = CameraTransform.position.y - rigidBody.transform.position.y;
        if (distanceCameraToPlayer >= HalfScreenHeight)
            GameManager.GameLives = 0;
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
    }
}
