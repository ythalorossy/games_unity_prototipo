using UnityEngine;
using System.Collections;

public class Haduken : MonoBehaviour {

	public float speed = 4f;

	public float timeToDeath = 3f;

	private GameObject player;

	private float direction = 1;
	private float directionY = 0;

	public float damage = 10.0f;

	private Vector2 myVelocity;

	// Use this for initialization
	void Start () {

		// Retrieve the direction which the Player are looking at.
		player = GameObject.FindGameObjectWithTag("Player");

		// Calculate the direction
		direction = player.transform.localScale.x * speed;

		if (direction < 0) {
			Flip ();
		}

		Destroy(gameObject, timeToDeath);
	}

	// Update is called once per frame
	void Update () {

		GetComponent<Rigidbody2D>().velocity = new Vector2(direction, directionY);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log (other.tag + " " + other.name);
		if (other.tag == "Enemy") 
		{
			other.GetComponent<EnemyHealth>().TakeDamage(damage);
		}

		direction = -direction;
		directionY = Mathf.Round(Random.Range (-1, 1));
		Flip ();

		Destroy(this.gameObject, .1f);
	}

	void Flip () {

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
