using UnityEngine;
using System.Collections;

public class BreastfeedController : MonoBehaviour {


	public ViewAddFeeding viewAddFeeding;
	
	public int leftAmount;
	public int rightAmount;
	public int bothBreastAmounts;


	public void AddBreastfeedAmount()
	{
		if(viewAddFeeding.leftBreast.CurrentLabelText == "Left Breast") leftAmount = 0;
		else leftAmount = int.Parse(viewAddFeeding.leftBreast.CurrentLabelText);


		if(viewAddFeeding.rightBreast.CurrentLabelText == "Right Breast") rightAmount = 0;
		else rightAmount = int.Parse(viewAddFeeding.rightBreast.CurrentLabelText);

		bothBreastAmounts = leftAmount + rightAmount;
	}



}
