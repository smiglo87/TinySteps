using UnityEngine;
using System.Collections;
using System;
using System.Globalization;

public class WeightListCell : MonoBehaviour {


	public UILabel date;
	public UILabel weight;

	public GameObject entryRoot;
	public GameObject dividerRoot;
	public UILabel monthLabel;



	public void Refresh(object obj)
	{
		
		if (obj.GetType() == typeof(Weight))
		{
			Weight weightOnTheList = (Weight)obj;
			
			entryRoot.SetActive(true);
			dividerRoot.SetActive(false);
			
			date.text = weightOnTheList.weightDate.ToString("dd.MM");

			if(weightOnTheList.weightUnit == Weight.WeightUnit.metric) weight.text = weightOnTheList.weightUnits + " kg " + weightOnTheList.weightDecimals + " g";
			else weight.text = weightOnTheList.weightUnits + " lb " + weightOnTheList.weightDecimals + " oz";	
		}
		else if (obj.GetType() == typeof(DateTime))
		{
			entryRoot.SetActive(false);
			dividerRoot.SetActive(true);
			
			DateTime month = (DateTime)obj;
			monthLabel.text = month.Date.ToString("MMMM") + ", " + month.Year.ToString();
		}
	}
}
