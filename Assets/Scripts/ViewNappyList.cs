using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ViewNappyList : UIView {


	public UserManager userManager;
	public ViewTracker viewTracker;

	public UIList nappiesList;


	public override void Show()
	{
		NappyListRefresh();
		base.Show();
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
			if (timeAgo.Days <= 7) recentNappies.Add(nappy);
		}


		//at this point we have recent 7 days of nappy

		//sorting nappies by date
		//declaring sorted list
		List<Nappy> sortedList = new List<Nappy>();
		//adding first entry to have someting to compare to
		if(recentNappies.Count > 0) sortedList.Add(recentNappies[0]);


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
				viewTracker.UpdateLastNappyLabels(recentNappies[n].nappyTime, recentNappies[n].nappyType);
			}
		}

		//Insert dividers
		ArrayList dividedList = new ArrayList();


		if (sortedList.Count > 0)
		{
			//adding first divider
			dividedList.Add(sortedList[0].nappyTime);

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
					else dividedList.Add(sortedList[e]);
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
}
