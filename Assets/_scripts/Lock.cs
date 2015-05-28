using UnityEngine;
using System.Collections;

public class Lock : MonoBehaviour {

	public GameObject objectLocked;

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.tag == "Player") 
		{
			if (other.GetComponent<ItemManager>().isYellowKeyPicked())
			{
				objectLocked.GetComponent<IUnlockable>().unlock();

				Destroy(this.gameObject);
			}
		}

	}
}
