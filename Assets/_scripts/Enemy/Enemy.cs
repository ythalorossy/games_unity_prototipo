using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	//[HideInInspector] 
	public bool isFacingRight = true;

	// Force to move
	public float moveForce = 1.0f;

	// Field of view
	public float lookupTo;

	// Power
	public Rigidbody2D power;

	// Position to start power
	public Transform targetInstatiatePower;


	private bool needRecharge = false;				// Needs recharge?

	public float timeNeedsToRecharge;				// Time to recharge

	public float elapsedTimeUntilRecharge = 0;		// Elapsed Time until Recharge

	private bool recharging = false;				// Are recharging?

	void Start() {

		StartCoroutine(Patrol());
	}

	void FixedUpdate() {

		checkNeedsRecharge();
		
		searchPlayer();

		Debug.DrawRay(
			new Vector2(transform.position.x + ((isFacingRight) ? 1f : -1f), transform.position.y + .5f), 
			-Vector2.up, 
			Color.red);

	}

	IEnumerator Patrol()
	{
		move();

		yield return new WaitForSeconds(1f);

		StartCoroutine(Patrol());
	}

	/*
	 * Check if can move.
	 * Start one raycast in the transform.position.x + (1 or -1) looking at down.
	 * If the hit has the tag EnemyCanMoveHere return true, otherside false;  
	 */
	bool canMove()
	{
		Vector2 rayCastPosition = new Vector2(transform.position.x + ((isFacingRight) ? 1f : -1f), transform.position.y + .5f);

		RaycastHit2D hit = Physics2D.Raycast(rayCastPosition, -Vector2.up, 1f);

		if (hit.collider != null) {

			if (hit.collider.gameObject.tag == "EnemyCanMoveHere")
			{
				return true;
			}
		}

		return false;
	}

	/*
	 * Movement
	 */
	void move()
	{
		if (!canMove())
		{
			Flip ();
		} 

		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().AddForce(Vector2.right * ((isFacingRight) ? moveForce : -moveForce));
	}

	void checkNeedsRecharge()
	{
		if (needRecharge) {
			recharge();
		}
	}

	void recharge ()
	{
		elapsedTimeUntilRecharge += Time.deltaTime;

		if (elapsedTimeUntilRecharge <= timeNeedsToRecharge)
		{
			recharging = true;
		} 
		else 
		{
			recharging = false;
			elapsedTimeUntilRecharge = 0;
			needRecharge = false;
		}

	}


	/*
	 * 
	 */
	void searchPlayer()
	{
		Vector2 rayCastPosition = new Vector2(transform.position.x + ((isFacingRight) ? .5f : -.5f), transform.position.y + .5f);

		Debug.DrawLine(//transform.position,
		               new Vector2(transform.position.x + .5f, transform.position.y + .5f),
		               //new Vector2( (isFacingRight) ? Vector2.right : -Vector2.right), 
		               new Vector2(
								transform.position.x + ((isFacingRight) ? lookupTo : -lookupTo), 
								transform.position.y + .5f),
		               Color.green);


		// Raycast looking...
		RaycastHit2D hit = Physics2D.Raycast(
			rayCastPosition, 
			(isFacingRight) ? Vector2.right : -Vector2.right, 
			lookupTo);


		// Hit detected
		if (hit.collider != null) {
			
			// Hit on Player
			if (hit.collider.gameObject.tag == "Player")
			{
				// Distance from my position until hit's position 
				float distanceHit = Mathf.Abs(hit.point.y - transform.position.y);
				
				// Distante of hit is less or equal to distance which i can see
				if (distanceHit <= lookupTo)
				{
					// If not recharging
					if (!recharging)
					{
						shot();

						needRecharge = true;
					}
				}
			}
		}
	}

	/*
	 * 
	 */ 
	void shot()
	{
		// Set direction of power
		power.GetComponent<HadukenEnemy>().direction = targetInstatiatePower.transform.localScale.x;
		
		Instantiate(power, targetInstatiatePower.position, targetInstatiatePower.rotation);
	}

	void Flip()
	{
		// Invert the value of facingRight
		isFacingRight = !isFacingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

		Transform targetPower = transform.FindChild("TargetInstantiatePower");
		Vector3 theScaleTarget = targetPower.transform.localScale;
		theScaleTarget.x *= -1;
		targetPower.localScale = theScale;

	}
}
