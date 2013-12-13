using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ViewFeedingList : UIView {


	public UserManager userManager;
	public ViewTracker viewTracker;

	public UIList mealsList;

	public override void Show()
	{
		MealListRefresh();
		base.Show();
	}

	public void MealListRefresh()
	{
		List<Meal> mealList = userManager.babies[userManager.currentBaby].meals;
		
		//grab last 7 days entries from all meals
		List<Meal> recentMeals = new List<Meal>();
		
		//loop going through all position in meal list
		foreach (Meal meal in mealList)
		{
			//checking time difference between specific meal and now
			TimeSpan timeAgo = meal.time - DateTime.Now;
			//catching recent meals
			if (timeAgo.Days <= 7) recentMeals.Add(meal);
		}
		//at this point we have recent 7 days of meals
		//sorting meals by date
		
		//declaring sorted list
		List<Meal> sortedList = new List<Meal>();
		//adding first entry to have someting to compare to
		if (recentMeals.Count > 0)  sortedList.Add (recentMeals[0]);
		
		//loop comparing each object with all in sorted list, starting loop from position 1 not 0 as we use first entry to compare to
		for (int i=1; i<recentMeals.Count; i++)
		{
			//declaring this variable to store position on the list if found
			int finalIndex = -1;
			
			//another loop going through sorted positions itself
			for (int s=0; s<sortedList.Count; s++)
			{
				if (finalIndex == -1)
				{
					if (DateTime.Compare(recentMeals[i].time, sortedList[s].time) < 0) //earlier
					{
						finalIndex = s;
						//insert i meal to specific place
						sortedList.Insert(finalIndex, recentMeals[i]);
					}
				}
			}
			//inside later entry not found so adding in the end of sorted list
			if (finalIndex == -1) 
			{
				sortedList.Add(recentMeals[i]);
				//sends last meal info to tracker view
				viewTracker.UpdateLastMealLabels(recentMeals[i].time, recentMeals[i].amount, recentMeals[i].unit); 
			}
		}

		//Insert dividers
		ArrayList dividedList = new ArrayList();
		
		if (sortedList.Count > 0) 
		{
			dividedList.Add(sortedList[0].time);
			dividedList.Add(sortedList[0]);
		}
		
		if (sortedList.Count > 1)
		{
			//loop comparing pairs of entries
			for (int e=0; e<sortedList.Count; e++)
			{
				if (e < sortedList.Count-1)
				{
					//comparing
					if (sortedList[e].time.Day != sortedList[e+1].time.Day)
					{
						dividedList.Add(sortedList[e]);
						dividedList.Add(sortedList[e+1].time);
					}
					else dividedList.Add(sortedList[e]);
				}
				//last one on the list
				else dividedList.Add(sortedList[e]);
			}
		}
		mealsList.BuildList(dividedList);
	}



}
