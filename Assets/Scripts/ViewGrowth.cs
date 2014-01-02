using UnityEngine;
using System.Collections;
using System;

public class ViewGrowth : UIView {

	//Last weight Labels
	public UILabel lastWeightDate;
	public UILabel lastWeight;
	public UILabel weightTimeSince;

	//Last length labels
	public UILabel lastLengthDate;
	public UILabel lastLength;
	public UILabel lengthTimeSince;


	public override void Show()
	{
		base.Show();
	}


	//updates last weight labels
	public void UpdateLastWeightLabels(DateTime weightTime, int wUnits, int wDecimals, Weight.WeightUnit wUnit)
	{
		lastWeightDate.text = "Last: " + weightTime.ToString("dd.MM.yyyy");

		if(wUnit == Weight.WeightUnit.metric) lastWeight.text = "Weight: " + wUnits + " kg " + wDecimals + " g";
		else lastWeight.text = "Weight: " + wUnits + " lb " + wDecimals + " oz";
		
		TimeSpan timeSince = DateTime.Now - weightTime;
		weightTimeSince.text = "Days since: \n" + timeSince.Days.ToString() + " days";
	}

	//updates last length labels
	public void UpdateLastLengthLabels(DateTime lengthTime, float lUnits, float lDecimals, Length.LengthUnit lUnit)
	{
		lastLengthDate.text = "Last: " + lengthTime.ToString("dd.MM.yyyy");
		
		if(lUnit == Length.LengthUnit.metric) lastLength.text = "Weight: " + lUnits + " cm";
		else lastLength.text = "Weight: " + lUnits + " ft " + lDecimals + " inch";
		
		TimeSpan timeSince = DateTime.Now - lengthTime;
		lengthTimeSince.text = "Days since: \n" + timeSince.Days.ToString() + " days";
	}


}
