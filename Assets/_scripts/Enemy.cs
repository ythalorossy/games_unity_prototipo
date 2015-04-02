using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool isDebuging = false;

	[HideInInspector] 
	public bool isFacingRight = true;

	// Field of view
	public float lookupTo;

	// Power
	public Rigidbody2D power;

	// Position to start power
	public Transform targetInstatiatePower;

	// Time to recharge 
	public float timeNeedsToRecharge;

	public float rechargingTime = 0;

	private bool recharging = false;

	void Start() {

		StartCoroutine(Patrol());
	}

	void FixedUpdate() {
		
		checkRecharging();
		
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

		} else {

			Vector2 target = new Vector2(
				transform.position.x + ((isFacingRight) ? 1f : -1f), 
				transform.position.y);
			
			transform.position = Vector2.Lerp(
				transform.position, 
				target,
				1);
		}
	}

	/*
	 * Check if "weapon" are recharging
	 */
	void checkRecharging()
	{
		rechargingTime += Time.deltaTime;
		
		if (rechargingTime <= timeNeedsToRecharge)
		{
			recharging = true;
		} 
		else 
		{
			rechargingTime = 0;
			recharging = false;
		}
	}

	/*
	 * 
	 */
	void searchPlayer()
	{
		Vector2 rayCastPosition = new Vector2(transform.position.x, transform.position.y + .5f);

		/*
		Debug.DrawLine(//transform.position,
		               new Vector2(transform.position.x, transform.position.y + .5f),
		               //new Vector2() (isFacingRight) ? Vector2.right : -Vector2.right, 
		               new Vector2(
								transform.position.x + ((isFacingRight) ? lookupTo : -lookupTo), 
								transform.position.y + .5f),
		               Color.green);
		*/

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

						recharging = true;
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
