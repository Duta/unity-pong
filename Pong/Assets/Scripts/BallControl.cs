using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {
	public float speed;
	public float spreadAngle;

	void Start () {
		Reset();
	}

	void Reset() {
		transform.position = Vector2.zero;
		rigidbody2D.velocity = Vector2.zero;

		float angle = Random.Range(-spreadAngle, spreadAngle);
		Vector2 unitDirection = new Vector2(
			Mathf.Cos(Mathf.Deg2Rad * angle),
			Mathf.Sin(Mathf.Deg2Rad * angle));
		Vector2 direction = unitDirection * speed;

		if(Random.Range(0f, 1f) < 0.5) {
			rigidbody2D.AddForce(new Vector2(-direction.x, direction.y));
		} else {
			rigidbody2D.AddForce(direction);
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.collider.tag == "Player") {
			float paddleSpeed = collision.collider.rigidbody2D.velocity.y;
			float x = rigidbody2D.velocity.x;
			float y = rigidbody2D.velocity.y;
			float newY = y/2 + paddleSpeed/3;
			float newX = Mathf.Sqrt(x*x + y*y - newY*newY);
			if(x < 0) newX = -newX;
			rigidbody2D.velocity = new Vector2(newX, newY);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "SideWall") {
			Reset();
		}
	}
}
