using UnityEngine;
using System.Collections;

public class PopUpManager : MonoBehaviour {



	public void ShowError(string title, string message)
	{
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title, message, new string[] {"OK"} );		
		Debug.Log (message);
	}


}
