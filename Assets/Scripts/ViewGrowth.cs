using UnityEngine;
using System.Collections;
using System;

public class ViewGrowth : UIView {

	//Last weight Labels
	public UILabel lastWeightDate;
	public UILabel lastWeight;
	public UILabel weightTimeSince;


	public override void Show()
	{
		base.Show();
	}


	//updates last weight labels
	public void UpdateLastWeightLabels(DateTime weightTime, int wUnits, int wDecimals, Weight.WeightUnit wUnit)
	{
		lastWeightDate.text = "Last: " + weightTime.ToString("dd.MM");

		if(wUnit == Weight.WeightUnit.metric) lastWeight.text = "Weight: " + wUnits + " kg " + wDecimals + " g";
		else lastWeight.text = "Weight: " + wUnits + " lb " + wDecimals + " oz";
		
		TimeSpan timeSince = DateTime.Now - weightTime;
		weightTimeSince.text = "Time since: \n" + timeSince.Days.ToString() + " days ";
	}
}
