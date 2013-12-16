using UnityEngine;
using System.Collections;

public class UIButtonPress : MonoBehaviour {


	public bool isPressed;

	public void OnPress(bool isDown)
	{
		isPressed = isDown;
	}


}
