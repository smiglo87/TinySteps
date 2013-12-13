﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GUIManager : MonoBehaviour {
	
	public ViewManager viewManager;
	public UserManager userManager;

	//GrowthLabelsRefresh

	public UILabel lastLengthDate;
	public UILabel lastLength;
	public UILabel lengthTimeSince;


	//AddLength
	public UIInput lengthDay;
	public UIInput lengthMonth;
	public UIInput lengthUnits;
	public UIInput lengthDecimals;

	public UIList lengthsList;



	public void FillLength()
	{
		if(lengthUnits.value == "cm" || lengthUnits.value == "ft" || lengthUnits.value == "") lengthUnits.value = "0";
		
		if(lengthDecimals.value == "inch" || lengthDecimals.value == "") lengthDecimals.value = "00";
	}


	public void LabelLengthClearing()
	{
		lengthUnits.value = "";
		lengthDecimals.value = "";
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






