using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour{

	public bool yellowKeyPicked = false;

	public void setYellowKeyPicked(bool isPicked)
	{
		yellowKeyPicked = isPicked;
	}

	public bool isYellowKeyPicked()
	{
		return yellowKeyPicked;
	}

	void OnGUI()
	{

		//if (yellowKeyPicked)
		//{
			//Debug.Log (GameObject.Find("YellowKey").name);

		//GameObject.Find("YellowKey").gameObject.
		//}
	}
}
