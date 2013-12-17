using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;
using System;
using System.Text.RegularExpressions;

public class UserManager : MonoBehaviour {

	//constructs static event
	public delegate void OnBabyChangedHandler();
	public static event OnBabyChangedHandler OnBabyChanged;

	public UIViewController viewController;
	public PhotoManager photoManager;

	public ViewWelcome viewWelcome;
	
	public ViewAddFeeding viewAddFeeding;
	public ViewFeedingList viewFeedingList;
	public BottleController bottleController;
	public BreastfeedController breastfeedController;
	public CupController cupController;

	public ViewAddNappy viewAddNappy;
	public ViewNappyList viewNappyList;

	public ViewAddSleeping viewAddSleeping;
	public ViewSleepingList viewSleepingList;

	public ViewWeightList viewWeightList;
	public ViewAddWeight viewAddWeight;

	public ViewLengthList viewLengthList;
	public ViewAddLength viewAddLength;

	public int currentBaby;
	
	public enum Unit { metric, imperial };
	public Unit userUnit;
	
	public string registerBabyProfilePicturePath = "";
	public string journalEventPicturePath = "";
	
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
			viewController.ToDashboardMainView();
		}
		else
		{
			viewController.ToWelcomeView();
		}
		
	}
	
	
	
	//Sets user units chosen at welcome screen
	public void SetInitialUnits()
	{
		if(viewWelcome.initialUnitList.value == "Metric")
		{
			PlayerPrefs.SetString("userUnits", "metric");
			userUnit = Unit.metric;
		}
		else if (viewWelcome.initialUnitList.value == "Imperial")
		{
			PlayerPrefs.SetString("userUnits", "imperial");
			userUnit = Unit.imperial;
		}
		else Debug.LogError("Initial user units unrecognised", viewWelcome.initialUnitList);
	}
	

	
	//Adding new baby to babies list
	public void AddBaby(string bPicture, string bName, Baby.Gender bGender, DateTime bDob, int bWeightUnits, int bWeightDecimals, float bLengthUnits, float bLengthDecimals)
	{
		Baby baby = new Baby();

		baby.profilePicture = bPicture;
		baby.babyName = bName;

		baby.gender = bGender;
		baby.dateOfBirth = new DateTime(bDob.Year, bDob.Month, bDob.Day);

		if(userUnit == Unit.metric)
		{
			baby.weightUnit = Baby.Unit.metric;
			baby.lengthUnit = Baby.Unit.metric;
		}
		else if(userUnit == Unit.imperial)
		{
			baby.weightUnit = Baby.Unit.imperial;
			baby.lengthUnit = Baby.Unit.imperial;
		}

		baby.bornWeightUnits = bWeightUnits;
		baby.bornWeightDecimals = bWeightDecimals;

		baby.bornLengthUnits = bLengthUnits;
		baby.bornLengthDecimals = bLengthDecimals;
	
		babies.Add(baby);
		SaveBabies();
		ShowBaby(babies.Count-1);

	    viewController.ToDashboardMainView();
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

			//Journal
			ArrayList listOfEvents = new ArrayList();

			foreach(Journal journal in baby.journals)
			{
				Hashtable eventHt = new Hashtable();
				eventHt.Add("date", journal.eventDate.ToString());
				eventHt.Add("picture", journal.eventPicture);

				eventHt.Add("title", journal.eventTitle);
				eventHt.Add("info", journal.eventDescription);

				listOfEvents.Add(eventHt);
			}
			ht.Add("jounalList", listOfEvents);


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
					mealht.Add("bothAmounts", meal.amount);
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
					mealht.Add("mealAmount", meal.amount);
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

				if(sleep.finishTime == DateTime.MinValue)
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



			//loading journalList for specific baby
			ArrayList listOfEvents = (ArrayList)babyHt["journalList"];

			for(int e = 0; e < listOfEvents.Count; e++)
			{
				Hashtable journalHt = (Hashtable)listOfEvents[e];
				Journal tempJournal = new Journal();

				tempJournal.eventDate = DateTime.Parse((string)journalHt["date"]);
				tempJournal.eventPicture = (string)journalHt["picture"];
				tempJournal.eventTitle = (string)journalHt["title"];
				tempJournal.eventDescription = (string)journalHt["info"];

				tempBaby.journals.Add(tempJournal);
			}



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
					tempMeal.amount = (float)(double)mealHt["bothAmounts"];
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
		currentBaby = babyIndex;

		//calls event - the baby has changed
		if(OnBabyChanged != null) OnBabyChanged();

		viewFeedingList.MealListRefresh();
		viewNappyList.NappyListRefresh();
		viewSleepingList.SleepingListRefresh();
		viewWeightList.WeightListRefresh();
		viewLengthList.LengthListRefresh();
		//viewJounalList.JounalListRefresh();
	}


	
	public void OnSwipe(SwipeGesture gesture)
	{
		if(viewController.currentView == viewController.viewDashboard)
		{
			if(gesture.Direction == FingerGestures.SwipeDirection.Left) NextBaby();
			else if(gesture.Direction == FingerGestures.SwipeDirection.Right) PreviousBaby();
		}
	}

	public void NextBaby()
	{
		if(currentBaby == babies.Count-1) ShowBaby(0);
		else ShowBaby(currentBaby+1);
	}
	
	public void PreviousBaby()
	{
		if(currentBaby > 0) ShowBaby(currentBaby-1);
		else ShowBaby(babies.Count-1);
	}



	public void AddJournalEvent(DateTime eTime, string ePicture, string eTitle, string eInfo)
	{
		Journal journal = new Journal();

		journal.eventDate = new DateTime(eTime.Year, eTime.Month, eTime.Day);
		journal.eventPicture = ePicture;
		journal.eventTitle = eTitle;
		journal.eventDescription = eInfo;

		babies[currentBaby].journals.Add(journal);

		SaveBabies();
		//viewJournalList.JournalListRefresh();
		viewController.ToViewJournalList();
	}



	public void AddMeal(DateTime mTime, string mtype)
	{
		Meal meal = new Meal();
		
		meal.time = new DateTime(mTime.Year, mTime.Month, mTime.Day, mTime.Hour, mTime.Minute, mTime.Second);

		if(mtype == "Breastfeed")
		{
			Debug.Log(mtype);
			meal.mealType = Meal.MealType.breastfeed;
			meal.unit = Meal.UnitType.min;
			breastfeedController.AddBreastfeedAmount();
			meal.leftAmount = breastfeedController.leftAmount;
			meal.rightAmount = breastfeedController.rightAmount;
			meal.amount = breastfeedController.bothBreastAmounts;
			Debug.Log(meal.amount);
		}
		else if(mtype == "Breastmilk")
		{
			meal.mealType = Meal.MealType.breastMilk;
			meal.amount = bottleController.currentAmount;
			if(userUnit == Unit.metric)
			{
				meal.unit = Meal.UnitType.ml;
			}
			else meal.unit = Meal.UnitType.oz;
		}
		else if(mtype == "Formula")
		{
			meal.mealType = Meal.MealType.formula;
			meal.amount = bottleController.currentAmount;
			if(userUnit == Unit.metric)
			{
				meal.unit = Meal.UnitType.ml;
			}
			else meal.unit = Meal.UnitType.oz;
		}
		else if(mtype == "Solids")
		{
			Debug.Log(mtype);
			meal.mealType = Meal.MealType.solidFood;
			meal.unit = Meal.UnitType.g; 
			meal.amount = cupController.currentAmount;

		}
		else Debug.LogError("Food type not recognised");
		
		babies[currentBaby].meals.Add(meal);

		SaveBabies();
		viewFeedingList.MealListRefresh();
		viewController.ToViewFeedingList();
	}

	
	public void AddNappy(DateTime nTime, string nType)
	{
		Nappy nappy  =  new Nappy();

		nappy.nappyTime = new DateTime(nTime.Year, nTime.Month, nTime.Day, nTime.Hour, nTime.Minute, nTime.Second);

		if(nType == "Wet") nappy.nappyType = Nappy.NappyType.Wet;
		else if(nType == "Stool") nappy.nappyType = Nappy.NappyType.Stool;
		else if(nType == "Both") nappy.nappyType = Nappy.NappyType.Both;
		else Debug.LogError("Nappy type not recognised");

		babies[currentBaby].nappies.Add(nappy);

		SaveBabies();
		viewNappyList.NappyListRefresh();
		viewController.ToViewNappyList();
	}



	public void AddSleeping(DateTime startTime, DateTime finishedTime)
	{
		//Debug.Log(GetSleepByStartTime(startTime).GetType());
		//for open sleep
		if(GetSleepByStartTime(startTime).startTime.Year != 2000)
		{
			Debug.Log("Closing existing sleep");
			Sleeping openSleep = GetSleepByStartTime(startTime);
			openSleep.finishTime = finishedTime;
		}
		//adding new closed sleep
		else
		{
			Debug.Log("Creating new sleep");
			Sleeping sleeping = new Sleeping();

			if(viewAddSleeping.noFinishCheckmark.value)
			{
				sleeping.startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, startTime.Minute, startTime.Second);
				sleeping.finishTime = DateTime.MinValue;
			}
			else
			{
				sleeping.startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, startTime.Minute, startTime.Second);
				sleeping.finishTime = new DateTime(finishedTime.Year, finishedTime.Month, finishedTime.Day, finishedTime.Hour, finishedTime.Minute, finishedTime.Second);
			}

			babies[currentBaby].sleeps.Add(sleeping);
		}


		SaveBabies();
		viewSleepingList.SleepingListRefresh();
		viewController.ToViewSleepingList();
	}



	public Sleeping GetSleepByStartTime(DateTime start)
	{

		foreach(Sleeping sleep in babies[currentBaby].sleeps)
		{
			if(start.Month == sleep.startTime.Month &&
			   start.Day == sleep.startTime.Day &&
			   start.Hour == sleep.startTime.Hour &&
			   start.Minute == sleep.startTime.Minute)
			{
				return sleep;
			}
		}
		Sleeping dummySleep = new Sleeping();
		return dummySleep;
	}


	
	public void AddWeight(DateTime wDate, int wUnits, int wDecimals)
	{
		Weight weight = new Weight();

		weight.weightDate = new DateTime(DateTime.Now.Year, wDate.Month, wDate.Day);

		if(userUnit == Unit.metric) weight.weightUnit = Weight.WeightUnit.metric;
		else weight.weightUnit = Weight.WeightUnit.imperial;

		weight.weightUnits = wUnits;
		weight.weightDecimals = wDecimals;


		babies[currentBaby].weights.Add(weight);

		SaveBabies();
		viewWeightList.WeightListRefresh();
		viewController.ToViewWeightList();
	}


	
	public void AddLength(DateTime lDate, float lUnits, float lDecimals)
	{
		Length length = new Length();
		
		length.lengthDate = new DateTime(DateTime.Now.Year, lDate.Month, lDate.Day);
		
		if(userUnit == Unit.metric) length.lengthUnit = Length.LengthUnit.metric;
		else length.lengthUnit = Length.LengthUnit.imperial;

		length.lengthUnits = lUnits;
		if(lDecimals > 0) length.lengthDecimals = lDecimals;

		babies[currentBaby].lengths.Add(length);
		
		SaveBabies();
		viewLengthList.LengthListRefresh();
		viewController.ToViewLengthList();
	}



}