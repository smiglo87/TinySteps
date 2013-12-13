using UnityEngine;
using System.Collections;
using System;

public class ViewAddNappy : UIView {

	public UserManager userManager;

	public UIInput nappyHour;
	public UIInput nappyMin;
	public UIPopupList nappyType;


	public override void Show()
	{
		UpdateTimeInputs();
		base.Show();
	}


	public void SubmitNappy()
	{
		DateTime nappyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(nappyHour.value), int.Parse(nappyMin.value), DateTime.Now.Second);

		userManager.AddNappy(nappyTime, (string)nappyType.value);
	}


	public void UpdateTimeInputs()
	{
		int hourNow = DateTime.Now.Hour;
		int minNow = DateTime.Now.Minute;
		
		if(minNow < 10) nappyMin.value = "0" + minNow.ToString();
		else nappyMin.value = minNow.ToString();
		
		if(hourNow < 10) nappyHour.value = "0" + hourNow.ToString();
		else nappyHour.value = hourNow.ToString();
	}
}
