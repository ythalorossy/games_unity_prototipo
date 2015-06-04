using UnityEngine;
using System.Collections;

public class HadukenEnemy : MonoBehaviour {

	public float speed = 4.0f;

	public float direction = 1.0f;

	public float damage = 10.0f;

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

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log (other.tag + "" + other.name);
		if (other.tag == "Player") 
		{
			other.GetComponent<PlayerHealth>().TakeDamage(damage);
			
		}

		Destroy(this.gameObject);
	}

}
