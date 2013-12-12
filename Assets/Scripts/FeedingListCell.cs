using UnityEngine;
using System.Collections;
using System;

public class FeedingListCell : MonoBehaviour {


	public UILabel time;
	public UILabel foodType;
	public UILabel amount;

	public GameObject entryRoot;
	public GameObject dividerRoot;
	public UILabel weekDayLabel;
	


	public void Refresh(object obj)
	{

		if (obj.GetType() == typeof(Meal))
		{
			Meal mealOnTheList = (Meal)obj;

			entryRoot.SetActive(true);
			dividerRoot.SetActive(false);
			
			time.text = mealOnTheList.time.ToString("HH:mm");
			
			if(mealOnTheList.mealType == Meal.MealType.breastfeed) foodType.text = "Breastfeed";
			else if(mealOnTheList.mealType == Meal.MealType.breastMilk) foodType.text = "Breastmilk";
			else if(mealOnTheList.mealType == Meal.MealType.formula) foodType.text = "Formula";
			else if(mealOnTheList.mealType == Meal.MealType.solidFood) foodType.text = "Solid food";

			if(mealOnTheList.mealType == Meal.MealType.breastfeed)
			{
				int bothBreasts = (int)mealOnTheList.leftAmount + (int)mealOnTheList.rightAmount;

				if(bothBreasts > 59)
				{
					amount.text = bothBreasts / 60 + " h " + bothBreasts % 60 + " " + mealOnTheList.unit;
				}
				else amount.text = bothBreasts + " " + mealOnTheList.unit;
			}
			else amount.text = mealOnTheList.amount.ToString() + " " + mealOnTheList.unit;

		}
		else if (obj.GetType() == typeof(DateTime))
		{
			entryRoot.SetActive(false);
			dividerRoot.SetActive(true);

			DateTime dayOfWeek = (DateTime)obj;
			weekDayLabel.text = dayOfWeek.DayOfWeek.ToString();
		}
	}
	
}
