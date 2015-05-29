using UnityEngine;
using System.Collections;

public class Lock : MonoBehaviour {

	public GameObject objectLocked;

	public ColoredKeyType coloredKeyType;

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.tag == "Player") 
		{
			if (other.GetComponent<ItemManager>().containsKey(coloredKeyType))
			{
				objectLocked.GetComponent<IUnlockable>().unlock();

				Destroy(this.gameObject);
			}
		}

	}
}
