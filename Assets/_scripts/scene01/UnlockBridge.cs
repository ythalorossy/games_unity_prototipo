using UnityEngine;
using System.Collections;

public class UnlockBridge : MonoBehaviour, IUnlockable {

	public void unlock () {

		float distance = 3f;

		Vector2 target = new Vector2(transform.position.x + distance, transform.position.y);

		transform.position = Vector2.Lerp(
			transform.position, 
			target,
			1);
	}

}
