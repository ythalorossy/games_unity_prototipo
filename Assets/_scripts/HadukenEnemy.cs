using UnityEngine;
using System.Collections;

public class HadukenEnemy : MonoBehaviour {

	public float speed = 4f;

	public float direction = 1;

	void Start()
	{
		if (direction < 0)
			Flip();
		
		GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
		
		Destroy(gameObject, 1.5f);
	}

	void Update () {
	}

	void Flip () {
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
