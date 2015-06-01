using UnityEngine;
using System.Collections;

public class MoveDefault : MonoBehaviour, IMovable {

	public float distance = 3f;					// distance until the target
	public float speed = .05f;					
	public bool isMovementVertical = false;		// Is it vertical movement?
	public Vector2 target;						// The target is calculated based on distance

	private bool isMoving = false;

	void Start () {

		if (isMovementVertical) {
			target = new Vector2(transform.position.x, transform.position.y + distance);
		} else {
			target = new Vector2(transform.position.x + distance, transform.position.y);
		}
	}
	
	void Update () {

		if (isMoving)
		{
			if ( (!isMovementVertical && transform.position.x <= target.x) 
			    || (isMovementVertical && transform.position.y <= target.y))
			{
				transform.position = Vector2.Lerp(
					transform.position, 
					target,
					speed);
			}
		}
	}

	public void move ()
	{
		this.isMoving = true;
	}
}
