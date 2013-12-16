using UnityEngine;
using System.Collections;
using System;

public class ViewAddJournalEvent : UIView {

	public UserManager userManager;
	public PhotoManager photoManager;

	public UIInput day;
	public UIInput month;
	public UIInput year;

	public UIInput addMilestone;

	public override void Show()
	{
		UpdateTimeInputs();
		base.Show();
	}


	public void UpdateTimeInputs()
	{
		int dayNow = DateTime.Now.Day;
		int monthNow = DateTime.Now.Month;
		int yearNow = DateTime.Now.Year;
		
		if(dayNow < 10) day.value = "0" + dayNow.ToString();
		else day.value = dayNow.ToString();
		
		if(monthNow < 10) month.value = "0" + monthNow.ToString();
		else month.value = monthNow.ToString();

		year.value = yearNow.ToString();
	}



}
