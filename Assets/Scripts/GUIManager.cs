using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GUIManager : MonoBehaviour {
	
	public ViewManager viewManager;
	public UserManager userManager;

	//Welcome
	public UIPopupList initialUnitList;
	
	//Baby Registration
	public UITexture babyRegisterProfilePicture;
	public Texture2D babyRegisterNoAvatarPicture;
	public UIInput babyRegisterNameInput;
	public UIToggle babyRegisterGenderMale;
	public UIInput babyRegisterDobDay;
	public UIInput babyRegisterDobMonth;
	public UIInput babyRegisterDobYear;
	
	public UIInput babyRegisterBirthWeightUnits;
	public UIInput babyRegisterBirthWeightDecimals;
	
	public UIInput babyRegisterBirthLengthUnits;
	public UIInput babyRegisterBirthLengthDecimals;
	
	//Dashboard
	public UILabel dashboardBabyname;
	public UILabel dashboardBabyAge;
	public UITexture dashboardBabyProfilePicture;
	

	//LoadTrackerLabels
	public UILabel lastMealTime;
	public UILabel lastMealAmount;
	public UILabel mealTimeSince;

	public UILabel lastNappyTime;
	public UILabel lastNappyType;
	public UILabel nappyTimeSince;

	public UILabel lastSleepingTime;
	public UILabel lastSleepingDuration;
	public UILabel sleepingTimeSince;


	//AddFeeding
	public UIInput feedingHour;
	public UIInput feedingMin;
	public UIPopupList foodType;
	public UILabel mealAmount;
	public UIList mealsList;

	public GameObject bottle;
	public IPTextPicker leftBreast;
	public IPTextPicker rightBreast;
	public GameObject breastfeeding;

	public GameObject cup;


	//AddNappy
	public UIInput nappyHour;
	public UIInput nappyMin;
	public UIPopupList nappyType;
	public UIList nappiesList;


	//AddSleeping
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

	public UIList sleepList;



	//GrowthLabelsRefresh
	public UILabel lastWeightDate;
	public UILabel lastWeight;
	public UILabel weightTimeSince;

	public UILabel lastLengthDate;
	public UILabel lastLength;
	public UILabel lengthTimeSince;

	//AddWeight
	public UIInput weightDay;
	public UIInput weightMonth;
	public UIInput weightUnits;
	public UIInput weightDecimals;

	public UIList weightsList;


	//AddLength
	public UIInput lengthDay;
	public UIInput lengthMonth;
	public UIInput lengthUnits;
	public UIInput lengthDecimals;

	public UIList lengthsList;


	//fills labels in register form if only one unit is filled (only kg or g)
	public void FillRegisterWeight()
	{

		if(babyRegisterBirthWeightUnits.value == "kg" || babyRegisterBirthWeightUnits.value == "lb" || babyRegisterBirthWeightUnits.value == "" ) babyRegisterBirthWeightUnits.value = "0";

		if(babyRegisterBirthWeightDecimals.value == "g" || babyRegisterBirthWeightDecimals.value == "oz" || babyRegisterBirthWeightDecimals.value == "") babyRegisterBirthWeightDecimals.value = "00";
	}
	

	//fills labels in register form if only one unit is filled(only ft or inch)
	public void FillRegisterLength()
	{
		if(babyRegisterBirthLengthUnits.value == "ft" || babyRegisterBirthLengthUnits.value == "") babyRegisterBirthLengthUnits.value = "0";

		if(babyRegisterBirthLengthDecimals.value == "inch" || babyRegisterBirthLengthDecimals.value == "") babyRegisterBirthLengthDecimals.value = "00";
	}

	public void FillWeight()
	{
		if(weightUnits.value == "kg" || weightUnits.value == "lb" || weightUnits.value == "") weightUnits.value = "0";

		if(weightDecimals.value == "g" || weightDecimals.value == "oz" || weightDecimals.value == "") weightDecimals.value = "00";
	}


	public void FillLength()
	{
		if(lengthUnits.value == "cm" || lengthUnits.value == "ft" || lengthUnits.value == "") lengthUnits.value = "0";
		
		if(lengthDecimals.value == "inch" || lengthDecimals.value == "") lengthDecimals.value = "00";
	}



	public void LengthUnitChanged()
	{
		if(userManager.userUnit == UserManager.Unit.metric)
		{
			babyRegisterBirthLengthDecimals.gameObject.SetActive(false);
			lengthDecimals.gameObject.SetActive(false);
		}
		else babyRegisterBirthLengthDecimals.gameObject.SetActive(true);
	}

	
	public void LabelLengthUnitChange()
	{
		if(userManager.userUnit == UserManager.Unit.metric)
		{
			babyRegisterBirthLengthUnits.label.text = "cm";
			lengthUnits.label.text = "cm";
		}
		else
		{
			babyRegisterBirthLengthUnits.label.text = "ft";
			babyRegisterBirthLengthDecimals.label.text = "inch";
			lengthDecimals.label.text = "inch";
			lengthUnits.label.text = "ft";
		}
	}
	
	
	public void LabelWeightUnitChange()
	{
		if(userManager.userUnit == UserManager.Unit.metric)
		{
			babyRegisterBirthWeightUnits.label.text = "kg";
			babyRegisterBirthWeightDecimals.label.text = "g";
			weightUnits.label.text = "kg";
			weightDecimals.label.text = "g";
		}
		else if(userManager.userUnit == UserManager.Unit.imperial)
		{
			babyRegisterBirthWeightUnits.label.text = "lb";
			babyRegisterBirthWeightDecimals.label.text = "oz";
			weightUnits.label.text = "lb";
			weightDecimals.label.text = "oz";
		}
	}
	
	public void LabelWeightClearing()
	{
		weightUnits.value = "";
		weightDecimals.value = "";
	}


	public void LabelLengthClearing()
	{
		lengthUnits.value = "";
		lengthDecimals.value = "";
	}



	public void SleepingNoFinishTimeCheckmarkChange()
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




	
	public void ShowError(string title, string message)
	{
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title, message, new string[] {"OK"} );		
		Debug.Log (message);
	}

	public void UpdateTimeInputs()
	{
		int hourNow = DateTime.Now.Hour;
		int minNow = DateTime.Now.Minute;
		feedingHour.value = hourNow.ToString();
		if(minNow < 10) feedingMin.value = "0" + minNow.ToString();
		else feedingMin.value = minNow.ToString();

		if(hourNow < 10) feedingHour.value = "0" + hourNow.ToString();
	}

	public void UpdateTimeInputsNappy()
	{
		int hourNow = DateTime.Now.Hour;
		int minNow = DateTime.Now.Minute;

		if(minNow < 10) nappyMin.value = "0" + minNow.ToString();
		else nappyMin.value = minNow.ToString();

		if(hourNow < 10) nappyHour.value = "0" + hourNow.ToString();
		else nappyHour.value = hourNow.ToString();
	}

	public void UpdateTimeInputsSleeping()
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
	
	public void UpdateTimeInputsWeight()
	{
		int dayNow = DateTime.Now.Day;
		int monthNow = DateTime.Now.Month;

		if(dayNow < 10) weightDay.value = "0" + dayNow.ToString();
		else weightDay.value = dayNow.ToString();

		if(monthNow < 10) weightMonth.value = "0" + monthNow.ToString();
		else weightMonth.value = monthNow.ToString();
	}


	public void UpdateTimeInputsLength()
	{
		int dayNow = DateTime.Now.Day;
		int monthNow = DateTime.Now.Month;
		
		if(dayNow < 10) lengthDay.value = "0" + dayNow.ToString();
		else lengthDay.value = dayNow.ToString();
		
		if(monthNow < 10) lengthMonth.value = "0" + monthNow.ToString();
		else lengthMonth.value = monthNow.ToString();
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
			if (timeAgo.Days <= 7)
			{
				recentMeals.Add(meal);
			}
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

				lastMealTime.text = "Last: " + recentMeals[i].time.ToString("dd.MM") + " at " + recentMeals[i].time.ToString("HH:mm");
				lastMealAmount.text = "Amount: " + recentMeals[i].amount.ToString() + " " + recentMeals[i].unit;
				TimeSpan timeSince = DateTime.Now - recentMeals[i].time;
				
				mealTimeSince.text = "Time since: \n" + timeSince.Days.ToString() + " days " + timeSince.Hours.ToString() + " h " + timeSince.Minutes.ToString() + " min";
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
					else
					{
						dividedList.Add(sortedList[e]);
					}
				}
				//last one on the list
				else
				{
					dividedList.Add(sortedList[e]);
				}
			}
		}

		mealsList.BuildList(dividedList);

	}


	public void ChangeMealTypeSprite()
	{
		if(foodType.value == "Breastmilk" || foodType.value == "Formula") 
		{
			bottle.SetActive(true);
			breastfeeding.SetActive(false);
			cup.SetActive(false);
		}
		else if(foodType.value == "Breastfeed")
		{
			bottle.SetActive(false);
			breastfeeding.SetActive(true);
			cup.SetActive(false);
		}
		else if(foodType.value == "Solids")
		{
			bottle.SetActive(false);
			breastfeeding.SetActive(false);
			cup.SetActive(true);
		}



	}


	public void NappyListRefresh()
	{
		ArrayList nappyList = new ArrayList(userManager.babies[userManager.currentBaby].nappies);

		List<Nappy> recentNappies = new List<Nappy>();
		
		//loop going through all position in nappy list
		foreach (Nappy nappy in nappyList)
		{
			//checking time difference between specific nappy and now
			TimeSpan timeAgo = nappy.nappyTime - DateTime.Now;
			//catching recent meals
			if (timeAgo.Days <= 7)
			{
				recentNappies.Add(nappy);
			}
		}
		//at this point we have recent 7 days of nappy
		//sorting nappies by date

		//declaring sorted list
		List<Nappy> sortedList = new List<Nappy>();
		//adding first entry to have someting to compare to
		if (recentNappies.Count > 0)  sortedList.Add (recentNappies[0]);
		
		//loop comparing each object with all in sorted list, starting loop from position 1 not 0 as we use first entry to compare to
		for (int n=1; n<recentNappies.Count; n++)
		{
			//declaring this variable to store position on the list if found
			int finalIndex = -1;
			
			//another loop going through sorted positions itself
			for (int s=0; s<sortedList.Count; s++)
			{
				if (finalIndex == -1)
				{
					if (DateTime.Compare(recentNappies[n].nappyTime, sortedList[s].nappyTime) < 0) //earlier
					{
						finalIndex = s;
						//insert i meal to specific place
						sortedList.Insert(finalIndex, recentNappies[n]);
					}
				}
			}
			//inside later entry not found so adding in the end of sorted list
			if (finalIndex == -1) 
			{
				sortedList.Add(recentNappies[n]);
			}
		}

		//Insert dividers
		ArrayList dividedList = new ArrayList();
		
		if (sortedList.Count > 0) 
		{
			dividedList.Add(sortedList[0].nappyTime);
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
					if (sortedList[e].nappyTime.Day != sortedList[e+1].nappyTime.Day)
					{
						dividedList.Add(sortedList[e]);
						dividedList.Add(sortedList[e+1].nappyTime);
					}
					else
					{
						dividedList.Add(sortedList[e]);
					}
				}
				//last one on the list
				else
				{
					dividedList.Add(sortedList[e]);
				}
			}
		}
		nappiesList.BuildList(dividedList);
	
	}


	public void SleepingListRefresh()
	{
		ArrayList sleepingList = new ArrayList(userManager.babies[userManager.currentBaby].sleeps);

		List<Sleeping> recentSleeps = new List<Sleeping>();

		//loop going through all position in sleeping list
		foreach(Sleeping sleep in sleepingList)
		{
			//checking time difference between specific sleep and now
			TimeSpan timeAgo = sleep.startTime - DateTime.Now;
			if(timeAgo.Days <= 7) recentSleeps.Add(sleep);

		}

		List<Sleeping> sortedList = new List<Sleeping>();

		if(recentSleeps.Count > 0) sortedList.Add(recentSleeps[0]);

		//loop comparing each object with all in sorted list, starting loop from position 1 not 0 as we use first entry to compare to
		for (int m = 1; m < recentSleeps.Count; m++)
		{
			//declaring this variable to store position on the list if found
			int finalIndex = -1;
			
			//another loop going through sorted positions itself
			for (int s=0; s<sortedList.Count; s++)
			{
				if (finalIndex == -1)
				{
					if (DateTime.Compare(recentSleeps[m].startTime, sortedList[s].startTime) < 0) //earlier
					{
						finalIndex = s;
						//insert i meal to specific place
						sortedList.Insert(finalIndex, recentSleeps[m]);

					}
				}
			}
			//inside later entry not found so adding in the end of sorted list
			if (finalIndex == -1)
			{
				sortedList.Add(recentSleeps[m]);
			}
		}

		//Insert dividers
		ArrayList dividedList = new ArrayList();

		if (sortedList.Count > 0) 
		{
			dividedList.Add(sortedList[0].startTime);
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
					if (sortedList[e].startTime.Day != sortedList[e+1].startTime.Day)
					{
						dividedList.Add(sortedList[e]);
						dividedList.Add(sortedList[e+1].startTime);
					}
					else
					{
						dividedList.Add(sortedList[e]);
					}
				}
				//last one on the list
				else
				{
					dividedList.Add(sortedList[e]);
				}
			}
		}
		sleepList.BuildList(dividedList);
	}




	public void WeightListRefresh()
	{
		ArrayList weightList = new ArrayList(userManager.babies[userManager.currentBaby].weights);

		List<Weight> recentWeights = new List<Weight>();

		foreach(Weight weight in weightList)
		{
			TimeSpan timeAgo = weight.weightDate - DateTime.Now;

			recentWeights.Add(weight);
		}

		List<Weight> sortedList = new List<Weight>();




		if(recentWeights.Count > 0)
		{
			sortedList.Add(recentWeights[0]);
		}

		for (int w=1; w<recentWeights.Count; w++)
		{
			int finalIndex = -1;

			for(int p=0; p<sortedList.Count; p++)
			{
				if (finalIndex == -1)
				{
					if (DateTime.Compare(recentWeights[w].weightDate, sortedList[p].weightDate) < 0) //earlier
					{
						finalIndex = p;
						//insert i meal to specific place
						sortedList.Insert(finalIndex, recentWeights[w]);
					}
				}
			}
			//inside later entry not found so adding in the end of sorted list
			if (finalIndex == -1)
			{
				sortedList.Add(recentWeights[w]);


			}


		}
		//Insert dividers
		ArrayList dividedList = new ArrayList();

		if(userManager.babies[userManager.currentBaby].bornWeightUnits != 0 && userManager.babies[userManager.currentBaby].bornWeightDecimals != 0)
		{
			Weight birthWeight = new Weight();
			
			birthWeight.weightDate = userManager.babies[userManager.currentBaby].dateOfBirth;
			dividedList.Insert(0, birthWeight.weightDate);
			
		}

		if (sortedList.Count > 0) 
		{
			dividedList.Add(sortedList[0].weightDate);
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
					if (sortedList[e].weightDate.Month != sortedList[e+1].weightDate.Month)
					{
						dividedList.Add(sortedList[e]);
						dividedList.Add(sortedList[e+1].weightDate);
					}
					else
					{
						dividedList.Add(sortedList[e]);
					}
				}
				//last one on the list
				else
				{
					dividedList.Add(sortedList[e]);
				}
			}
		}

		if(userManager.babies[userManager.currentBaby].bornWeightUnits != 0 && userManager.babies[userManager.currentBaby].bornWeightDecimals != 0)
		{


			Weight birthWeight = new Weight();

			birthWeight.weightDate = userManager.babies[userManager.currentBaby].dateOfBirth;
			birthWeight.weightUnits = userManager.babies[userManager.currentBaby].bornWeightUnits;
			birthWeight.weightDecimals = userManager.babies[userManager.currentBaby].bornWeightDecimals;

			dividedList.Insert(1, birthWeight);

		}


		weightsList.BuildList(dividedList);
	}


	

	public void LengthListRefresh()
	{
		ArrayList lengthList = new ArrayList(userManager.babies[userManager.currentBaby].lengths);
		
		List<Length> recentLengths = new List<Length>();
		
		foreach(Length length in lengthList)
		{
			TimeSpan timeAgo = length.lengthDate - DateTime.Now;
			
			recentLengths.Add(length);
		}
		
		List<Length> sortedList = new List<Length>();
		
		
		
		
		if(recentLengths.Count > 0)
		{
			sortedList.Add(recentLengths[0]);
		}
		
		for (int w=1; w<recentLengths.Count; w++)
		{
			int finalIndex = -1;
			
			for(int p=0; p<sortedList.Count; p++)
			{
				if (finalIndex == -1)
				{
					if (DateTime.Compare(recentLengths[w].lengthDate, sortedList[p].lengthDate) < 0) //earlier
					{
						finalIndex = p;
						//insert i meal to specific place
						sortedList.Insert(finalIndex, recentLengths[w]);
					}
				}
			}
			//inside later entry not found so adding in the end of sorted list
			if (finalIndex == -1)
			{
				sortedList.Add(recentLengths[w]);
	
			}
		
		
		}
		//Insert dividers
		ArrayList dividedList = new ArrayList();


		if(userManager.userUnit == UserManager.Unit.metric)
		{
			if(userManager.babies[userManager.currentBaby].bornLengthUnits != 0)
			{
				Length birthLength = new Length();
				
				birthLength.lengthDate = userManager.babies[userManager.currentBaby].dateOfBirth;
				dividedList.Insert(0, birthLength.lengthDate);
				
			}
		}
		else
		{
			if(userManager.babies[userManager.currentBaby].bornLengthUnits != 0 && userManager.babies[userManager.currentBaby].bornLengthDecimals != 0)
			{
				Length birthLength = new Length();
				
				birthLength.lengthDate = userManager.babies[userManager.currentBaby].dateOfBirth;
				dividedList.Insert(0, birthLength.lengthDate);
				
			}
		}


		if (sortedList.Count > 0) 
		{
			dividedList.Add(sortedList[0].lengthDate);
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
					if (sortedList[e].lengthDate.Month != sortedList[e+1].lengthDate.Month)
					{
						dividedList.Add(sortedList[e]);
						dividedList.Add(sortedList[e+1].lengthDate);
					}
					else
					{
						dividedList.Add(sortedList[e]);
					}
				}
				//last one on the list
				else
				{
					dividedList.Add(sortedList[e]);
				}
			}
		}

		if(userManager.userUnit == UserManager.Unit.metric)
		{
			if(userManager.babies[userManager.currentBaby].bornLengthUnits != 0)
			{
				
				Length birthLength = new Length();
				
				birthLength.lengthDate = userManager.babies[userManager.currentBaby].dateOfBirth;
				birthLength.lengthUnits = userManager.babies[userManager.currentBaby].bornLengthUnits;
				
				dividedList.Insert(1, birthLength);

			}
		}
		else 
		{
			if(userManager.babies[userManager.currentBaby].bornLengthUnits != 0 && userManager.babies[userManager.currentBaby].bornLengthDecimals != 0)
			{
		
				Length birthLength = new Length();
				
				birthLength.lengthDate = userManager.babies[userManager.currentBaby].dateOfBirth;
				birthLength.lengthUnits = userManager.babies[userManager.currentBaby].bornLengthUnits;
				birthLength.lengthDecimals = userManager.babies[userManager.currentBaby].bornLengthDecimals;

				dividedList.Insert(1, birthLength);
			}
		}

		lengthsList.BuildList(dividedList);

	}



}






