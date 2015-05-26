using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

	public bool yellowKeyPicked = false;

	//Canvas canvas;

	void Start ()
	{
		//canvas = GameObject.FindObjectOfType<Canvas> ();
	}

	void Update()
	{

	}


	public void setYellowKeyPicked(bool isPicked)
	{
		yellowKeyPicked = isPicked;
	}

	public bool isYellowKeyPicked()
	{
		return yellowKeyPicked;
	}
}
