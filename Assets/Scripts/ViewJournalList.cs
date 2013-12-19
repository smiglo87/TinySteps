using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ViewJournalList : UIView {

	public UserManager userManager;

	public UIList eventList;


	public override void Show()
	{
		//JournalListRefresh();
		base.Show();
	}

	public void JournalListRefresh()
	{
		ArrayList journalList = new ArrayList(userManager.babies[userManager.currentBaby].journals);

		List<Journal> recentEvents = new List<Journal>();
		
		//loop going through all position in meal list
		foreach (Journal journal in journalList)
		{
			 recentEvents.Add(journal);
		}

		//declaring sorted list
		List<Journal> sortedList = new List<Journal>();
		//adding first entry to have someting to compare to
		if (recentEvents.Count > 0)  sortedList.Add(recentEvents[0]);
		
		//loop comparing each object with all in sorted list, starting loop from position 1 not 0 as we use first entry to compare to
		for (int i=1; i<recentEvents.Count; i++)
		{
			//declaring this variable to store position on the list if found
			int finalIndex = -1;
			
			//another loop going through sorted positions itself
			for (int s=0; s<sortedList.Count; s++)
			{
				if (finalIndex == -1)
				{
					if (DateTime.Compare(recentEvents[i].eventDate, sortedList[s].eventDate) < 0) //earlier
					{
						finalIndex = s;
						sortedList.Insert(finalIndex, recentEvents[i]);
					}
				}
			}
			//inside later entry not found so adding in the end of sorted list
			if (finalIndex == -1) 
			{
				sortedList.Add(recentEvents[i]);
			}
		}

		//journalList.BuildList(sortedList);
	}
}