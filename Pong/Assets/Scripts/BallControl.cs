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
		switch(collision.collider.tag) {
		case "Player":
			rigidbody2D.velocity = new Vector2(
				rigidbody2D.velocity.x,
				rigidbody2D.velocity.y/2 + collision.collider.rigidbody2D.velocity.y/3);
			break;
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		switch(collider.tag) {
		case "SideWall":
			Reset();
			break;
		}
	}
}
