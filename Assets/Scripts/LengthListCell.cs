using UnityEngine;
using System.Collections;
using System;

public class LengthListCell : MonoBehaviour {

	public ViewAddLength viewAddLength;

	public UILabel date;
	public UILabel length;
	
	public GameObject entryRoot;
	public GameObject dividerRoot;
	public UILabel monthLabel;
	
	public Length currentLength;
	
	public void Refresh(object obj)
	{
		
		if (obj.GetType() == typeof(Length))
		{
			Length lengthOnTheList = (Length)obj;

			currentLength = lengthOnTheList;

			entryRoot.SetActive(true);
			dividerRoot.SetActive(false);
			
			date.text = lengthOnTheList.lengthDate.ToString("dd.MM");
			
			if(lengthOnTheList.lengthUnit == Length.LengthUnit.metric) length.text = lengthOnTheList.lengthUnits + " cm";
			else length.text = lengthOnTheList.lengthUnits + " ft " + lengthOnTheList.lengthDecimals + " inch";	
		}
		else if (obj.GetType() == typeof(DateTime))
		{
			entryRoot.SetActive(false);
			dividerRoot.SetActive(true);
			
			DateTime month = (DateTime)obj;
			monthLabel.text = month.Date.ToString("MMMM") + ", " + month.Date.ToString("yyyy");
		}
	}

	public void EditLength()
	{
		GameObject addLength = GameObject.Find("10d.AddLength");
		viewAddLength = addLength.GetComponent<ViewAddLength>();
		viewAddLength.EditCurrentLength(currentLength);
	}

}
