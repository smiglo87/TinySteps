using UnityEngine;
using System.Collections;
using System;

public class ViewAddLength : UIView {

	public UserManager userManager;

	public UIInput lengthDay;
	public UIInput lengthMonth;
	public UIInput lengthYear;
	public UIInput lengthUnits;
	public UIInput lengthDecimals;


	public override void Show()
	{
		UpdateTimeInputs();
		LabelLengthClearing();
		InputLabelsChange();
		base.Show();
	}
	
	public void SubmitLength()
	{
		DateTime lengthTime = new DateTime(int.Parse(lengthYear.value), int.Parse(lengthMonth.value), int.Parse(lengthDay.value));

		if(userManager.userUnit == UserManager.Unit.metric) lengthDecimals.value = "0";
		userManager.AddLength(lengthTime, float.Parse(lengthUnits.value), float.Parse(lengthDecimals.value));
	}

	public void UpdateTimeInputs()
	{
		int dayNow = DateTime.Now.Day;
		int monthNow = DateTime.Now.Month;
		int yearNow = DateTime.Now.Year;
		
		if(dayNow < 10) lengthDay.value = "0" + dayNow.ToString();
		else lengthDay.value = dayNow.ToString();
		
		if(monthNow < 10) lengthMonth.value = "0" + monthNow.ToString();
		else lengthMonth.value = monthNow.ToString();

		lengthYear.value = yearNow.ToString();
	}

	public void FillLength()
	{
		if(lengthUnits.value == "cm" || lengthUnits.value == "ft" || lengthUnits.value == "") lengthUnits.value = "0";
		
		if(lengthDecimals.value == "inch" || lengthDecimals.value == "") lengthDecimals.value = "00";
	}

	public void LabelLengthClearing()
	{
		lengthUnits.value = "";
		lengthDecimals.value = "";
	}

	//updates inputs labels according to chosen userUnit 
	public void InputLabelsChange()
	{
		if(userManager.userUnit == UserManager.Unit.metric)
		{
			lengthUnits.defaultText = "cm";
			lengthUnits.label.text = "cm";
			lengthDecimals.gameObject.SetActive(false);
		}
		else
		{
			lengthDecimals.gameObject.SetActive(true);
			lengthUnits.defaultText = "ft";
			lengthDecimals.defaultText = "inch";
			lengthUnits.label.text = "ft";
			lengthDecimals.label.text = "inch";
		}
	}
}
