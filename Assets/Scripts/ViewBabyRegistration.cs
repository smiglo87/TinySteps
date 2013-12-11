using UnityEngine;
using System.Collections;
using System;

public class ViewBabyRegistration : UIView {

	public PopUpManager popUpManager;
	public UIViewController viewController;
	public ViewBabyRegistration2 viewBR2;
	public UserManager userManager;

	public UITexture profilePicture;
	public Texture2D noAvatarPicture;
	public UIInput nameInput;
	public UIToggle gender;
	public UIInput dobDay;
	public UIInput dobMonth;
	public UIInput dobYear;

	public override void Show()
	{
		Debug.Log("ViewBabyRegistration show called");
		if(nameInput.value.Length < 1) ClearRegistrationForm();
		base.Show();
	}


	public void VerifyFirstForm()
	{
		if (CheckBabyRegistrationForm() == true) viewController.ToBabyRegistrationView2();
	}

	//checks if data in the form are in the correct format
	public bool CheckBabyRegistrationForm()
	{
		if(nameInput.value.Length > 0)
		{
			if(Baby.IsDateTime(dobYear.value + "." + dobMonth.value + "." + dobDay.value))
			{
				if(DateTime.Today.CompareTo(new DateTime(int.Parse(dobYear.value), int.Parse(dobMonth.value), int.Parse(dobDay.value))) >= 0) 
				{
					//success
					return true;
				}
				else
				{
					popUpManager.ShowError ("Error", "Ooops! Your baby isn't out yet! Please enter valid date of birth");
					return false;
				}
			}
			else
			{
				popUpManager.ShowError("Error", "Please enter valid date of birth");
				return false;
			}
		}
		else 
		{
			popUpManager.ShowError("Error", "Please enter baby's name");
			return false;
		}
	}



	public void ClearRegistrationForm()
	{
		profilePicture.mainTexture = noAvatarPicture;
		nameInput.label.text = "Baby Name";
		gender.value = true;
		dobDay.label.text = "Day";
		dobMonth.label.text = "Month";
		dobYear.label.text = "Year";

		if(userManager.userUnit == UserManager.Unit.metric)
		{
			viewBR2.birthWeightUnits.label.text = "kg";
			viewBR2.birthWeightDecimals.label.text = "g";
			viewBR2.birthLengthUnits.label.text = "cm";
		}
		else
		{
			viewBR2.birthWeightUnits.label.text = "lb";
			viewBR2.birthWeightDecimals.label.text = "oz";
			viewBR2.birthLengthUnits.label.text = "ft";
			viewBR2.birthLengthDecimals.label.text = "inch";
		}
	}


}
