using UnityEngine;
using System.Collections;
using System;

public class ViewBabyRegistration2 : UIView {

	public ViewBabyRegistration viewBRegistration;
	public UserManager userManager;
	public PopUpManager popUpManager;

	public UIInput birthWeightUnits;
	public UIInput birthWeightDecimals;
	
	public UIInput birthLengthUnits;
	public UIInput birthLengthDecimals;


	public override void Show()
	{
		Debug.Log("ViewBabyRegistration2 show called");
		base.Show();
	}


	public bool CheckForm()
	{
		bool result = false;

		if (viewBRegistration.CheckBabyRegistrationForm() == true)
		{
			if(birthWeightUnits.value.Length > 0) 
			{
				int weightUnits;
				if (int.TryParse(birthWeightUnits.value, out weightUnits) == true)
				{
					if(birthWeightDecimals.value.Length > 0)
					{
						int weightDecimals;
						if(int.TryParse(birthWeightDecimals.value, out weightDecimals) == true) 
						{
							Debug.Log("Weight correct");
							if(birthLengthUnits.value.Length > 0)
							{
								float lengthUnits;
								if(float.TryParse(birthLengthUnits.value, out lengthUnits) == true) 
								{
									Debug.Log("Length units correct");
									if(userManager.userUnit == UserManager.Unit.imperial)
									{
										if(birthLengthDecimals.value.Length > 0)
										{
											float lengthDecimals;
											if(float.TryParse(birthLengthDecimals.value, out lengthDecimals) == true) 
											{
												Debug.Log("Length decimals correct");
												result = true;
											}
											else popUpManager.ShowError("Error", "Please enter valid length");
										}
									}
									else if(userManager.userUnit == UserManager.Unit.metric) return true;
								}
								else popUpManager.ShowError("Errror", "Please eneter valid length");
							}
						}
						else popUpManager.ShowError("Error", "Please enter valid weight");
					} 
				}
				else popUpManager.ShowError("Error", "Please enter valid weight");
			}
		}
		else popUpManager.ShowError("Error", "Invalid data in baby form");

		return result;
	}


	public void SubmitBaby()
	{
		if(CheckForm() == true) 
		{
			Baby.Gender gender = Baby.Gender.male;
			if(viewBRegistration.gender.value == false) gender = Baby.Gender.female;

			DateTime dob = new DateTime(int.Parse(viewBRegistration.dobYear.value), int.Parse(viewBRegistration.dobMonth.value), int.Parse(viewBRegistration.dobDay.value));

			userManager.AddBaby(userManager.registerBabyProfilePicturePath, viewBRegistration.nameInput.value, gender, dob, int.Parse(birthWeightUnits.value), int.Parse(birthWeightDecimals.value), float.Parse(birthLengthUnits.value), float.Parse(birthLengthDecimals.value));
		}
	}

}
