using UnityEngine;
using System.Collections;
using System;

public class ViewAddSleeping : UIView {

	public UserManager userManager;

	public UIInput startMonth;
	public UIInput startDay;
	public UIInput startHour;
	public UIInput startMin;
	
	public UIToggle noFinishCheckmark;
	
	public UILabel finishLabel;

	public UIInput finishMonth;
	public UIInput finishDay;
	public UIInput finishHour;
	public UIInput finishMin;


	public override void Show()
	{
		UpdateTimeInputs();
		base.Show();
	}


	//sends sleep details to AddSleeping
	public void SubmitSleep()
	{
		DateTime startTime = new DateTime(DateTime.Now.Year, int.Parse(startMonth.value), int.Parse(startDay.value), int.Parse(startHour.value), int.Parse(startMin.value), DateTime.Now.Second);

		if(noFinishCheckmark.value) 
		{
			DateTime finishTime = DateTime.MinValue;
			userManager.AddSleeping(startTime, finishTime);
		}
		else
		{
			DateTime finishTime = new DateTime(DateTime.Now.Year, int.Parse(finishMonth.value), int.Parse(finishDay.value), int.Parse(finishHour.value), int.Parse(finishMin.value), DateTime.Now.Second);
			userManager.AddSleeping(startTime, finishTime);
		}

	}

	//shows/hides finish sleep option
	public void SleepingStatusChange()
	{
		if(noFinishCheckmark.value)
		{
			finishLabel.gameObject.SetActive(false);
			finishDay.gameObject.SetActive(false);
			finishMonth.gameObject.SetActive(false);
			finishHour.gameObject.SetActive(false);
			finishMin.gameObject.SetActive(false);
		}
		else
		{
			finishLabel.gameObject.SetActive(true);
			finishDay.gameObject.SetActive(true);
			finishMonth.gameObject.SetActive(true);
			finishHour.gameObject.SetActive(true);
			finishMin.gameObject.SetActive(true);
		}
	}

	//updates time inputs
	public void UpdateTimeInputs()
	{
		int dayNow = DateTime.Now.Day;
		int monthNow = DateTime.Now.Month;
		int hourNow = DateTime.Now.Hour;
		int minNow = DateTime.Now.Minute;
		
		
		if(dayNow < 10) 
		{
			startDay.value = "0" + dayNow.ToString();
			finishDay.value = "0" + dayNow.ToString();
		}
		else
		{
			startDay.value = dayNow.ToString();
			finishDay.value = dayNow.ToString();
		}

		if(monthNow < 10)
		{
			startMonth.value = "0" + monthNow.ToString();
			finishMonth.value = "0" + monthNow.ToString();
		}
		else 
		{
			startMonth.value = monthNow.ToString();
			finishMonth.value = monthNow.ToString();
		}

		if(hourNow < 10)
		{
			startHour.value = "0" + hourNow.ToString();
			finishHour.value = "0" + hourNow.ToString();
		}
		else
		{
			startHour.value = hourNow.ToString();
			finishHour.value = hourNow.ToString();
		}

		if(minNow < 10)
		{
			startMin.value = "0" + minNow.ToString();
			finishMin.value = "0" + minNow.ToString();
		}
		else
		{
			startMin.value = minNow.ToString();
			finishMin.value = minNow.ToString();
		}
	}
}
