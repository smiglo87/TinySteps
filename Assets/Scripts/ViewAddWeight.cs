using UnityEngine;
using System.Collections;
using System;

public class ViewAddWeight : UIView {

	public UserManager userManager;


	public UIInput weightDay;
	public UIInput weightMonth;
	public UIInput weightUnits;
	public UIInput weightDecimals;



	public override void Show()
	{
		UpdateTimeInputs();
		LabelWeightClearing();
		InputLabelsChange();
		base.Show();
	}

	public void SubmitWeight()
	{
		DateTime weightTime = new DateTime(DateTime.Now.Year, int.Parse(weightMonth.value), int.Parse(weightDay.value));
		
		userManager.AddWeight(weightTime, int.Parse(weightUnits.value), int.Parse(weightDecimals.value));
	}
		                                  

	public void UpdateTimeInputs()
	{
		int dayNow = DateTime.Now.Day;
		int monthNow = DateTime.Now.Month;
		
		if(dayNow < 10) weightDay.value = "0" + dayNow.ToString();
		else weightDay.value = dayNow.ToString();
		
		if(monthNow < 10) weightMonth.value = "0" + monthNow.ToString();
		else weightMonth.value = monthNow.ToString();
	}


	public void FillWeight()
	{
		if(weightUnits.value == "kg" || weightUnits.value == "lb" || weightUnits.value == "") weightUnits.value = "0";
			
		if(weightDecimals.value == "g" || weightDecimals.value == "oz" || weightDecimals.value == "") weightDecimals.value = "00";
	}


	public void LabelWeightClearing()
	{
		weightUnits.value = "";
		weightDecimals.value = "";
	}

	//updates inputs labels according to chosen userUnit 
	public void InputLabelsChange()
	{
		if(userManager.userUnit == UserManager.Unit.metric)
		{
			weightUnits.defaultText = "kg";
			weightUnits.label.text = "kg";
			weightDecimals.defaultText = "g";
			weightDecimals.label.text = "g";
		}
		else
		{
			weightUnits.defaultText = "lb";
			weightDecimals.defaultText = "oz";
			weightUnits.label.text = "lb";
			weightDecimals.label.text = "oz";
		}
	}
}
