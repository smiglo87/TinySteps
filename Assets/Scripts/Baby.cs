using UnityEngine;
using System.Collections;
using System;
using Prime31;
using System.Collections.Generic;
	
[System.Serializable]

public class Baby : MonoBehaviour {
	
	

	public string babyName = "";
	public enum Gender { male, female };
	public Gender gender;
	public DateTime dateOfBirth = new DateTime(2000, 01, 01);
	public enum Unit { metric, imperial };
	public Unit weightUnit;
	public int bornWeightUnits = 0;
	public int bornWeightDecimals = 0;
	public Unit lengthUnit;
	public float bornLengthUnits = 0;
	public float bornLengthDecimals = 0;
	public string profilePicture = "";
	
	
	public List<Meal> meals = new List<Meal>();
	public List<Nappy> nappies = new List<Nappy>();
	public List<Sleeping> sleeps = new List<Sleeping>();
	public List<Weight> weights = new List<Weight>();
	public List<Length> lengths = new List<Length>();

	
	/// <summary>
	/// Gets the age and returns string in "years,months" format.
	/// </summary>
	/// <returns>The age.</returns>
	public string GetAge()
	{

		string formattedAge = "";
			
		DateTime todayDate = DateTime.Today;
		int babyAgeInYears = todayDate.Year - dateOfBirth.Year;
		int babyAgeInMonths = todayDate.Month - dateOfBirth.Month;
		int babyAgeInDays = todayDate.Day - dateOfBirth.Day;
		
			
			if(babyAgeInMonths < 0 && babyAgeInMonths > -12 && dateOfBirth.Day > todayDate.Day)
			{
				babyAgeInYears = babyAgeInYears - 1;
				int numberOfMonths = babyAgeInMonths + 11;
				formattedAge = babyAgeInYears + "," + numberOfMonths;
			}
			else if(babyAgeInMonths < 0 && babyAgeInMonths > -12)
			{
				babyAgeInYears = babyAgeInYears - 1;
				int numberOfMonths = babyAgeInMonths + 12;
				formattedAge = babyAgeInYears + "," + numberOfMonths;
			}
			else if(babyAgeInMonths == 0 && dateOfBirth.Day > todayDate.Day)
			{
				babyAgeInYears = babyAgeInYears - 1;
				int numberOfMonths = babyAgeInMonths + 11;
				formattedAge = babyAgeInYears + "," + numberOfMonths;
			}
			else 	
			{
				formattedAge = babyAgeInYears + "," + babyAgeInMonths;
			}
		
	
			
		return formattedAge;
		
	}
		
	public static bool IsDateTime(string txtDate)
    {
       DateTime tempDate;
       return DateTime.TryParse(txtDate, out tempDate) ? true : false;
	}
	
		
	
	

}
