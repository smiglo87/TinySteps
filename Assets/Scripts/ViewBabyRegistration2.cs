using UnityEngine;
using System.Collections;
using System;

public class ViewBabyRegistration2 : UIView {

	public ViewBabyRegistration viewBRegistration;
	public UserManager userManager;

	public UIInput birthWeightUnits;
	public UIInput birthWeightDecimals;
	
	public UIInput birthLengthUnits;
	public UIInput birthLengthDecimals;


	public override void Show()
	{
		Debug.Log("ViewBabyRegistration2 show called");
		base.Show();
	}


	public void CheckForm()
	{
		if (viewBRegistration.CheckBabyRegistrationForm() == true)
		{
			if(birthWeightUnits.value.Length > 0) 
			{
				int number;
				if (int.TryParse(birthWeightUnits.value, out number) == true)
				{
					//success
				}
				else
				{
					//display error
				}
			}
		}



	}



}
