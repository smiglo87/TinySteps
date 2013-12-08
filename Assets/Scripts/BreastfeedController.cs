using UnityEngine;
using System.Collections;

public class BreastfeedController : MonoBehaviour {


	public GUIManager guiManager;
	
	public int leftAmount;
	public int rightAmount;
	public int bothBreastAmounts;


	public void AddBreastfeedAmount()
	{
		if(guiManager.leftBreast.CurrentLabelText == "Left Breast") leftAmount = 0;
		else leftAmount = int.Parse(guiManager.leftBreast.CurrentLabelText);


		if(guiManager.rightBreast.CurrentLabelText == "Right Breast") rightAmount = 0;
		else rightAmount = int.Parse(guiManager.rightBreast.CurrentLabelText);

		bothBreastAmounts = leftAmount + rightAmount;
	}



}
