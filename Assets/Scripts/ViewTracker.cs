﻿using UnityEngine;
using System.Collections;
using System;

public class ViewTracker : UIView {

	public ViewFeedingList viewFeedingList;


	//Last Feeding labels
	public UILabel lastMealTime;
	public UILabel lastMealAmount;
	public UILabel mealTimeSince;

	//Last Nappy labels
	public UILabel lastNappyTime;
	public UILabel lastNappyType;
	public UILabel nappyTimeSince;


	public override void Show()
	{
		base.Show();
	}


	//updates last meal labels 
	public void UpdateLastMealLabels(DateTime mealTime, float mealAmount, Meal.UnitType mealUnit)
	{
		lastMealTime.text = "Last: " + mealTime.ToString("dd.MM") + " at " + mealTime.ToString("HH:mm");
		
		if(mealUnit == Meal.UnitType.min)
		{
			if(mealAmount > 60) lastMealAmount.text = "Time " + (int)mealAmount / 60 + " h " + mealAmount % 60 + mealUnit;
			else lastMealAmount.text = "Time " + mealAmount.ToString() + " " + mealUnit;
		}
		else lastMealAmount.text = "Amount: " + mealAmount.ToString() + " " + mealUnit;
		
		TimeSpan timeSince = DateTime.Now - mealTime;
		mealTimeSince.text = "Time since: \n" + timeSince.Days.ToString() + " days " + timeSince.Hours.ToString() + " h " + timeSince.Minutes.ToString() + " min";
	}

	//updates last nappy labels
	public void UpdateLastNappyLabels(DateTime nappyTime, Nappy.NappyType nappyType)
	{
		lastNappyTime.text = "Last: " + nappyTime.ToString("dd.MM") + " at " + nappyTime.ToString("HH:mm");
		
		lastNappyType.text = "Type: " + nappyType.ToString();
		
		TimeSpan timeSince = DateTime.Now - nappyTime;
		nappyTimeSince.text = "Time since: \n" + timeSince.Days.ToString() + " days " + timeSince.Hours.ToString() + " h " + timeSince.Minutes.ToString() + " min";
	}
}
