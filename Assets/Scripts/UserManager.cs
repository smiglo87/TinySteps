using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;
using System;
using System.Text.RegularExpressions;

public class UserManager : MonoBehaviour {
	
	public GUIManager guiManager;
	public ViewManager viewManager;
	public PhotoManager photoManager;
	public BottleController bottleController;
	public BreastfeedController breastfeedController;
	public CupController cupController;
	
	public int currentBaby;
	
	public enum Unit { metric, imperial };
	public Unit userUnit;
	
	public string registerBabyProfilePicturePath = "";
	
	//Main list storing all babies data
	public List<Baby> babies = new List<Baby>();
	
	
	
	void Start()
	{

//#if UNITY_EDITOR
		
//#endif
//#if !UNITY_EDITOR
		
		if(PlayerPrefs.GetString("savedBabies").Length > 0)
		{
			LoadBabies();
		}
		
		if(babies.Count > 0)
		{
			ShowBaby(0);
			viewManager.ToDashboardMainView();
		}
		else
		{
			viewManager.ToWelcomeView();
		}
		
	}
	
	
	
	//Sets user units chosen at welcome screen
	public void SetInitialUnits()
	{
		if(guiManager.initialUnitList.value == "Metric")
		{
			PlayerPrefs.SetString("userUnits", "metric");
			userUnit = Unit.metric;
		}
		else if (guiManager.initialUnitList.value == "Imperial")
		{
			PlayerPrefs.SetString("userUnits", "imperial");
			userUnit = Unit.imperial;
		}
		else Debug.LogError("Initial user units unrecognised", guiManager.initialUnitList);
		

		viewManager.ToBabyRegistrationView();
		
	}
	

	
	//Adding new baby to babies list
	public void AddBaby()
	{

		Baby baby = new Baby();
		
		baby.profilePicture = registerBabyProfilePicturePath;
		baby.babyName = guiManager.babyRegisterNameInput.value;
		
		
		if(guiManager.babyRegisterGenderMale.value)
		{
			baby.gender = Baby.Gender.male;
		}
		else
		{
			baby.gender = Baby.Gender.female;
		}
		
		baby.dateOfBirth = new DateTime(int.Parse(guiManager.babyRegisterDobYear.value), int.Parse(guiManager.babyRegisterDobMonth.value), int.Parse(guiManager.babyRegisterDobDay.value));
		
		if(guiManager.babyRegisterBirthWeightUnits.value.Length > 0)
		{
			if(userUnit == Unit.metric)
			{
				baby.weightUnit = Baby.Unit.metric;
			}
			else
			{
				baby.weightUnit = Baby.Unit.imperial;
			}
			
			baby.bornWeightUnits = int.Parse(guiManager.babyRegisterBirthWeightUnits.value);
			baby.bornWeightDecimals = int.Parse (guiManager.babyRegisterBirthWeightDecimals.value);
		}
		
		
		if(guiManager.babyRegisterBirthLengthUnits.value.Length > 0)
		{
			if(userUnit == Unit.metric)
			{
				baby.lengthUnit = Baby.Unit.metric;
			}
			else
			{
				baby.lengthUnit = Baby.Unit.imperial;
				baby.bornLengthDecimals = int.Parse(guiManager.babyRegisterBirthLengthDecimals.value);
			}
			
			baby.bornLengthUnits = int.Parse(guiManager.babyRegisterBirthLengthUnits.value);
			
		}
		
		babies.Add(baby);
		SaveBabies();
		ShowBaby(babies.Count-1);
		
		
	    viewManager.ToDashboardMainView();
	}
	
	
	//Saving all babies to PlayerPrefs
	public void SaveBabies()
	{
		ArrayList listOfBabies = new ArrayList();
		
		//converting all babies to hashtables
		foreach (Baby baby in babies)
		{
			Debug.Log ("saving baby : " + baby.babyName);
			
			Hashtable ht = new Hashtable();
			ht.Add("babyName", baby.babyName);
			
			if(baby.gender == Baby.Gender.male) ht.Add("gender", "male");
			else ht.Add("gender", "female");
			
			ht.Add("dob", baby.dateOfBirth.ToString());
			
			if(baby.weightUnit == Baby.Unit.metric) 
			{
				ht.Add("weightUnits", "metric");
				ht.Add("kg", baby.bornWeightUnits);
				ht.Add("g", baby.bornWeightDecimals);
			}
			else 
			{
				ht.Add("weightUnits", "imperial");
				ht.Add("lb", baby.bornWeightUnits);
				ht.Add("oz", baby.bornWeightDecimals);
			}
			
			if(baby.lengthUnit == Baby.Unit.metric) 
			{
				ht.Add("lengthUnits", "metric");
				ht.Add("cm", baby.bornLengthUnits);
			}
			else
			{
				ht.Add ("lengthUnits", "imperial");
				ht.Add ("ft", baby.bornLengthUnits);
				ht.Add("inch", baby.bornLengthDecimals);
			}
			
			ht.Add("profilePicture", baby.profilePicture);
			
			//Meals
			ArrayList listOfMeals = new ArrayList();
			
			foreach(Meal meal in baby.meals)
			{
				Hashtable mealht = new Hashtable();
				mealht.Add("mealTime", meal.time.ToString());
				
				if(meal.mealType == Meal.MealType.breastfeed)
				{
					mealht.Add("foodType", "breastfeed");
					mealht.Add ("mealUnits", "min");
					mealht.Add("leftAmount", meal.leftAmount);
					mealht.Add("rightAmount", meal.rightAmount);
				}
				else if(meal.mealType == Meal.MealType.breastMilk)
				{
					mealht.Add("foodType", "breastMilk");
					if(userUnit == Unit.metric) mealht.Add("mealUnits", "ml");
					else mealht.Add("mealUnits", "oz");
					mealht.Add("mealAmount", meal.amount);
				}
				else if(meal.mealType == Meal.MealType.formula)
				{
					mealht.Add("foodType", "formula");
					if(userUnit == Unit.metric) mealht.Add("mealUnits", "ml");
					else mealht.Add("mealUnits", "oz");
					mealht.Add("mealAmount", meal.amount);
				}
				else if(meal.mealType == Meal.MealType.solidFood)
				{
					mealht.Add("foodType", "solidFood");
					mealht.Add("mealUnits", "g");	
					mealht.Add("mealAmount", meal.cupAmount);
				}
				
				listOfMeals.Add(mealht);	
			}
			
			//adding list of meals to specific baby
			ht.Add("mealList", listOfMeals);



			//Nappy
			ArrayList listOfNappies = new ArrayList();

			foreach(Nappy nappy in baby.nappies)
			{
				Hashtable nappyht = new Hashtable();
				nappyht.Add("nappyTime", nappy.nappyTime.ToString());

				if(nappy.nappyType == Nappy.NappyType.Wet) nappyht.Add("nappyType", "wet");
				else if(nappy.nappyType == Nappy.NappyType.Stool) nappyht.Add("nappyType", "stool");
				else if(nappy.nappyType == Nappy.NappyType.Both) nappyht.Add("nappyType", "both");

				listOfNappies.Add(nappyht);
			}

			//adding list of nappies to specific baby
			ht.Add("nappyList", listOfNappies);


			//Sleeping
			ArrayList listOfSleeps = new ArrayList();

			foreach(Sleeping sleep in baby.sleeps)
			{
				Hashtable sleepHt = new Hashtable();

				if(guiManager.noFinishCheckmark.value)
				{
					sleepHt.Add("startTime", sleep.startTime.ToString());
					sleepHt.Add("finishTime", "notFinished");
				}
				else
				{
					sleepHt.Add("startTime", sleep.startTime.ToString());
					sleepHt.Add("finishTime", sleep.finishTime.ToString());
				}
				listOfSleeps.Add(sleepHt);

			}

			ht.Add("sleepingList", listOfSleeps);


			//Weight
			ArrayList listOfWeights = new ArrayList();

			foreach(Weight weight in baby.weights)
			{
				Hashtable weightHt = new Hashtable();

				weightHt.Add("weightDate", weight.weightDate.ToString());

				if(weight.weightUnit == Weight.WeightUnit.metric)
				{
					weightHt.Add("weightUnit", "metric");
					weightHt.Add("kg", weight.weightUnits);
					weightHt.Add("g", weight.weightDecimals);
				}
				else if(weight.weightUnit == Weight.WeightUnit.imperial)
				{
					weightHt.Add("weightUnit", "imperial");
					weightHt.Add("lb", weight.weightUnits);
					weightHt.Add("oz", weight.weightDecimals);
				}
				listOfWeights.Add(weightHt);
			}

			ht.Add("weightList", listOfWeights);



			//Length
			ArrayList listOfLengths = new ArrayList();
			
			foreach(Length length in baby.lengths)
			{
				Hashtable lengthHt = new Hashtable();
				
				lengthHt.Add("lengthDate", length.lengthDate.ToString());
				
				if(length.lengthUnit == Length.LengthUnit.metric)
				{
					lengthHt.Add("lengthUnit", "metric");
					lengthHt.Add("cm",length.lengthUnits);

				}
				else if(length.lengthUnit == Length.LengthUnit.imperial)
				{
					lengthHt.Add("lengthUnit", "imperial");
					lengthHt.Add("ft", length.lengthUnits);
					lengthHt.Add("inch", length.lengthDecimals);
				}
				listOfLengths.Add(lengthHt);
			}
			
			ht.Add("lengthList", listOfLengths);

			//adding specific baby to arraylist
			listOfBabies.Add(ht);
			
			
			
		}	
		
		string json = JSON.JsonEncode(listOfBabies);
		Debug.Log(json);
		
		
		//saves all babies info on device
		PlayerPrefs.SetString("savedBabies", json);	
		
	}
	
	
	
	//Load babies from PlayerPrefs to babies list
	public void LoadBabies()
	{
		string json = PlayerPrefs.GetString("savedBabies");
		
		//Creates arraylist(listOfBabies) and assigns it decoded Json(json) to casted ArrayList(in brackets stating before Json)
		ArrayList listOfBabies = (ArrayList)JSON.JsonDecode(json);
		
		
		//Loop going through all babies on the list 
		for(int i = 0; i < listOfBabies.Count; i++)
		{
			Hashtable babyHt = (Hashtable)listOfBabies[i];
			Baby tempBaby = new Baby();
			
			tempBaby.babyName = (string)babyHt["babyName"];
		
		
			if((string)babyHt["gender"] == "male") tempBaby.gender = Baby.Gender.male;
			else tempBaby.gender = Baby.Gender.female;
			
			tempBaby.dateOfBirth = DateTime.Parse((string)babyHt["dob"]);
	
			if((string)babyHt["weightUnits"] == "metric")
			{
				tempBaby.weightUnit = Baby.Unit.metric;
				tempBaby.bornWeightUnits = (int)(double)babyHt["kg"];
				tempBaby.bornWeightDecimals = (int)(double)babyHt["g"];
			}
			else
			{
				tempBaby.weightUnit = Baby.Unit.imperial;
				tempBaby.bornWeightUnits = (int)(double)babyHt["lb"];
				tempBaby.bornWeightDecimals = (int)(double)babyHt["oz"];
			}
			
			if((string)babyHt["lengthUnits"] == "metric")
			{
				tempBaby.lengthUnit = Baby.Unit.metric;
				tempBaby.bornLengthUnits = (int)(double)babyHt["cm"];
			}
			else
			{
				tempBaby.lengthUnit = Baby.Unit.imperial;
				tempBaby.bornLengthUnits = (int)(double)babyHt["ft"];
				tempBaby.bornLengthDecimals = (int)(double)babyHt["inch"];
			}
			
			tempBaby.profilePicture = (string)babyHt["profilePicture"];



			//loading mealList for specific baby
			ArrayList listOfMeals = (ArrayList)babyHt["mealList"];
			
			
			for(int m = 0; m < listOfMeals.Count; m++)
			{
				Hashtable mealHt = (Hashtable)listOfMeals[m];
				Meal tempMeal = new Meal();
				
				tempMeal.time = DateTime.Parse((string)mealHt["mealTime"]);

				if((string)mealHt["foodType"] == "breastfeed")
				{
					tempMeal.mealType = Meal.MealType.breastfeed;
					tempMeal.leftAmount = (float)(double)mealHt["leftAmount"];
					tempMeal.rightAmount = (float)(double)mealHt["rightAmount"];
				}
				else if((string)mealHt["foodType"] == "breastMilk")
				{
					tempMeal.mealType = Meal.MealType.breastMilk;
					tempMeal.amount = (float)(double)mealHt["mealAmount"];
				}
				else if((string)mealHt["foodType"] == "formula")
				{
					tempMeal.mealType = Meal.MealType.formula;
					tempMeal.amount = (float)(double)mealHt["mealAmount"];
				}
				else if((string)mealHt["foodType"] == "solidFood")
				{
					tempMeal.mealType = Meal.MealType.solidFood;
					tempMeal.amount = (float)(double)mealHt["mealAmount"];
				}

				
				if((string)mealHt["mealUnits"] == "ml") tempMeal.unit = Meal.UnitType.ml;
				else if((string)mealHt["mealUnits"] == "oz") tempMeal.unit = Meal.UnitType.oz;
				else if((string)mealHt["mealUnits"] == "min") tempMeal.unit = Meal.UnitType.min;
				else if((string)mealHt["mealUnits"] == "g") tempMeal.unit = Meal.UnitType.g;
				
				tempBaby.meals.Add(tempMeal);
			}


			//loading nappyList for specific baby
			ArrayList listOfNappies = (ArrayList)babyHt["nappyList"];

			for(int n = 0; n < listOfNappies.Count; n++)
			{
				Hashtable nappyHT = (Hashtable)listOfNappies[n];
				Nappy tempNappy = new Nappy();

				tempNappy.nappyTime = DateTime.Parse((string)nappyHT["nappyTime"]);

				if((string)nappyHT["nappyType"] == "wet") tempNappy.nappyType = Nappy.NappyType.Wet;
				else if((string)nappyHT["nappyType"] == "stool") tempNappy.nappyType = Nappy.NappyType.Stool;
				else if((string)nappyHT["nappyType"] == "both") tempNappy.nappyType = Nappy.NappyType.Both;

				tempBaby.nappies.Add(tempNappy);

			}
			
			//loading sleepingList for specific baby
			ArrayList listOfSleeps = (ArrayList)babyHt["sleepingList"];

			for(int s = 0; s < listOfSleeps.Count; s++)
			{
				Hashtable sleepingHt = (Hashtable)listOfSleeps[s];
				Sleeping tempSleep = new Sleeping();

				tempSleep.startTime = DateTime.Parse((string)sleepingHt["startTime"]);

				if((string)sleepingHt["finishTime"] != "notFinished") tempSleep.finishTime = DateTime.Parse((string)sleepingHt["finishTime"]);


				tempBaby.sleeps.Add(tempSleep);
			}

			//loading weightList for specific baby
			ArrayList listOfWeights = (ArrayList)babyHt["weightList"];

			for(int w = 0; w < listOfWeights.Count; w++)
			{
				Hashtable weightHt = (Hashtable)listOfWeights[w];
				Weight tempWeight = new Weight();

				tempWeight.weightDate = DateTime.Parse((string)weightHt["weightDate"]);

				if((string)weightHt["weightUnit"] == "metric")
				{
					tempWeight.weightUnit = Weight.WeightUnit.metric;
					tempWeight.weightUnits = (int)(double)weightHt["kg"];
					tempWeight.weightDecimals = (int)(double)weightHt["g"];
				}
				else if((string)weightHt["weightUnit"] == "imperial")
				{
					tempWeight.weightUnit = Weight.WeightUnit.imperial;
					tempWeight.weightUnits = (int)(double)weightHt["lb"];
					tempWeight.weightDecimals = (int)(double)weightHt["oz"];
				}
				tempBaby.weights.Add(tempWeight);
			}


			//loading lengthList for specific baby
			ArrayList listOfLengths = (ArrayList)babyHt["lengthList"];
			
			for(int l = 0; l < listOfLengths.Count; l++)
			{
				Hashtable lengthHt = (Hashtable)listOfLengths[l];
				Length tempLength = new Length();
				
				tempLength.lengthDate = DateTime.Parse((string)lengthHt["lengthDate"]);
				
				if((string)lengthHt["lengthUnit"] == "metric")
				{
					tempLength.lengthUnit = Length.LengthUnit.metric;
					tempLength.lengthUnits = (float)(double)lengthHt["cm"];
				}
				else if((string)lengthHt["lengthUnit"] == "imperial")
				{
					tempLength.lengthUnit = Length.LengthUnit.imperial;
					tempLength.lengthUnits = (float)(double)lengthHt["ft"];
					tempLength.lengthDecimals = (float)(double)lengthHt["inch"];
				}
				tempBaby.lengths.Add(tempLength);
			}
			babies.Add(tempBaby);
		}
	}
	


	//Showing specific baby from the babiesList in Dashboard
	public void ShowBaby(int babyIndex)
	{
		if (babies[babyIndex].profilePicture != "") StartCoroutine(photoManager.GetTexture(babies[babyIndex].profilePicture, value => guiManager.dashboardBabyProfilePicture.mainTexture = value));
		else guiManager.dashboardBabyProfilePicture.mainTexture = guiManager.babyRegisterNoAvatarPicture;

		guiManager.dashboardBabyname.text = babies[babyIndex].babyName;
		guiManager.dashboardBabyAge.text = babies[babyIndex].GetAge();
	
		string[] words = babies[babyIndex].GetAge().Split(',');
		
		//format baby age
		if(words[0].ToString() == "1")
		{
			if(words[1].ToString() == "1")
			{
				guiManager.dashboardBabyAge.text = "is " + words[0].ToString() + " year and " + words[1].ToString() + " month old today";
			}
			else
			{
				guiManager.dashboardBabyAge.text = "is " + words[0].ToString() + " year and " + words[1].ToString() + " months old today";
			}
		}
		else
		{
			if(words[1].ToString () == "1")
			{
				guiManager.dashboardBabyAge.text = "is " + words[0].ToString() + " years and " + words[1].ToString() + " month old today";
			}
			else
			{
				guiManager.dashboardBabyAge.text = "is " + words[0].ToString() + " years and " + words[1].ToString() + " months old today";
			}
		}
		
		currentBaby = babyIndex;

		guiManager.MealListRefresh();
		guiManager.NappyListRefresh();
		guiManager.SleepingListRefresh();
		guiManager.WeightListRefresh();
		guiManager.LengthListRefresh();
	}


	
	public void OnSwipe(SwipeGesture gesture)
	{
		if(viewManager.currentView == viewManager.dashboardMainView)
		{
			if(gesture.Direction == FingerGestures.SwipeDirection.Left)
			{
				NextBaby();
			}
			else if(gesture.Direction == FingerGestures.SwipeDirection.Right)
			{
				PreviousBaby();
			}
		}
	}
	
	
	
	public void NextBaby()
	{
		if(currentBaby == babies.Count-1)
		{
			ShowBaby(0);
		}
		else
		{
			ShowBaby(currentBaby+1);
		}
	}
	
	public void PreviousBaby()
	{
		
		if(currentBaby > 0)
		{
			ShowBaby(currentBaby-1);
		}
		else
		{
			ShowBaby(babies.Count-1);
		}
	}
	

	
	public void AddMeal()
	{
		
		Meal meal = new Meal();
		
		meal.time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(guiManager.feedingHour.value), int.Parse(guiManager.feedingMin.value), DateTime.Now.Second);
		

		if(guiManager.foodType.value == "Breastfeed")
		{
			meal.mealType = Meal.MealType.breastfeed;
			meal.unit = Meal.UnitType.min;
			breastfeedController.AddBreastfeedAmount();
			meal.leftAmount = breastfeedController.leftAmount;
			meal.rightAmount = breastfeedController.rightAmount;
		}
		else if(guiManager.foodType.value == "Breastmilk")
		{
			meal.mealType = Meal.MealType.breastMilk;
			meal.amount = bottleController.currentAmount;
			if(userUnit == Unit.metric)
			{
				meal.unit = Meal.UnitType.ml;
			}
			else meal.unit = Meal.UnitType.oz;
		}
		else if(guiManager.foodType.value == "Formula")
		{
			meal.mealType = Meal.MealType.formula;
			meal.amount = bottleController.currentAmount;
			if(userUnit == Unit.metric)
			{
				meal.unit = Meal.UnitType.ml;
			}
			else meal.unit = Meal.UnitType.oz;
		}
		else if (guiManager.foodType.value == "Solids")
		{
			meal.mealType = Meal.MealType.solidFood;
			meal.unit = Meal.UnitType.g; 
			meal.amount = cupController.currentAmount;
		}
		else Debug.LogError("Food type not recognised");
		
		babies[currentBaby].meals.Add(meal);
		
		SaveBabies();
		guiManager.MealListRefresh();
		viewManager.ToTrackerFeedingListView();
	}

	
	public void AddNappy()
	{
		Nappy nappy  =  new Nappy();

		nappy.nappyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(guiManager.nappyHour.value), int.Parse(guiManager.nappyMin.value), DateTime.Now.Second);

		if(guiManager.nappyType.value == "Wet") nappy.nappyType = Nappy.NappyType.Wet;
		else if(guiManager.nappyType.value == "Stool") nappy.nappyType = Nappy.NappyType.Stool;
		else if(guiManager.nappyType.value == "Both") nappy.nappyType = Nappy.NappyType.Both;
		else Debug.LogError("Nappy type not recognised");

		babies[currentBaby].nappies.Add(nappy);

		SaveBabies();
		guiManager.NappyListRefresh();
		viewManager.ToTrackerNappyListView();
	}
	

	public void AddSleeping()
	{
		Sleeping sleeping = new Sleeping();

		if(guiManager.noFinishCheckmark.value)
		{
			sleeping.startTime = new DateTime(DateTime.Now.Year, int.Parse(guiManager.startMonth.value), int.Parse(guiManager.startDay.value), int.Parse(guiManager.startHour.value), int.Parse(guiManager.startMin.value), DateTime.Now.Second);
		}
		else
		{
			sleeping.startTime = new DateTime(DateTime.Now.Year, int.Parse(guiManager.startMonth.value), int.Parse(guiManager.startDay.value), int.Parse(guiManager.startHour.value), int.Parse(guiManager.startMin.value), DateTime.Now.Second);
			sleeping.finishTime = new DateTime(DateTime.Now.Year, int.Parse(guiManager.finishMonth.value), int.Parse(guiManager.finishDay.value), int.Parse(guiManager.finishHour.value), int.Parse(guiManager.finishMin.value), DateTime.Now.Second);
		}

		babies[currentBaby].sleeps.Add(sleeping);

		SaveBabies();
		guiManager.SleepingListRefresh();
		viewManager.ToTrackerSleepingListView();
	}


	public void AddEndOfSleeping()
	{

	}



	public void AddWeight()
	{
		Weight weight = new Weight();

		weight.weightDate = new DateTime(DateTime.Now.Year, int.Parse(guiManager.weightMonth.value), int.Parse(guiManager.weightDay.value));

		if(guiManager.weightUnits.value.Length > 0)
		{
			if(userUnit == Unit.metric)
			{
				weight.weightUnit = Weight.WeightUnit.metric;
			}
			else
			{
				weight.weightUnit = Weight.WeightUnit.imperial;
			}
			
			weight.weightUnits = int.Parse(guiManager.weightUnits.value);
			weight.weightDecimals = int.Parse (guiManager.weightDecimals.value);
		}

		babies[currentBaby].weights.Add(weight);

		SaveBabies();
		guiManager.WeightListRefresh();
		viewManager.ToGrowthWeightListView();
	}


	
	public void AddLength()
	{
		Length length = new Length();
		
		length.lengthDate = new DateTime(DateTime.Now.Year, int.Parse(guiManager.lengthMonth.value), int.Parse(guiManager.lengthDay.value));
		
		if(guiManager.lengthUnits.value.Length > 0)
		{
			if(userUnit == Unit.metric)
			{
				length.lengthUnit = Length.LengthUnit.metric;
			}
			else
			{
				length.lengthUnit = Length.LengthUnit.imperial;
				length.lengthDecimals = float.Parse(guiManager.lengthDecimals.value);
			}
			
			length.lengthUnits = float.Parse(guiManager.lengthUnits.value);

		}
		
		babies[currentBaby].lengths.Add(length);
		
		SaveBabies();
		guiManager.LengthListRefresh();
		viewManager.ToGrowthLengthListView();
	}

}