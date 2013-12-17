using UnityEngine;
using System.Collections;
using System;

public class Meal : MonoBehaviour {
	
	public enum MealType { breastfeed, breastMilk, formula, solidFood };
	public MealType mealType;
	public DateTime time = new DateTime(2000, 01, 01, 00, 00, 00);
	public float amount = 0.0f;
	public enum UnitType { ml, oz, g, min };
	public UnitType unit;
	
	public float leftAmount = 0.0f;
	public float rightAmount = 0.0f;

	public float cupAmount = 0.0f;


}
