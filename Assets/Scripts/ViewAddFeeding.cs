using UnityEngine;
using System.Collections;
using System;

public class ViewAddFeeding : UIView {

	public UserManager userManager;

	public UIInput mealHour;
	public UIInput mealMin;
	public UIPopupList mealType;
	public UILabel mealAmountBottle;
	public UILabel mealAmountCup;
	

	public GameObject bottle;
	public IPTextPicker leftBreast;
	public IPTextPicker rightBreast;
	public GameObject breastfeeding;
	
	public GameObject cup;


	public override void Show()
	{
		Debug.Log("ViewAddFeeding show called");
		UpdateTimeInputs();
		base.Show();
	}


	public void SubmitMeal()
	{
		DateTime mealTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(mealHour.value), int.Parse(mealMin.value), DateTime.Now.Second);

		userManager.AddMeal(mealTime, (string)mealType.value);
	}



	//updates time inputs when view is loaded
	public void UpdateTimeInputs()
	{
		int hourNow = DateTime.Now.Hour;
		int minNow = DateTime.Now.Minute;
		mealHour.value = hourNow.ToString();

		if(minNow < 10) mealMin.value = "0" + minNow.ToString();
		else mealMin.value = minNow.ToString();
		
		if(hourNow < 10) mealHour.value = "0" + hourNow.ToString();
	}



	//changes sprites according to specific meal type
	public void ChangeMealSprite()
	{
		if(mealType.value == "Breastmilk" || mealType.value == "Formula") 
		{
			bottle.SetActive(true);
			breastfeeding.SetActive(false);
			cup.SetActive(false);
		}
		else if(mealType.value == "Breastfeed")
		{
			bottle.SetActive(false);
			breastfeeding.SetActive(true);
			cup.SetActive(false);
		}
		else if(mealType.value == "Solids")
		{
			bottle.SetActive(false);
			breastfeeding.SetActive(false);
			cup.SetActive(true);
		}
		
		
		
	}
}
