using UnityEngine;
using System.Collections;
using System;

public class Weight : MonoBehaviour {


	public DateTime weightDate = new DateTime(2000, 01, 01);

	public enum WeightUnit { metric, imperial };
	public WeightUnit weightUnit;

	public int weightUnits = 0;
	public int weightDecimals = 0;
}
