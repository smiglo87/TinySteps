using UnityEngine;
using System.Collections;
using System;

public class ViewAddFeeding : UIView {

	public UIInput feedingHour;
	public UIInput feedingMin;
	public UIPopupList foodType;
	public UILabel mealAmount;
	public UIList mealsList;
	
	public GameObject bottle;
	public IPTextPicker leftBreast;
	public IPTextPicker rightBreast;
	public GameObject breastfeeding;
	
	public GameObject cup;


	public override void Show()
	{
		Debug.Log("ViewAddFeeding show called");
		base.Show();
	}

}
