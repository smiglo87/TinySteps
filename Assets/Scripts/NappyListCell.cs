using UnityEngine;
using System.Collections;
using System;

public class NappyListCell : MonoBehaviour {


	public UILabel time;
	public UILabel nappyType;

	public GameObject entryRoot;
	public GameObject dividerRoot;
	public UILabel weekDayLabel;



	public void Refresh(object obj)
	{

		if (obj.GetType() == typeof(Nappy))
		{
			Nappy nappyOnTheList = (Nappy)obj;

			entryRoot.SetActive(true);
			dividerRoot.SetActive(false);


			time.text = nappyOnTheList.nappyTime.ToString("HH:mm");

			if(nappyOnTheList.nappyType == Nappy.NappyType.Wet) nappyType.text = "Wet";
			else if(nappyOnTheList.nappyType == Nappy.NappyType.Stool) nappyType.text = "Stool";
			else if(nappyOnTheList.nappyType == Nappy.NappyType.Both) nappyType.text = "Both";

		}
		else if (obj.GetType() == typeof(DateTime))
		{
			entryRoot.SetActive(false);
			dividerRoot.SetActive(true);
			
			DateTime dayOfWeek = (DateTime)obj;
			weekDayLabel.text = dayOfWeek.DayOfWeek.ToString();
		}
	}
}
