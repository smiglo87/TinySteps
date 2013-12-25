using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ViewSleepingList : UIView {

	public UserManager userManager;
	public ViewTracker viewTracker;
	public ViewAddSleeping viewAddSleeping;

	public UIList sleepList;

	public override void Show()
	{
		SleepingListRefresh();
		base.Show();
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
				//sends last sleep to tracker labels
				viewTracker.UpdateLastSleepingLabels(recentSleeps[m].startTime, recentSleeps[m].finishTime);
			}
		}
		
		//Insert dividers
		ArrayList dividedList = new ArrayList();
		
		if (sortedList.Count > 0)
		{
			dividedList.Add(sortedList[0].startTime);

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
					else dividedList.Add(sortedList[e]);
				}
				//last one on the list
				else dividedList.Add(sortedList[e]);
			}
		}
		sleepList.BuildList(dividedList);
	}
}
