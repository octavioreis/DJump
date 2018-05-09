using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	float jumpForce = 11f;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.relativeVelocity.y > 0f)
			return;
		
		var rigidBody = collision.collider.GetComponent<Rigidbody2D>();

		if (rigidBody != null)
		{
			var velocity = rigidBody.velocity;
			velocity.y = jumpForce;
			rigidBody.velocity = velocity;
		}
	}
}
