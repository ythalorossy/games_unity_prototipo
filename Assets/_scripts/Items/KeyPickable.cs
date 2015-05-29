 using UnityEngine;
using System.Collections;


//[RequireComponent(typeof(AudioSource))]
public class KeyPickable : MonoBehaviour {

	public ColoredKeyType ColoredKeyType;

	public AudioClip pickupAudio;

	void Start() {

	}


	void OnTriggerEnter2D(Collider2D collider) {

		if (collider.tag == "Player") 
		{
			collider.GetComponent<ItemManager>().pickKey(this.ColoredKeyType);

			AudioSource.PlayClipAtPoint(pickupAudio, collider.transform.position);

			Destroy(this.gameObject);
		}

	}
}
