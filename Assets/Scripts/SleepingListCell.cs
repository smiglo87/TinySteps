using UnityEngine;
using System.Collections;
using System;

public class SleepingListCell : MonoBehaviour {

	public UserManager userManager;
	public ViewAddSleeping viewAddSleeping;

	public UILabel startTime;
	public UILabel sleepStatus;
	public UILabel durationLabel;
	public UIButton addFinishTime;

	public DateTime start;

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

			start = sleepOnTheList.startTime;

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


	public void AddFinish()
	{
		//Debug.Log(start);
		GameObject addSleeping = GameObject.Find("9b.AddSleeping");
		viewAddSleeping = addSleeping.GetComponent<ViewAddSleeping>();
		viewAddSleeping.FinishSleep(start);
	}
}
