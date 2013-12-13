using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ViewWeightList : UIView {

	public UserManager userManager;
	public ViewGrowth viewGrowth;

	public UIList weightsList;

	public override void Show()
	{
		WeightListRefresh();
		base.Show();
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

		if(recentWeights.Count > 0) sortedList.Add(recentWeights[0]);

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
				viewGrowth.UpdateLastWeightLabels(recentWeights[w].weightDate, recentWeights[w].weightUnits, recentWeights[w].weightDecimals, recentWeights[w].weightUnit);
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
					else dividedList.Add(sortedList[e]);
				}
				//last one on the list
				else dividedList.Add(sortedList[e]);
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
}
