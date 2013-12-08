using UnityEngine;
using System.Collections;
using System;

public class SleepingListCell : MonoBehaviour {

	public GUIManager guiManager;


	public UILabel startTime;
	public UILabel sleepStatus;
	public UILabel durationLabel;
	public UIButton addFinishTime;

	public GameObject entryRoot;
	public GameObject dividerRoot;
	public UILabel weekDayLabel;

	public void Refresh(object obj)
	{
		if (obj.GetType() == typeof(Sleeping))
		{
			Sleeping sleepOnTheList = (Sleeping)obj;
			
			entryRoot.SetActive(true);
			dividerRoot.SetActive(false);


			startTime.text = sleepOnTheList.startTime.ToString("HH:mm");


			if(sleepOnTheList.finishTime == DateTime.MinValue)
			{
				durationLabel.gameObject.SetActive(false);
				addFinishTime.gameObject.SetActive(true);
				sleepStatus.text = "Start";
			}
			else
			{
				addFinishTime.gameObject.SetActive(false);
				TimeSpan duration = sleepOnTheList.finishTime - sleepOnTheList.startTime;
				durationLabel.text = duration.Hours + " h " + duration.Minutes + " min";
			}

		}
		else if (obj.GetType() == typeof(DateTime))
		{
			entryRoot.SetActive(false);
			dividerRoot.SetActive(true);
				
			DateTime dayOfWeek = (DateTime)obj;
			weekDayLabel.text = dayOfWeek.DayOfWeek.ToString();
		}
	}

	public void AddFinishTime(DateTime unfinishedSleep)
	{





	}


}
