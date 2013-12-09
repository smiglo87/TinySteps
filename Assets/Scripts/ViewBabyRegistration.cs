using UnityEngine;
using System.Collections;
using System;

public class ViewBabyRegistration : UIView {

	public GUIManager guiManager;
	public ViewManager viewManager;

	public UITexture profilePicture;
	public Texture2D noAvatarPicture;
	public UIInput nameInput;
	public UIToggle genderMale;
	public UIInput dobDay;
	public UIInput dobMonth;
	public UIInput dobYear;

	public UIButton back;
	public UIButton next;

	public override void Show()
	{
		Debug.Log("ViewBabyRegistration show called");
		ClearRegistrationForm();
		base.Show();
	}


	public void MoveToRegistrationView2()
	{
		if(next.isEnabled == true) 
		{
			CheckBabyRegistrationForm();
		}
	}



	public void CheckBabyRegistrationForm()
	{
		if(nameInput.value.Length > 0)
		{
			if(Baby.IsDateTime(dobYear.value + "." + dobMonth.value + "." + dobDay.value))
			{
				if(DateTime.Today.CompareTo(new DateTime(int.Parse(dobYear.value), int.Parse(dobMonth.value), int.Parse(dobDay.value))) >= 0)
				{
					viewManager.ToBabyRegistrationView2();
				}
				else guiManager.ShowError ("Error", "Ooops! Your baby isn't out yet! Please enter valid date of birth");
			}
			else guiManager.ShowError("Error", "Please enter valid date of birth");
		}
		else guiManager.ShowError("Error", "Please enter baby's name");
	}



	public void ClearRegistrationForm()
	{
		profilePicture.mainTexture = noAvatarPicture;
		nameInput.label.text = "Baby Name";
		genderMale.value = true;
		dobDay.label.text = "Day";
		dobMonth.label.text = "Month";
		dobYear.label.text = "Year";
	}


}
